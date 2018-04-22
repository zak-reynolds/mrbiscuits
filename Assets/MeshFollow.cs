using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFollow : MonoBehaviour {

    public enum VelocityMethod { Rigidbody, NavMeshAgent }
    
    private float rotationDamping = 10f;
    private float velocityDeadZone = 0.01f;
    private float tiltSpeedThreshold = 5f;
    private float tiltDamping = 8f;
    
    private Rigidbody rb;
    private Animator animator;

    private enum CharacterParam
    {
        Idle, Run,
        Pass, Reach, PassMirror, ReachMirror
    }

    private Dictionary<CharacterParam, int> animatorParameterMap;

    private float[] splineSamples;

    private void InitializeStaticData()
    {
        if (animatorParameterMap == null)
        {
            animatorParameterMap = new Dictionary<CharacterParam, int>()
            {
                { CharacterParam.Idle, Animator.StringToHash("Idle") },
                { CharacterParam.Run, Animator.StringToHash("Run") },

                { CharacterParam.Pass, Animator.StringToHash("Pass") },
                { CharacterParam.Reach, Animator.StringToHash("Reach") },
                { CharacterParam.PassMirror, Animator.StringToHash("Pass-Mirror") },
                { CharacterParam.ReachMirror, Animator.StringToHash("Reach-Mirror") }
            };
        }
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        InitializeStaticData();
        lastFrame = animatorParameterMap[CharacterParam.Idle];
        nextFrame = animatorParameterMap[CharacterParam.Idle];
        lastPosition = transform.position;
        target = GameObject.Find("Player").transform;
    }


    private Vector3 velocity;
    private Vector3 lastPosition;

    private float speedOffset = 0f;
    private float radius = 0.5f;
    private Transform target;

    void Update()
    {
        transform.position = target.position;
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        var flattenedVelocity = new Vector3(velocity.x, 0, velocity.z).normalized;
        if (new Vector3(velocity.x, 0, velocity.z).magnitude > velocityDeadZone)
        {
            var velocityRotation = Quaternion.LookRotation(flattenedVelocity);
            var targetRotation = Quaternion.Slerp(velocityRotation, velocityRotation * GetTiltRotation(), Time.deltaTime * tiltDamping);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
        }
        if (!animator || animatorParameterMap == null) return;
        if (speedOffset > Mathf.Deg2Rad * 360) speedOffset -= Mathf.Deg2Rad * 360;
        radius = Mathf.Clamp(new Vector3(velocity.x, 0, velocity.z).magnitude, 0.1f, 1f);
        speedOffset += new Vector3(velocity.x, 0, velocity.z).magnitude / radius * Time.deltaTime;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")) < 0.1)
        {
            animator.SetFloat(animatorParameterMap[CharacterParam.Idle], 1f);
            animator.SetFloat(animatorParameterMap[CharacterParam.Run], 0f);
        }
        else
        {
            UpdateAnimator();
        }
        lastPosition = transform.position;
    }

    private int nextFrame;
    private int lastFrame;

    private void UpdateAnimator()
    {
        animator.SetFloat(animatorParameterMap[CharacterParam.Idle], 0f);
        animator.SetFloat(animatorParameterMap[CharacterParam.Run], 1f);
        int quantized = Mathf.FloorToInt((Mathf.Rad2Deg * speedOffset) / 360f * 8f);
        int targetFrame = 0;
        switch (quantized % 4)
        {
            case 0: targetFrame = animatorParameterMap[CharacterParam.Pass]; break;
            case 1: targetFrame = animatorParameterMap[CharacterParam.Reach]; break;
            case 2: targetFrame = animatorParameterMap[CharacterParam.PassMirror]; break;
            case 3: targetFrame = animatorParameterMap[CharacterParam.ReachMirror]; break;
        }
        if (targetFrame != nextFrame)
        {
            animator.SetFloat(lastFrame, 0f);
            lastFrame = nextFrame;
            nextFrame = targetFrame;
        }
        float t = ((Mathf.Rad2Deg * speedOffset) / 360f * 8f) - quantized;
        animator.SetFloat(lastFrame, 1 - t);
        animator.SetFloat(nextFrame, t);
    }

    private Quaternion GetTiltRotation()
    {
        var flatVelocity = new Vector3(velocity.x, velocity.z, 0).normalized;
        if (velocity.magnitude < tiltSpeedThreshold)
        {
            return Quaternion.identity;
        }
        var direction = new Vector3(transform.forward.x, transform.forward.z, 0).normalized;
        var cross = Vector3.Cross(direction, flatVelocity);
        var dot = Vector3.Dot(direction, flatVelocity);
        if (dot > 0.999999f)
        {
            return Quaternion.identity;
        }
        if (dot < -0.999999f)
        {
            return Quaternion.Inverse(Quaternion.identity);
        }
        return new Quaternion(
            cross.x,
            cross.y,
            cross.z,
            Mathf.Sqrt((direction.magnitude * direction.magnitude) * (flatVelocity.magnitude * flatVelocity.magnitude)) + dot
        );
    }
}

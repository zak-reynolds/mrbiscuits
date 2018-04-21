using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public Vector3 currentVelocity = Vector3.zero;
    public float dampTime = 0.5f;
    public float maxSpeed = 4f;
    	
	void LateUpdate () {
		if (target != null)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                target.position + offset,
                ref currentVelocity,
                dampTime,
                maxSpeed,
                Time.deltaTime);
        }
	}
}

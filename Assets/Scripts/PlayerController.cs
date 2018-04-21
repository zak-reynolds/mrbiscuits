using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public float speed = 6;

    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

	void FixedUpdate () {
        var velocity = Vector3.down;
        velocity.x += Input.GetAxis("Horizontal");
        velocity.z += Input.GetAxis("Vertical");
        cc.Move(velocity.normalized * Time.deltaTime * speed);
    }
}

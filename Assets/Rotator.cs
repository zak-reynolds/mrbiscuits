using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float speed = 1f;
    private float angle = 0;

	void Update () {
        transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * speed * 360, Vector3.forward);
	}
}

using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public float speed = 6;

    private CharacterController cc;
    private Spawner spawner;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        spawner = GetComponentInChildren<Spawner>();
    }

	void FixedUpdate () {
        var velocity = Vector3.down;
        velocity.x += Input.GetAxis("Horizontal");
        velocity.z += Input.GetAxis("Vertical");
        cc.Move(velocity.normalized * Time.deltaTime * speed * (spawner.Charge <= 0 ? 0.5f : 1));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("EscapePoint") && GameObject.FindGameObjectWithTag("MrBiscuits4") != null)
        {
            GameOrchestrator.NextPhase();
            Utility.PhysicalDestroy(gameObject);
        }
        if (col.CompareTag("MrBiscuits4"))
        {
            GameOrchestrator.PlayerKilled();
            Utility.PhysicalDestroy(gameObject);
        }
    }
}

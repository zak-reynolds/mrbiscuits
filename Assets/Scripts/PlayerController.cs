using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            MessageController.AddMessage("You escaped!");
            MessageController.AddMessage("but Mr. Biscuits has been unleashed upon this world");
            MessageController.AddMessage("-.. --- --- -- ... --- ...");
            MessageController.AddMessage("This was an entry in the Ludum Dare 41 compo");
            MessageController.AddMessage("By Zak Reynolds");
            MessageController.AddMessage("Thanks for playing!");
        }
        if (col.CompareTag("MrBiscuits4"))
        {
            MessageController.AddMessage("You have fed Mr. Biscuits the ultimate meal");
            MessageController.AddMessage("Game over");
            Utility.PhysicalDestroy(gameObject);
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BatController : MonoBehaviour {

    public BatSpawner parentSpawner;
    public float speed = 0.5f;

    private GameObject target;
    private CharacterController cc;

	void Start () {
        cc = GetComponent<CharacterController>();
	}
	
	void Update () {
        if (parentSpawner.Charge > 0) target = parentSpawner.gameObject;
        else target = null;
        if (target == null) target = GameObject.FindGameObjectWithTag("MrBiscuits");
        if (target == null) target = GameObject.FindGameObjectWithTag("MrBiscuits2");
        if (target == null) target = GameObject.FindGameObjectWithTag("MrBiscuits3");
        if (target == null) target = GameObject.FindGameObjectWithTag("MrBiscuits4");

        if (target != null)
        {
            transform.LookAt(target.transform);
            cc.Move(transform.forward * speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ChickenController : MonoBehaviour {
    
    private CharacterController cc;
    private float speed = 0;
    private bool active = true;

    private IEnumerator ChangeDirection()
    {
        while (active)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            transform.Rotate(new Vector3(0, Random.Range(0, 359), 0));
        }
    }
    private IEnumerator ChangeSpeed()
    {
        while (active)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            speed = Random.Range(0, 2) == 0 ? 0 : Random.Range(0.5f, 4);
        }
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        StartCoroutine(ChangeDirection());
        StartCoroutine(ChangeSpeed());
    }

	void Update () {
        cc.Move(transform.forward * speed * Time.deltaTime);
	}

    void OnDestroy()
    {
        active = false;
    }
}

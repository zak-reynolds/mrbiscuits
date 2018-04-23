using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SuperBiscuitsController : MonoBehaviour {

    public float speed = 1.2f;
    public float feedTime = 1f;

    public bool isFeeding = false;

    private Transform target;
    private CharacterController cc;

    public float startTimer = 3;

	void Start () {
        cc = GetComponent<CharacterController>();
        target = ((GameObject)GameObject.FindGameObjectWithTag("Player")).transform;
    }
	
	void Update ()
    {
        startTimer -= Time.deltaTime;
        if (startTimer < 0 && target != null && target.transform.position.y > -100 && !isFeeding)
        {
            transform.LookAt(target);
            cc.Move(transform.forward * speed * Time.deltaTime);
        }
    }

    private IEnumerator FeedTimer()
    {
        isFeeding = true;
        yield return new WaitForSeconds(feedTime);
        isFeeding = false;
    }

    void OnFedItem(InventorySlot.ItemType type)
    {
        StartCoroutine(FeedTimer());
    }
}

using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class MrBiscuitsController : MonoBehaviour {

    public GameObject nextPrefab;
    public int foodToEvolve = 1;
    private int timesFed = 0;
    private int attacks = 0;

    public float Evolution { get { return (float)timesFed / foodToEvolve; } }
    public float Health = 1f;

    private bool active = true;

    private IEnumerator AttackTimer()
    {
        while (active)
        {
            if (attacks > 0)
            {
                OnAttacked(0.1f);
            }
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    void Start()
    {
        StartCoroutine(AttackTimer());
    }

    void OnDestroy()
    {
        active = false;
    }

    void OnFedItem(InventorySlot.ItemType type)
    {
        timesFed++;
        if (timesFed >= foodToEvolve)
        {
            Instantiate(nextPrefab, transform.position, Quaternion.identity);
            Utility.PhysicalDestroy(gameObject);
        }
        Health = 1f;
    }

    void OnAttacked(float amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Utility.PhysicalDestroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bat"))
        {
            attacks++;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Bat"))
        {
            attacks--;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp)) {
            OnFedItem(InventorySlot.ItemType.None);
        }
    }
}

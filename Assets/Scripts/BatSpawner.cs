using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour, IHaveCharge
{
    public GameObject prefab;
    public float spawnDistance = 2;

    public float Charge { get { return charge; } }
    public float decayTime = 3;
    public float spawnRate = 1;

    private float charge = 0;
    private float rotation = 0;

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (charge == 0)
            {
                var offset = Quaternion.AngleAxis(rotation, Vector3.up) * transform.forward * spawnDistance;
                rotation += 100;
                var bat = Instantiate(prefab, transform.position + offset, Quaternion.identity);
                bat.GetComponent<BatController>().parentSpawner = this;
            }
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {
        charge = Mathf.Max(0, charge - (Time.deltaTime / decayTime));
    }

    void OnFedItem(InventorySlot.ItemType type)
    {
        charge = 1;
    }
}

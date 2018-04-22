using Assets;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour, IHaveCharge {

    public GameObject prefab;
    public float spawnDistance = 2;
    public bool continuous = false;

    public float Charge { get { return charge; } }
    public float decayTime = 3;
    public float spawnRate = 1;

    private float charge = 0;
    private float rotation = 0;

    private IEnumerator Spawn()
    {
        bool spawned = false;
        while (!spawned || continuous)
        {
            spawned = true;
            yield return new WaitForSeconds(spawnRate);
            if (!continuous || charge > 0)
            {
                var offset = Quaternion.AngleAxis(rotation, Vector3.up) * transform.forward * spawnDistance;
                rotation += 100;
                Instantiate(prefab, transform.position + offset, Quaternion.identity);
            }
        }
    }

    void Start()
    {
        if (continuous)
        {
            StartCoroutine(Spawn());
        }
    }

	void Update () {
        charge = Mathf.Max(0, charge - (Time.deltaTime / decayTime));
	}

    public void OnFedItem(InventorySlot.ItemType type)
    {
        charge = 1;
        if (!continuous)
        {
            StartCoroutine(Spawn());
        }
    }
}

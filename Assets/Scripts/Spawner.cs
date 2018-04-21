using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject prefab;
    public float spawnDistance = 2;

    public float charge = 0;
    public float decayTime = 3;
    public float spawnRate = 1;

    private float rotation = 0;

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (charge > 0)
            {
                var offset = Quaternion.AngleAxis(rotation, Vector3.up) * transform.forward * spawnDistance;
                rotation += 100;
                Instantiate(prefab, transform.position + offset, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

	void Update () {
        charge = Mathf.Max(0, charge - (Time.deltaTime / decayTime));
	}

    void OnFedItem(InventorySlot.ItemType type)
    {
        charge = 1;
    }
}

using Assets;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChargeCanvasController : MonoBehaviour {

    private IHaveCharge spawner;
    private bool active = true;
    private float targetAmount = 0;
    public Image image;

    void Start()
    {
        spawner = transform.parent.GetComponent<Spawner>();
        if (spawner == null) spawner = transform.parent.GetComponent<BatSpawner>();
        targetAmount = spawner.Charge;
        StartCoroutine(SetTarget());
    }

    private IEnumerator SetTarget()
    {
        while (active)
        {
            targetAmount = spawner.Charge;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnDestroy()
    {
        active = false;
    }

    void Update()
    {
        bool speedy = spawner.Charge == 1;
        image.fillAmount = Mathf.Lerp(image.fillAmount, (speedy ? 1 : targetAmount), Time.deltaTime * (speedy ? 10f : 2f));
        if (targetAmount < image.fillAmount && image.fillAmount < 0.01)
        {
            image.fillAmount = 0;
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}

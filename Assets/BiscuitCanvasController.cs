using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BiscuitCanvasController : MonoBehaviour
{

    private MrBiscuitsController mbc;
    private bool active = true;
    private float targetAmount1 = 0;
    private float targetAmount2 = 1;
    public Image image1;
    public Image image2;

    void Start()
    {
        mbc = transform.parent.GetComponent<MrBiscuitsController>();
        if (mbc != null)
        {
            targetAmount1 = mbc.Evolution;
            targetAmount2 = mbc.Health;
        } else
        {
            targetAmount1 = 0;
            targetAmount2 = 1;
        }
        StartCoroutine(SetTarget());
    }

    private IEnumerator SetTarget()
    {
        while (active && mbc != null)
        {
            targetAmount1 = mbc.Evolution;
            targetAmount2 = mbc.Health;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnDestroy()
    {
        active = false;
    }

    void Update()
    {
        image1.fillAmount = Mathf.Lerp(image1.fillAmount, targetAmount1, Time.deltaTime * 2f);
        if (targetAmount1 < image1.fillAmount && image1.fillAmount < 0.01)
        {
            image1.fillAmount = 0;
        }
        image2.fillAmount = Mathf.Lerp(image2.fillAmount, targetAmount2, Time.deltaTime * 2f);
        if (targetAmount2 < image2.fillAmount && image2.fillAmount < 0.01)
        {
            image2.fillAmount = 0;
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}

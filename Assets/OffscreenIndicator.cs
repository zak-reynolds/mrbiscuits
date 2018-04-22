using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OffscreenIndicator : MonoBehaviour {

    private Camera mainCamera;
    private RectTransform goalIndicator;
    private Image image;

    private bool isFlashing = false;

	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        goalIndicator = GameObject.Find("GoalIndicator").GetComponent<RectTransform>();
        image = goalIndicator.gameObject.GetComponent<Image>();
	}

    private IEnumerator Flash()
    {
        while (isFlashing)
        {
            yield return new WaitForSeconds(0.5f);
            image.enabled = !image.enabled;
        }
        image.enabled = false;
    }
	
    public void StartFlashing()
    {
        isFlashing = true;
        StartCoroutine(Flash());
    }
    public void StopFlashing()
    {
        isFlashing = false;
    }

    void Update () {
        var anchor = new Vector2(
            Mathf.Clamp01(mainCamera.WorldToScreenPoint(transform.position).x / mainCamera.pixelWidth),
            Mathf.Clamp01(mainCamera.WorldToScreenPoint(transform.position).y / mainCamera.pixelHeight));
        goalIndicator.anchorMax = anchor;
        goalIndicator.anchorMin = anchor;
        goalIndicator.pivot = anchor;
        
	}
}

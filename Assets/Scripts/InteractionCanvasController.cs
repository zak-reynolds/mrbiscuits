using UnityEngine;
using UnityEngine.UI;

public class InteractionCanvasController : MonoBehaviour {
    void OnSelectedInteractableChanged(Transform interactable)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void OnSelectedInteractableRemoved()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

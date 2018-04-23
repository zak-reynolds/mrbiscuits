using System.Collections.Generic;
using UnityEngine;

public class InteractableTracker : MonoBehaviour {

    public Transform SelectedInteractable { get { return selectedInteractable; } }

    private Transform selectedInteractable = null;
    private List<Transform> nearInteractables = new List<Transform>(5);
    
    private float Distance(Transform a, Transform b)
    {
        return Mathf.Abs((a.position - b.position).magnitude);
    }

	void Update()
    {
        var newSelectedInteractable = selectedInteractable;
        foreach (Transform t in nearInteractables)
        {
            if (selectedInteractable == null ||
                Distance(t, transform) < Distance(selectedInteractable, transform))
            {
                newSelectedInteractable = t;
            }
        }
        if (newSelectedInteractable != selectedInteractable)
        {
            selectedInteractable = newSelectedInteractable;
            transform.parent.SendMessage("OnSelectedInteractableChanged", selectedInteractable, SendMessageOptions.DontRequireReceiver);
            for (int i = 0; i < transform.parent.childCount; ++i)
            {
                transform.parent.GetChild(i).SendMessage("OnSelectedInteractableChanged", selectedInteractable, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
	
	void OnTriggerEnter(Collider col)
    {
        nearInteractables.Add(col.transform);
    }

    void OnTriggerExit(Collider col)
    {
        nearInteractables.Remove(col.transform);
        if (nearInteractables.Count == 0)
        {
            selectedInteractable = null;
            transform.parent.SendMessage("OnSelectedInteractableRemoved", SendMessageOptions.DontRequireReceiver);
            for (int i = 0; i < transform.parent.childCount; ++i)
            {
                transform.parent.GetChild(i).SendMessage("OnSelectedInteractableRemoved", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}

using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

    public enum ItemType { Water = 0, Grass, Chicken, Egg, Carrot, None }

    [Header("Config")]
    public Vector3 propOffset;

    [Header("Water, Grass, Chicken, Egg, Carrot")]
    public List<GameObject> meshes;
    public ItemType selectedItem = ItemType.None;
    private List<GameObject> props;
    private ItemType selectedInteractable = ItemType.None;
    private Transform selectedInteractableTransform = null;

	void Start ()
    {
        props = new List<GameObject>(meshes.Count);
        foreach (var gob in meshes)
        {
            props.Add(Instantiate(gob));
        }
        foreach (var gob in props)
        {
            gob.transform.SetParent(transform);
            gob.transform.localPosition = propOffset;
            gob.SetActive(false);
        }
	}
    void OnSelectedInteractableChanged(Transform t)
    {
        switch (t.parent.tag)
        {
            case "Water":
                selectedInteractable = ItemType.Water;
                break;
            case "Grass":
                selectedInteractable = ItemType.Grass;
                break;
            case "Chicken":
                selectedInteractable = ItemType.Chicken;
                break;
            case "Egg":
                selectedInteractable = ItemType.Egg;
                break;
            case "Carrot":
                selectedInteractable = ItemType.Carrot;
                break;
        }
        selectedInteractableTransform = t;
    }
    void OnSelectedInteractableRemoved()
    {
        selectedInteractable = ItemType.None;
        selectedInteractableTransform = null;
    }
    void Update () {
		if (Input.GetButtonDown("Interact"))
        {
            // Pick up
            if (selectedItem == ItemType.None)
            {
                switch (selectedInteractable)
                {
                    case ItemType.None:
                        break;
                    case ItemType.Water:
                    case ItemType.Grass:
                    case ItemType.Egg:
                    case ItemType.Carrot:
                        SetSelectedItem(selectedInteractable);
                        Utility.PhysicalDestroy(selectedInteractableTransform.parent.gameObject);
                        break;
                }
            }
            // Feed
            else
            {

            }
        }
	}

    private void SetSelectedItem(ItemType itemType)
    {
        if (selectedItem != ItemType.None)
        {
            props[(int)selectedItem].SetActive(false);
        }
        if (itemType != ItemType.None)
        {
            props[(int)itemType].SetActive(true);
        }
        selectedItem = itemType;
    }
}

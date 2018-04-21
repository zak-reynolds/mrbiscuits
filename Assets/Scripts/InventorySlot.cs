using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

    public enum ItemType { None = -1, Water = 0, Grass, Chicken, Egg, Carrot, MrBiscuits, WaterSpawner, GrassSpawner, CarrotSpawner, MrBiscuits2, MrBiscuits3, MrBiscuits4, BatSpawner }

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
            case "MrBiscuits":
                selectedInteractable = ItemType.MrBiscuits;
                break;
            case "WaterSpawner":
                selectedInteractable = ItemType.WaterSpawner;
                break;
            case "GrassSpawner":
                selectedInteractable = ItemType.GrassSpawner;
                break;
            case "CarrotSpawner":
                selectedInteractable = ItemType.CarrotSpawner;
                break;
            case "MrBiscuits2":
                selectedInteractable = ItemType.MrBiscuits2;
                break;
            case "MrBiscuits3":
                selectedInteractable = ItemType.MrBiscuits3;
                break;
            case "MrBiscuits4":
                selectedInteractable = ItemType.MrBiscuits4;
                break;
            case "BatSpawner":
                selectedInteractable = ItemType.BatSpawner;
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
                        SetSelectedItem(selectedInteractable);
                        break;
                    case ItemType.Grass:
                    case ItemType.Egg:
                    case ItemType.Carrot:
                    case ItemType.Chicken:
                        SetSelectedItem(selectedInteractable);
                        Utility.PhysicalDestroy(selectedInteractableTransform.parent.gameObject);
                        break;
                }
            }
            // Feed
            else
            {
                switch (selectedInteractable)
                {
                    case ItemType.None:
                        // Eat
                        Utility.SendMessageToChildren(transform.parent, "OnFedItem", selectedItem, SendMessageOptions.DontRequireReceiver);
                        SetSelectedItem(ItemType.None);
                        break;
                    case ItemType.WaterSpawner:
                        break;
                    case ItemType.GrassSpawner:
                    case ItemType.CarrotSpawner:
                    case ItemType.Chicken:
                    case ItemType.MrBiscuits:
                    case ItemType.MrBiscuits2:
                    case ItemType.MrBiscuits3:
                    case ItemType.MrBiscuits4:
                    case ItemType.BatSpawner:
                        if (!FeedRules.CanBeFed(selectedInteractable, selectedItem))
                        {
                            break;
                        }
                        selectedInteractableTransform.parent.SendMessage("OnFedItem", selectedItem, SendMessageOptions.DontRequireReceiver);
                        SetSelectedItem(ItemType.None);
                        break;
                }
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

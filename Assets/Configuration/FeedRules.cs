using System.Collections.Generic;

public static class FeedRules {
    private static Dictionary<InventorySlot.ItemType, List<InventorySlot.ItemType>> Rules;

    static FeedRules()
    {
        Rules = new Dictionary<InventorySlot.ItemType, List<InventorySlot.ItemType>>();
        Rules.Add(InventorySlot.ItemType.Carrot, new List<InventorySlot.ItemType>());
        Rules.Add(InventorySlot.ItemType.CarrotSpawner, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Water
        });
        Rules.Add(InventorySlot.ItemType.Chicken, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Grass,
            InventorySlot.ItemType.Carrot
        });
        Rules.Add(InventorySlot.ItemType.Egg, new List<InventorySlot.ItemType>());
        Rules.Add(InventorySlot.ItemType.Grass, new List<InventorySlot.ItemType>());
        Rules.Add(InventorySlot.ItemType.GrassSpawner, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Water
        });
        Rules.Add(InventorySlot.ItemType.MrBiscuits, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Water
        });
        Rules.Add(InventorySlot.ItemType.Water, new List<InventorySlot.ItemType>());
        Rules.Add(InventorySlot.ItemType.WaterSpawner, new List<InventorySlot.ItemType>());
        Rules.Add(InventorySlot.ItemType.None, new List<InventorySlot.ItemType>());
        Rules.Add(InventorySlot.ItemType.MrBiscuits2, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Water,
            InventorySlot.ItemType.Grass,
            InventorySlot.ItemType.Carrot,
            InventorySlot.ItemType.Egg
        });
        Rules.Add(InventorySlot.ItemType.MrBiscuits3, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Grass,
            InventorySlot.ItemType.Carrot,
            InventorySlot.ItemType.Egg,
            InventorySlot.ItemType.Chicken
        });
        Rules.Add(InventorySlot.ItemType.MrBiscuits4, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Water,
            InventorySlot.ItemType.Grass,
            InventorySlot.ItemType.Carrot,
            InventorySlot.ItemType.Egg,
            InventorySlot.ItemType.Chicken
        });
        Rules.Add(InventorySlot.ItemType.BatSpawner, new List<InventorySlot.ItemType> {
            InventorySlot.ItemType.Egg,
            InventorySlot.ItemType.Chicken
        });
    }

    public static bool CanBeFed(InventorySlot.ItemType target, InventorySlot.ItemType food)
    {
        return Rules[target].Contains(food);
    }
}

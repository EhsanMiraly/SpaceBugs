using System.Collections.Generic;
using UnityEngine;

public class InventoryShopData
{
    public static InventoryShopItem[] InventoryShopItems = new InventoryShopItem[]
    {
        new InventoryShopItem(false, "Health", 1),
        new InventoryShopItem(false, "Bullet", 1)
    };


    public static void FillInventoryShopData(InventoryShopItem[] inventoryShopItems)
    {
        InventoryShopItems = new InventoryShopItem[inventoryShopItems.Length];

        for (int i = 0; i < inventoryShopItems.Length; i++)
        {
            InventoryShopItems[i].IsBought = inventoryShopItems[i].IsBought;
            InventoryShopItems[i].ItemName = inventoryShopItems[i].ItemName;
            InventoryShopItems[i].Price = inventoryShopItems[i].Price;
        }
    }

}

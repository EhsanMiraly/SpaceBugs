using System.Collections.Generic;
using UnityEngine;

public class InventoryShopData
{
    public static InventoryShopItem[] InventoryShopItems = new InventoryShopItem[]
    {
        new InventoryShopItem(false, new string[]{"Health","جان"}, 1),
        new InventoryShopItem(false, new string[]{"Bullet","تیر"}, 1),
        new InventoryShopItem(false, new string[]{"Bullet","تیر"}, 1)
    };


    public static void FillInventoryShopData(InventoryShopItem[] inventoryShopItems)
    {
        if (InventoryShopItems.Length <= inventoryShopItems.Length)
        {
            InventoryShopItems = new InventoryShopItem[inventoryShopItems.Length];
        }

        for (int i = 0; i < inventoryShopItems.Length; i++)
        {
            InventoryShopItems[i].IsBought = inventoryShopItems[i].IsBought;
            InventoryShopItems[i].ItemName = new string[inventoryShopItems[i].ItemName.Length];
            for (int j = 0; j < inventoryShopItems[i].ItemName.Length; j++)
            {
                InventoryShopItems[i].ItemName[j] = inventoryShopItems[i].ItemName[j];
            }
            InventoryShopItems[i].Price = inventoryShopItems[i].Price;
        }
    }

}

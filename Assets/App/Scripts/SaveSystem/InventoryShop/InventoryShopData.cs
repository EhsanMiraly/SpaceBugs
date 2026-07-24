using System.Collections.Generic;
using UnityEngine;

public class InventoryShopData
{
    public static InventoryShopItem[] InventoryShopItems = new InventoryShopItem[]
    {
        new InventoryShopItem(false, new string[]{"Bullet","تیر"}, 10),
        new InventoryShopItem(false, new string[]{"Bullet","تیر"}, 20),
        new InventoryShopItem(false, new string[]{"Bullet","تیر"}, 30),
        new InventoryShopItem(false, new string[]{"Bullet","تیر"}, 40),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 10),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 20),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 30),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 40),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 50),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 60),
        new InventoryShopItem(false, new string[]{"Health","جان"}, 70),
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

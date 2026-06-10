using System.IO;
using UnityEngine;


public class InventoryShop_SaveSystem : MonoBehaviour
{
    private static string saveDirectory;
    private static InventoryShopItems_SaveData inventoryShopItems_SaveData = new InventoryShopItems_SaveData();


    private static void CreateSaveDirectory()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public static string Levels_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/InventoryShopItems_SaveData" + ".txt";
    }

    public static void Save_InventoryShopItems()
    {
        inventoryShopItems_SaveData.InventoryShopItems = InventoryShopData.InventoryShopItems;

        File.WriteAllText(Levels_SaveFileName(), JsonUtility.ToJson(inventoryShopItems_SaveData, true));
    }

    public static void Load_InventoryShopItems()
    {
        if (!File.Exists(Levels_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Levels_SaveFileName());

        inventoryShopItems_SaveData = JsonUtility.FromJson<InventoryShopItems_SaveData>(saveContent);

        InventoryShopData.FillInventoryShopData(inventoryShopItems_SaveData.InventoryShopItems);
    }
}


[System.Serializable]
public struct InventoryShopItems_SaveData
{
    public InventoryShopItem[] InventoryShopItems;
}

[System.Serializable]
public struct InventoryShopItem
{
    public bool IsBought;
    public string[] ItemName;
    public int Price;

    public InventoryShopItem(bool isBought, string[] itemName, int price)
    {
        IsBought = isBought;
        ItemName = new string[itemName.Length];
        for (int i = 0; i < itemName.Length; i++)
        {
            ItemName[i] = itemName[i];
        }
        Price = price;
    }

}
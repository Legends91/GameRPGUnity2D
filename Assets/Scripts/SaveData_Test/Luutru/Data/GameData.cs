using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int deathCount;
    public int ngay;
    public bool isInventorySystemInitialized;
    public Vector3 targetPosition;

    public List<string> collectedItems;

    public SerializableDictionary<string, bool> coinsCollected;
    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public SerializableDictionary<string, InventorySaveData> chestDictionary;
    public SerializableDictionary<string, ShopSaveData> _shopKeeperDictionary;
    public InventorySaveData playerInventory;
    // public AttributesData playerAttributesData;

    public GameData() 
    {
        this.deathCount = 0;

        collectedItems = new List<string>();
        coinsCollected = new SerializableDictionary<string, bool>();
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        _shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
        playerInventory = new InventorySaveData();
   //     playerAttributesData = new AttributesData();
    }
    public void AddCollectedItem(string id)
    {
        if (!collectedItems.Contains(id))
        {
            collectedItems.Add(id);
        }
    }  
    public void RemoveCollectedItem(string id)
    {
           activeItems.Remove(id);
    }

    public int GetPercentageComplete() 
    {
        int percentageCompleted = 0;
        if (ngay != 0) 
        {
            percentageCompleted = (ngay * 100 / 10);
        }
        return percentageCompleted;
    }
}

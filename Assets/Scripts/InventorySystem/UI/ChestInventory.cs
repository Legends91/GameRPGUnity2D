using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable, IDataPersistence
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        QuanliLuutru.instance.LoadGame();
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventorySystem, 0);
        interactSuccessful = true;
    }
    public void EndInteraction()
    {
    }

    public void LoadData(GameData data)
    {
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.inventorySystem = chestData.InvSystem;
            this.transform.position = chestData.Position;
            this.transform.rotation = chestData.Rotation;
        }
    }

    public void SaveData(GameData data)
    {
        var chestSaveData = new InventorySaveData(inventorySystem, transform.position, transform.rotation);
        
        if (data.chestDictionary.ContainsKey(GetComponent<UniqueID>().ID))
        {
            data.chestDictionary.Remove(GetComponent<UniqueID>().ID);
        }
        data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }
}


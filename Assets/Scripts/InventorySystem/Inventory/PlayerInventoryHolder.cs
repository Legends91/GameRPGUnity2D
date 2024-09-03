using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInventoryHolder : InventoryHolder, IDataPersistence
{
    public static UnityAction OnPlayerInventoryChanged;
    public InventoryUIController InventoryUIController;

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;
    [SerializeField] private bool isInventorySystemInitialized = false;
    [SerializeField] private bool isInventoryOpen = false;
    [Header("------ Hiệu ứng âm thanh ------")]
    [SerializeField] public AudioSource pickupSFX;

    private void Start()
    {
        QuanliLuutru.instance.LoadGame();
        var gameData = QuanliLuutru.instance.GetGameData();
        gameData.playerInventory = new InventorySaveData(gameData.playerInventory.InvSystem);
    }
    public void OpenInventory(InputAction.CallbackContext Inven)
    {
        if (Inven.performed)
        {
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen)
            {
                OnPlayerInventoryDisplayRequested?.Invoke(inventorySystem, offset);
            }
            else
            {
                InventoryUIController.CloseInventory();
            }
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount, bool spawnItemOnFail = false)
    {
        if (inventorySystem.AddToInventory(data, amount))
        {
            pickupSFX.Play();
            OnPlayerInventoryChanged?.Invoke();
            return true;
        }
        var transform1 = transform;
       if (spawnItemOnFail) Instantiate(data.ItemPrefab,transform1.position + transform1.forward * 2f,Quaternion.identity);
        
        return false;
    }
    public void DropItemFromInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseItemData mouseItemData = FindObjectOfType<MouseItemData>();
            if (mouseItemData != null && mouseItemData.AssignedInventorySlot.ItemData != null)
            {
                mouseItemData.DropItem();
            }
        }
    }
    public void LoadData(GameData data)
    {/*
        data.playerInventory = new InventorySaveData(this.inventorySystem);
     //   data.isInventorySystemInitialized = this.isInventorySystemInitialized;
        if (data.playerInventory.InvSystem != null)
         {
            this.inventorySystem = data.playerInventory.InvSystem;

            OnPlayerInventoryChanged?.Invoke();
            data.playerInventory = new InventorySaveData(this.inventorySystem);
        }
        */
    }

    public void SaveData(GameData data)
    {
        data.playerInventory.InvSystem = this.inventorySystem;
    }
}

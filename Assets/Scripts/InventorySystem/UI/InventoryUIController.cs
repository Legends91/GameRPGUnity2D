using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InventoryUIController : MonoBehaviour
{
    [FormerlySerializedAs("chestPanel")] public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;
    [SerializeField] private CraftingDisplay _cratingWindowParent;
    [SerializeField] private ShopKeeperDisplay shopKeeperDisplay;
    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
        _cratingWindowParent.gameObject.SetActive(false);
        shopKeeperDisplay.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
        CraftingBench.OnCraftingDisplayRequest += DisplayCraftingWindow;
        ShopKeeper.OnShopWindowRequested += DisplayShopKeeper;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
        CraftingBench.OnCraftingDisplayRequest -= DisplayCraftingWindow; 
        ShopKeeper.OnShopWindowRequested -= DisplayShopKeeper;
    }

    void Update()
    {   
    }

    public void Close(InputAction.CallbackContext close)
    {
        if (inventoryPanel.gameObject.activeInHierarchy && close.performed)//Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseChest();
        }
        if (playerBackpackPanel.gameObject.activeInHierarchy && close.performed)//Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseInventory();
        }
        if (_cratingWindowParent.gameObject.activeInHierarchy && close.performed)//Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseCrafting();
        }
        if (shopKeeperDisplay.gameObject.activeInHierarchy && close.performed)//Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseShop();
        }
    }
    public void CloseChest()
    {
        inventoryPanel.gameObject.SetActive(false);
    }

    public void CloseInventory()
    {
        playerBackpackPanel.gameObject.SetActive(false);
    }

    public void CloseCrafting()
    {
        _cratingWindowParent.gameObject.SetActive(false);
    }

    public void CloseShop()
    {
        shopKeeperDisplay.gameObject.SetActive(false);
    }
    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
    void DisplayPlayerInventory(InventorySystem invToDisplay, int offset)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
    private void DisplayCraftingWindow(CraftingBench craftingBench)
    {
        _cratingWindowParent.gameObject.SetActive(true);
        _cratingWindowParent.DisplayCratingWindow(craftingBench);
    }
    private void DisplayShopKeeper(ShopSystem shopSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        shopKeeperDisplay.gameObject.SetActive(true);
        shopKeeperDisplay.DisplayShopWindow(shopSystem, playerInventoryHolder);
    }
}

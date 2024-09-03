using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable, IDataPersistence
{
    [SerializeField] private ShopItemList _shopItemHelds;
    [SerializeField] private ShopSystem _shopSystem;

    private ShopSaveData _shopSaveData;
    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;
    private string id;
    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopItemHelds.Items.Count,_shopItemHelds.MaxAllowedGold,
            _shopItemHelds.BuyMarkUp, _shopItemHelds.SellMarkUp);
        foreach (var item in _shopItemHelds.Items)
        {
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }
        id = GetComponent<UniqueID>().ID;
        _shopSaveData = new ShopSaveData(_shopSystem);
    }
    public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>();
        if (playerInv != null)
        {
            OnShopWindowRequested?.Invoke(_shopSystem, playerInv);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
        }
    }
    public void EndInteraction()
    {

    }

    public void LoadData(GameData data)
    {
       // if (!data._shopKeeperDictionary.ContainsKey(id)) data._shopKeeperDictionary.Add(id, _shopSaveData);
        if (!data._shopKeeperDictionary.TryGetValue(id, out ShopSaveData shopSaveData)) return;
        _shopSaveData = shopSaveData;
        _shopSystem = _shopSaveData.ShopSystem;
    }

    public void SaveData(GameData data)
    {
        if (!data._shopKeeperDictionary.ContainsKey(id)) data._shopKeeperDictionary.Add(id, _shopSaveData);
        else data._shopKeeperDictionary.Remove(id);
    }
}
[System.Serializable]
public class ShopSaveData
{
    public ShopSystem ShopSystem;
    public ShopSaveData(ShopSystem shopSystem)
    {
        ShopSystem = shopSystem;
    }
}

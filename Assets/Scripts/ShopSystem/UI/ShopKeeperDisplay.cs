using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperDisplay : MonoBehaviour
{
    [SerializeField] private ShopSlotUI _shopSlotPrefab;
    [SerializeField] private ShoppingCartItemUI _shoppingCartItemPrefab;

    [SerializeField] private Button _buyTab;
    [SerializeField] private Button _sellTab;

    [Header("Danh sách vật phẩm")]
    [SerializeField] private TextMeshProUGUI _basketTotalText;
    [SerializeField] private TextMeshProUGUI _playerGoldText;
    [SerializeField] private TextMeshProUGUI _shopGoldText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _buyButtonText;

    [Header("Thông tin vật phẩm")]
    [SerializeField] private Image _itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;
        
    [SerializeField] private GameObject _itemListContentPanel;
    [SerializeField] private GameObject _shoppingCartContentPanel;

    private int basketTotal;

    private ShopSystem _shopSystem;
    private PlayerInventoryHolder _playerInventoryHolder;

    private Dictionary<InventoryItemData,int> _shoppingCart = new Dictionary<InventoryItemData,int>();
    private Dictionary<InventoryItemData, ShoppingCartItemUI> _shoppingCartUI = new Dictionary<InventoryItemData, ShoppingCartItemUI>();
    private bool isSelling;

    public void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        _shopSystem = shopSystem;
        _playerInventoryHolder = playerInventoryHolder;

        RefreshDisplay(); 
    }

    private void RefreshDisplay()
    {
        if (_buyButton != null)
        {
            _buyButtonText.text = isSelling ? "Sell" : "Buy";
            _buyButton.onClick.RemoveAllListeners();
            if (isSelling) _buyButton.onClick.AddListener(SellItems);
            else _buyButton.onClick.AddListener(BuyItems);
        }
        ClearItemPreview();
        ClearSlots();

        _basketTotalText.enabled = false;
        _buyButton.gameObject.SetActive(false);
        basketTotal = 0;
        _playerGoldText.text = $"Player Gold: {_playerInventoryHolder.InventorySystem.Gold}";
        _shopGoldText.text = $"Shop Gold: {_shopSystem.AvailableGold}";
        if (isSelling) DisplayerPlayerInventory();
        else DisplayShopInventory();
    }
    private void BuyItems()
    {
        if (_playerInventoryHolder.InventorySystem.Gold < basketTotal) return;
        if (!_playerInventoryHolder.InventorySystem.CheckInventoryRemaining(_shoppingCart)) return;
        foreach (var kvp in _shoppingCart)
        {
            _shopSystem.PurschaseItem(kvp.Key, kvp.Value);
            for (int i = 0; i < kvp.Value; i++)
            {
                _playerInventoryHolder.InventorySystem.AddToInventory(kvp.Key, 1);
            }
        }
        _shopSystem.GainGold(basketTotal);
        _playerInventoryHolder.InventorySystem.SpendGold(basketTotal);
        RefreshDisplay();
    }
    private void SellItems()
    {
        if (_shopSystem.AvailableGold < basketTotal) return;
        foreach (var kvp in _shoppingCart)
        {
            var price = GetModifiedPrice(kvp.Key, kvp.Value, _shopSystem.SellMarkUp);
            _shopSystem.SellItem(kvp.Key, kvp.Value, price);

            _playerInventoryHolder.InventorySystem.GainGold(price);
            _playerInventoryHolder.InventorySystem.RemoveItemsFromInventory(kvp.Key, kvp.Value);
        }
        RefreshDisplay();
    }

    private void ClearSlots()
    {
        _shoppingCart = new Dictionary<InventoryItemData, int>();
        _shoppingCartUI = new Dictionary<InventoryItemData, ShoppingCartItemUI>();

        foreach (var item in _itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in _shoppingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void DisplayShopInventory()
    {
        foreach (var item in _shopSystem.ShopInventory)
        {
            if (item.ItemData == null) continue;
            var shopSlot = Instantiate(_shopSlotPrefab, _itemListContentPanel.transform);
            shopSlot.Init(item, _shopSystem.BuyMarkUp);
        }
    }

    private void DisplayerPlayerInventory()
    {
        foreach (var item in _playerInventoryHolder.InventorySystem.GetAllItemsHold())
        {
            var tempSlot = new ShopSlot();
            tempSlot.AssignItem(item.Key, item.Value);

            var shopSlot = Instantiate(_shopSlotPrefab, _itemListContentPanel.transform);
            shopSlot.Init(tempSlot, _shopSystem.SellMarkUp);
        }
    }
    public void RemoveItemFromCart(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;
        var price = GetModifiedPrice(data, 1, shopSlotUI.MarkUp);

        if (_shoppingCart.ContainsKey(data))
        {
            _shoppingCart[data]--;
            var newString = $"{data.TenVp} ({price}G) x{_shoppingCart[data]}";
            _shoppingCartUI[data].SetItemText(newString);

            if (_shoppingCart[data] <= 0)
            {
                _shoppingCart.Remove(data);
                var tempObj = _shoppingCartUI[data].gameObject;
                _shoppingCartUI.Remove(data);
                Destroy(tempObj);
            }
        }

       basketTotal -= price;
       _basketTotalText.text = $"{basketTotal}G";
        if (basketTotal <= 0 && _basketTotalText.IsActive())
        {
            _basketTotalText.enabled = false ;
            _buyButton.gameObject.SetActive(false);
            ClearItemPreview();
            return;
        }
        CheckCartVsAvailableGold();
    }
    private void ClearItemPreview()
    {
        _itemPreviewSprite.sprite = null;
        _itemPreviewSprite.color = Color.clear;
        _itemPreviewName.text = "";
        _itemPreviewDescription.text = "";
    }

    public void AddItemToCart(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;
        UpdateItemPreview(shopSlotUI);
        var price = GetModifiedPrice(data, 1, shopSlotUI.MarkUp);
        if (_shoppingCart.ContainsKey(data))
        {
            _shoppingCart[data]++;
            var newString = $"{data.TenVp} ({price}G) x{_shoppingCart[data]}";
            _shoppingCartUI[data].SetItemText(newString);
        }
        else
        {
            _shoppingCart.Add(data, 1);

            var shoppingCartTextObj = Instantiate(_shoppingCartItemPrefab, _shoppingCartContentPanel.transform);
            var newString = $"{data.TenVp} ({price}G) x1";
            shoppingCartTextObj.SetItemText(newString);
            _shoppingCartUI.Add(data, shoppingCartTextObj);
        }
        basketTotal += price;
        _basketTotalText.text = $"{basketTotal}G";

        if (basketTotal > 0 && !_basketTotalText.IsActive())
        {
            _basketTotalText.enabled = true;
            _buyButton.gameObject.SetActive(true);
        }
        CheckCartVsAvailableGold();
    }

    private void CheckCartVsAvailableGold()
    {
        var goldToCheck = isSelling ? _shopSystem.AvailableGold : _playerInventoryHolder.InventorySystem.Gold;
        _basketTotalText.color = basketTotal > goldToCheck ? Color.red : Color.white;

        if (isSelling || _playerInventoryHolder.InventorySystem.CheckInventoryRemaining(_shoppingCart)) return;
        _basketTotalText.text = "Not enough room in inventory";
        _basketTotalText.color = Color.red;
    }

    public static int GetModifiedPrice(InventoryItemData data, int amount, float markUp)
    {
        var baseValue = data.GoldValue * amount;
        return Mathf.RoundToInt(baseValue + baseValue * markUp);
    }
    public void UpdateItemPreview(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;

        _itemPreviewSprite.sprite = data.Icon;
        _itemPreviewSprite.color = Color.white;
        _itemPreviewName.text = data.TenVp;
        _itemPreviewDescription.text = data.Mota;
    }
    public void OnBuyTabPressed()
    {
        isSelling = false;
        RefreshDisplay();
    }
    public void OnSellTabPressed()
    {
        isSelling = true;
        RefreshDisplay();
    }
    public void SetColor()
    {
        Color textColor;
        if (ColorUtility.TryParseHtmlString("#86661A", out textColor))
        {
            _itemPreviewDescription.color = textColor;
        }
    }
}

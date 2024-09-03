using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private ShopSlot _assignItemSlot;

    public ShopSlot AssignedItemSlot => _assignItemSlot;

    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;

    public int _tempAmount;
    public ShopKeeperDisplay ParentDisplay { get; private set; }
    public float MarkUp {  get; private set; }  

    private void Awake()
    {
        _itemSprite.sprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;
        _itemName.text = "";
        _itemCount.text = "";

        _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        ParentDisplay = transform.parent.GetComponentInParent<ShopKeeperDisplay>();
    }
    public void Init(ShopSlot slot, float markUp)
    {
        _assignItemSlot = slot;
        MarkUp = markUp;
        _tempAmount = slot.StackSize;
        UpdateUISlot();
    }
    private void UpdateUISlot()
    {
        if (_assignItemSlot != null)
        {
            _itemSprite.sprite = _assignItemSlot.ItemData.Icon;
            _itemSprite.color = Color.white;
            _itemCount.text = $"x{_assignItemSlot.StackSize.ToString()}";
            var modifiedPrice = ShopKeeperDisplay.GetModifiedPrice(_assignItemSlot.ItemData, 1, MarkUp);
            _itemName.text = $"{_assignItemSlot.ItemData.TenVp} - {modifiedPrice}G";
        }
        else
        {
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;
            _itemName.text = "";
            _itemCount.text = "";
        }
    }
    private void RemoveItemFromCart()
    {
        if (_tempAmount == _assignItemSlot.StackSize) return;

        _tempAmount++;
        ParentDisplay.RemoveItemFromCart(this);
        _itemCount.text = $"x{_tempAmount.ToString()}";
    }

    private void AddItemToCart()
    {
        if (_tempAmount <= 0) return;
            _tempAmount--;
            ParentDisplay.AddItemToCart(this);
            _itemCount.text = $"x{_tempAmount.ToString()}";
    }

}

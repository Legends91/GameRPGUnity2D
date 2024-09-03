using UnityEngine;

[System.Serializable]
public class QLItem
{
    [SerializeField] public InventoryItemData item = null;
    [SerializeField] public int quantity = 0;

    public QLItem()
    {
        item = null;
        quantity = 0;
    }

    public QLItem(InventoryItemData _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
    }

    public QLItem(QLItem slot)
    {
        this.item = slot.GetItem();
        this.quantity = slot.GetQuantity();
    }

    public void Clear()
    {
        this.item = null;
        this.quantity = 0;
    }

    public InventoryItemData GetItem() { return item; }
    public int GetQuantity() { return quantity; }
    public void AddQuantity(int _quantity) { quantity += _quantity; }
    public void SubQuantity(int _quantity) { quantity -= _quantity; }
    public void AddItem(InventoryItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

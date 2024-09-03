using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private int _gold;

    public int Gold => _gold;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;
    public UnityAction<InventorySlot> OnInventorySlotChanged;
    public InventorySystem(int size)
    {
        _gold = 0;
        CreateInventory(size);
    }
    public InventorySystem(int size, int gold)
    {
        _gold = gold;
        CreateInventory(size);
    }

    private void CreateInventory(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ConstainItem(itemToAdd, out List<InventorySlot> invSlot)) //Kiểm tra xem trong kho đồ có vật phẩm không
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        if (HasFreeSlot(out InventorySlot freeSlot)) //Lấy vị trí đầu tiên còn trống trong kho đồ.
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ConstainItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }

    public void RemoveItemsFromInventory(InventoryItemData data, int amount)
    {
        if (ConstainItem(data, out List<InventorySlot> invSlot))
        {
            foreach(var slot in invSlot)
            {
                var stackSize = slot.StackSize;
                if (stackSize > amount) slot.RemoveFromStack(amount);
                else
                {
                    slot.RemoveFromStack(stackSize);
                    amount -= stackSize;
                }
                OnInventorySlotChanged?.Invoke(slot);
            }
        }
    }
    public bool CheckInventoryRemaining(Dictionary<InventoryItemData, int> shoppingCart)
    {
        var clonedSystem = new InventorySystem(this.InventorySize);

        for (int i = 0; i < InventorySize; i++)
        {
            clonedSystem.InventorySlots[i].AssignItem(this.InventorySlots[i].ItemData, this.InventorySlots[i].StackSize);
        }

        foreach (var kvp in shoppingCart)
        {
            for (int i = 0;i < kvp.Value; i++)
            {
                if (!clonedSystem.AddToInventory(kvp.Key, 1)) return false;
            }
        }
        return true;
    }
    public void SpendGold(int basketTotal)
    {
        _gold -= basketTotal;
    }
    public void GainGold(int price)
    {
        _gold += price;
    }

    public Dictionary<InventoryItemData, int> GetAllItemsHold()
    {
        var distincItems = new Dictionary<InventoryItemData, int>();
        foreach (var item in inventorySlots)
        {
            if (item.ItemData == null) continue;
            if (!distincItems.ContainsKey(item.ItemData))
            {
                distincItems.Add(item.ItemData, item.StackSize);
            }
            else
            {
                distincItems[item.ItemData] += item.StackSize;
            }
        }
        return distincItems;
    }
}

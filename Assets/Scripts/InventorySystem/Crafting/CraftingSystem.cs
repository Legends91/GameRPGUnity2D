using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private List<CraftingSlot> inventorySlots;
    public List<CraftingSlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;
    public UnityAction<CraftingSlot> OnInventorySlotChanged;
    public CraftingSystem(int size)
    {
        inventorySlots = new List<CraftingSlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new CraftingSlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ConstainItem(itemToAdd, out List<CraftingSlot> invSlot)) //Kiểm tra xem trong kho đồ có vật phẩm không
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
        if (HasFreeSlot(out CraftingSlot freeSlot)) //Lấy vị trí đầu tiên còn trống trong kho đồ.
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ConstainItem(InventoryItemData itemToAdd, out List<CraftingSlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out CraftingSlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}

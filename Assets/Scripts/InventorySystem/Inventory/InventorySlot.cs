using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot: ItemSlot
{
    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        _itemID = itemData.ID;
        stackSize = amount ;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return RoomLeftInStack(amountToAdd);
    }
    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }


    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        _itemID = itemData.ID;
        stackSize = amount;
    }
  /*  public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }
    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null
    }
  */
    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }
        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }


    // Crafting
    /*  public QLItem FindItem(InventoryItemData item)
      {
          for (int i = 0; i < stackSize; i++)
          {
              if (items[i].GetItem() == item)
                  return items[i];
          }
          return null;
      }

      public bool Contains(InventoryItemData item, int quantity)
      {
          for (int i = 0; i < stackSize; i++)
          {
              if (items[i].GetItem() == item && items[i].GetQuantity() >= quantity)
                  return true;
          }
          return false;
      }


      public bool Remove(InventoryItemData item, int quantity)
      {
          QLItem temp = FindItem(item); // Sử dụng FindItem thay vì Contains
          if (temp != null)
          {
              if (temp.GetQuantity() > quantity)
              {
                  temp.SubQuantity(quantity); // Đảm bảo SubQuantity chấp nhận int
              }
              else
              {
                  int slotToRemoveIndex = -1;
                  for (int i = 0; i < stackSize; i++)
                  {
                      if (items[i].GetItem() == item)
                      {
                          slotToRemoveIndex = i;
                          break;
                      }
                  }
                  if (slotToRemoveIndex != -1)
                  {
                      items[slotToRemoveIndex].Clear();
                  }
              }
          }
          else
          {
              return false;
          }
          return true;
      } */
}

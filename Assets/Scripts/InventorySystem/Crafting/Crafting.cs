using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private CraftingSlot assignedCraftingSlot;
    [SerializeField] private CraftingRecipe recipe;
    [SerializeField] private StaticInventoryDisplay inventoryDisplay;

    [SerializeField] private Button button;
    public CraftingSlot AssignedCraftingSlot => assignedCraftingSlot;
    public InventoryDisplay ParentDisplay { get; private set; }
    private void Awake()
    {
        //ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISLotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(CraftingSlot slot)
    {
        assignedCraftingSlot = slot;
        UpdateUISlotCraft(slot);
    }

    public void UpdateUISlotCraft(CraftingSlot slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;
            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            itemSprite.color = Color.clear;
            itemSprite.sprite = null;
            itemCount.text = "";
        }
    }

    public void UpdateUISlotCraft()
    {
        if (assignedCraftingSlot != null) UpdateUISlotCraft(assignedCraftingSlot);
    }

    /*public void ClearSlot()
    {
        assignedCraftingSlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }  */


   
    public void OnUISLotClick()
    {
        /*
         for (int i = 0; i < recipe.inputItems.Length; i++)
       {
          Inventory.Remove(recipe.inputItems[i].GetItem(), recipe.inputItems[i].GetQuantity());
       }
        */

        // inventory.AddToStack(output.GetItem(), output.GetQuantity()); 
    }
}

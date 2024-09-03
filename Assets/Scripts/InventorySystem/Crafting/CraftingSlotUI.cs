using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    public InventoryItemData AssignedData { get; private set; }
    public void Init(InventoryItemData data, int amount)
    {
        AssignedData = data;
        itemSprite.preserveAspect = true;
        itemSprite.sprite = data.Icon;
        itemSprite.color = Color.white;
        UpdateRequiredAmount(amount);
    }

    public void UpdateRequiredAmount(int amount, bool playerHasRequireItem = true)
    {
        itemCount.text = amount.ToString();
        if (!playerHasRequireItem) { itemCount.color = Color.red; }
    }
}

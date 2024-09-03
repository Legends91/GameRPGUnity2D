using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Công thức chế tạo:")]
    //  public QLItem[] inputItems;
    //  public QLItem output;
    [SerializeField] private InventoryItemData _craftedItem;
    [SerializeField] private int _craftAmount = 1;
    [SerializeField] private List<NguyenlieuChetao> _nguyenlieu;
    public List<NguyenlieuChetao> Nguyenlieu => _nguyenlieu;
    public InventoryItemData CraftedItem => _craftedItem;
    public int CraftedAmount => _craftAmount;

}
[System.Serializable]
public struct NguyenlieuChetao
{
    public InventoryItemData ItemRequire;
    public int AmountRequire;

    public NguyenlieuChetao(InventoryItemData itemRequire, int amountRequire)
    {
        ItemRequire = itemRequire;
        AmountRequire = amountRequire;
    }
}
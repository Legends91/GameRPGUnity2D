using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public ItemType.LoaiItem LoaiVp;
    public int ID = -1;
    public string TenVp;
    [TextArea(4, 4)]
    public string Mota;
    public Sprite Icon;
    public GameObject ItemPrefab;
    public int MaxStackSize;
    public int GoldValue;

    public bool stackable;
    public bool isSeed; 
    public bool isWeapon;

    public void UseItem()
    {

    }
}

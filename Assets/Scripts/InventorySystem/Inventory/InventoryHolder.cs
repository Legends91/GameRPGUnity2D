using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected int offset = 9;
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;
    [SerializeField] protected int _gold;

    public int Offset => offset;
    public InventorySystem InventorySystem => inventorySystem;
    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize, _gold);
    }

}

[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InvSystem;
    public Vector3 Position;
    public Quaternion Rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
    {
        InvSystem = _invSystem;
        Position = _position;
        Rotation = _rotation;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        InvSystem = _invSystem;
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
    }
    public bool IsValid()
    {
        return InvSystem != null;
    }
}
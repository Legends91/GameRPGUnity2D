using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;
    public float dropAreaSize = 1.0f;
    public float dropForce = 5.0f;
    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;

        if (invSlot.StackSize > 1)
            ItemCount.text = invSlot.StackSize.ToString();
        else
            ItemCount.text = "";

        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                DropItem();
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    public void DropItem()
    {
            GameObject player = GameObject.FindWithTag("Player");

        Vector3 dropPosition = player.transform.position;
        dropPosition.x += dropAreaSize * UnityEngine.Random.value - dropAreaSize / 2;
        dropPosition.y += dropAreaSize * UnityEngine.Random.value - dropAreaSize / 2;

        GameObject droppedItem = Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, dropPosition, Quaternion.identity);

        // Sử dụng ItemPickUp để xử lý vật phẩm được thả
        ItemPickUp itemEntity = droppedItem.GetComponent<ItemPickUp>();
        itemEntity.Initialize(AssignedInventorySlot.ItemData, 1);

        // Giảm số lượng vật phẩm trong chuột
        AssignedInventorySlot.RemoveFromStack(1);

        // Nếu không còn vật phẩm nào, xóa con trỏ chuột
        if (AssignedInventorySlot.StackSize <= 0)
        {
            ClearSlot();
        }
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

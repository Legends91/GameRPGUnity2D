using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarDisplay : StaticInventoryDisplay
{
    private int _maxIndexSize = 8;
    private int _currentIndex = 0;

    private PlayerInput playerInput;
    private PlayerWeapon playerController;


    private InventoryItemData _currentItemData;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerController = FindObjectOfType<PlayerWeapon>();
    }
    protected override void Start()
    {
        base.Start();

        _currentIndex = 0;
        _maxIndexSize = slots.Length - 1;

        slots[_currentIndex].ToggleHighlight();
        UpdateCurrentItem();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        playerInput.Enable();

        playerInput.Player.HotBar1.performed += Hotbar1;
        playerInput.Player.HotBar2.performed += Hotbar2;
        playerInput.Player.HotBar3.performed += Hotbar3;
        playerInput.Player.HotBar4.performed += Hotbar4;
        playerInput.Player.HotBar5.performed += Hotbar5;
        playerInput.Player.HotBar6.performed += Hotbar6;
        playerInput.Player.HotBar7.performed += Hotbar7;
        playerInput.Player.HotBar8.performed += Hotbar8;
        playerInput.Player.HotBar9.performed += Hotbar9;
        playerInput.Player.UseItem.performed += UseItem;
        playerInput.Player.Interact.performed += UseItem; // Gọi UseItem khi nhấn nút Interact
        playerInput.Player.Action.performed += UseItem;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        playerInput.Disable();

        playerInput.Player.HotBar1.performed -= Hotbar1;
        playerInput.Player.HotBar2.performed -= Hotbar2;
        playerInput.Player.HotBar3.performed -= Hotbar3;
        playerInput.Player.HotBar4.performed -= Hotbar4;
        playerInput.Player.HotBar5.performed -= Hotbar5;
        playerInput.Player.HotBar6.performed -= Hotbar6;
        playerInput.Player.HotBar7.performed -= Hotbar7;
        playerInput.Player.HotBar8.performed -= Hotbar8;
        playerInput.Player.HotBar9.performed -= Hotbar9;
        playerInput.Player.UseItem.performed -= UseItem;
        playerInput.Player.Interact.performed -= UseItem; // Gọi UseItem khi nhấn nút Interact
        playerInput.Player.Action.performed -= UseItem;
    }
    #region Hot Bar Selction Methods
    private void Hotbar1(InputAction.CallbackContext obj)
    {
        SetIndex(0);
    }
    private void Hotbar2(InputAction.CallbackContext obj)
    {
        SetIndex(1);
    }
    private void Hotbar3(InputAction.CallbackContext obj)
    {
        SetIndex(2);
    }
    private void Hotbar4(InputAction.CallbackContext obj)
    {
        SetIndex(3);
    }
    private void Hotbar5(InputAction.CallbackContext obj)
    {
        SetIndex(4);
    }
    private void Hotbar6(InputAction.CallbackContext obj)
    {
        SetIndex(5);
    }
    private void Hotbar7(InputAction.CallbackContext obj)
    {
        SetIndex(6);
    }
    private void Hotbar8(InputAction.CallbackContext obj)
    {
        SetIndex(7);
    }
    private void Hotbar9(InputAction.CallbackContext obj)
    {
        SetIndex(8);
    }
    #endregion
    private void Update()
    {
        if (playerInput.Player.MouseWheel.ReadValue<float>() > 0.1f) ChangeIndex(1);
        if (playerInput.Player.MouseWheel.ReadValue<float>() < -0.1f) ChangeIndex(-1);
        UpdateCurrentItem();
    }
    private void UseItem(InputAction.CallbackContext obj)
    {
        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null) slots[_currentIndex].AssignedInventorySlot.ItemData.UseItem();
    }
    private void ChangeIndex(int direction)
    {
        slots[_currentIndex].ToggleHighlight();
        _currentIndex += direction;

        if (_currentIndex > _maxIndexSize) _currentIndex = 0;
        if (_currentIndex < 0) _currentIndex = _maxIndexSize;

        slots[_currentIndex].ToggleHighlight();
        //UpdateCurrentItem();
    }

    private void SetIndex(int newIndex)
    {
        slots[_currentIndex].ToggleHighlight();
        if (newIndex < 0) _currentIndex = 0;
        if (newIndex > _maxIndexSize) newIndex = _maxIndexSize;

        _currentIndex = newIndex;

        slots[_currentIndex].ToggleHighlight();
        //UpdateCurrentItem();
    }

    private void UpdateCurrentItem()
    {
        InventoryItemData itemData = slots[_currentIndex].AssignedInventorySlot.ItemData;
        if (itemData != _currentItemData)
        {
            // Nếu itemData mới là null hoặc không phải là vũ khí, hủy vũ khí cũ
            if (itemData == null || !itemData.isWeapon)
            {
                playerController.DestroyWeapon();
            }
            else
            {
                // Nếu là vũ khí mới, khởi tạo vũ khí
                if (playerController != null)
                {
                    playerController.InstantiateItem(itemData.TenVp);
                }
            }
            _currentItemData = itemData; // Cập nhật item hiện tại
        }
    }
}

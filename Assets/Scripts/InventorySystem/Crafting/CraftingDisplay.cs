using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftingDisplay : MonoBehaviour
{
    [Header("Bảng Công thức")]
    [SerializeField] private GameObject _recipeListPanel;
    [SerializeField] private CraftListUI _craftListUI;

    [Header("Bảng Nguyên Liệu")]
    [SerializeField] private CraftingSlotUI _nguyenlieuSlotPrefab;
    [SerializeField] private Transform _nguyenlieuGrid;

    [Header("Danh sách vật phẩm")]
    [SerializeField] private Image _itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemLoai;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;

    [Header("Vật phẩm hoàn thành")]
    [SerializeField] private CraftingBench _craftingBench;
    [SerializeField] private CraftingRecipe _chosenRecipe;

    [Header("Craft")]
    [SerializeField] private Button increaseItem;
    [SerializeField] private Button decreaseItem;
    [SerializeField] private Button craftButton;
    [SerializeField] private TextMeshProUGUI _craftAmountText;
    [SerializeField] private PlayerInventoryHolder _playerInventory;

    [Header("Craft")]
    public GameObject warningPanel; // Panel cảnh báo
    public TextMeshProUGUI warningText; // Text cảnh báo

    private int _craftAmount = 1;
    private List<CraftingSlotUI> nguyenlieuUISlot = new List<CraftingSlotUI>();

    private void Awake()
    {
        craftButton.onClick.RemoveAllListeners();
        decreaseItem.onClick.RemoveAllListeners();
        increaseItem.onClick.RemoveAllListeners();
        craftButton.gameObject.SetActive(false);
        increaseItem.gameObject.SetActive(false);
        decreaseItem.gameObject.SetActive(false);
        _craftAmountText.text = "";
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();
    }

    private void ChangeCraftAmount(int amount)
    {
        if (_craftAmount <= 1 && amount < 0) return;
        _craftAmount += amount;
        _craftAmountText.text = _craftAmount.ToString();
        RefreshRecipeGrid();
        if (_craftAmount <= 1) _craftAmount = 1;
    }

    public void OnButtonClicked()
    {
        CraftItem();
    }
    public void DisplayCratingWindow(CraftingBench craftingBench)
    {
        _craftingBench = craftingBench;
        ClearItemPreview();
        RefreshListDisplay();
    }
    
    public void RefreshListDisplay()
    {
        ClearSlots(_recipeListPanel.transform);
        foreach (var recipe in _craftingBench.KnowRecipes)
        {
            var recipeSlot = Instantiate(_craftListUI, _recipeListPanel.transform);
            recipeSlot.Init(recipe, this);
        }
    }

    private void ClearSlots(Transform transformToDestroy)
    {
        foreach (var item in transformToDestroy.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
    private void ClearItemPreview()
    {
        _itemPreviewSprite.sprite = null;
        _itemPreviewSprite.color = Color.clear;
        _itemPreviewName.text = " ";
        _itemLoai.text = " ";
        _itemPreviewDescription.text = " ";
    }
    private void DisplayItemPreview(InventoryItemData data)
    {
        _itemPreviewSprite.sprite = data.Icon;
        _itemPreviewSprite.color = Color.white;
        _itemPreviewName.text = data.TenVp;
        _itemLoai.text = data.LoaiVp.ToString();
        _itemPreviewDescription.text = data.Mota;
    }
    public void UpdateChosenRecipe(CraftingRecipe recipe)
    {
        _chosenRecipe = recipe;
        DisplayItemPreview(_chosenRecipe.CraftedItem);
        RefreshRecipeWindow();
    }

    public void RefreshRecipeWindow()
    {
        ClearSlots(_nguyenlieuGrid);
        nguyenlieuUISlot = new List<CraftingSlotUI>();
        _craftAmount = 1;
        _craftAmountText.text = _craftAmount.ToString();
        if (!craftButton.IsActive())
        {
            craftButton.gameObject.SetActive(true);
            craftButton.onClick.AddListener(OnButtonClicked);
        }
        if (!increaseItem.IsActive())
        {
            increaseItem.gameObject.SetActive(true);
            increaseItem.onClick.AddListener(() => ChangeCraftAmount(1));
        }
        if (!decreaseItem.IsActive())
        {
            decreaseItem.gameObject.SetActive(true);
            decreaseItem.onClick.AddListener(() => ChangeCraftAmount(-1));
        }
        foreach (var nguyenlieu in _chosenRecipe.Nguyenlieu)
        {
            var nguyenlieuSlot = Instantiate(_nguyenlieuSlotPrefab, _nguyenlieuGrid.transform);
            nguyenlieuUISlot.Add(nguyenlieuSlot);
            nguyenlieuSlot.Init(nguyenlieu.ItemRequire, nguyenlieu.AmountRequire);
        }

    }

    private void RefreshRecipeGrid()
    {
        foreach (var nguyenlieu in _chosenRecipe.Nguyenlieu)
        {
            foreach (var slot in nguyenlieuUISlot)
            {
                if (slot.AssignedData == nguyenlieu.ItemRequire)
                {
                    slot.UpdateRequiredAmount(nguyenlieu.AmountRequire * _craftAmount);
                }
            }
        }
    }

    public void CraftItem()
    {
        if (CheckIfCanCraft())
        {
            foreach (var nguyenlieu in _chosenRecipe.Nguyenlieu)
            {
                _playerInventory.InventorySystem.RemoveItemsFromInventory(nguyenlieu.ItemRequire, nguyenlieu.AmountRequire);
            }
            RefreshRecipeWindow();
            _playerInventory.AddToInventory(_chosenRecipe.CraftedItem, _chosenRecipe.CraftedAmount, true);
        }
        else
        {
            DisplayWarning();
        }
    }
    public bool CheckIfCanCraft()
    {
        var itemsHeld = _playerInventory.InventorySystem.GetAllItemsHold();
        foreach (var nguyenlieu in _chosenRecipe.Nguyenlieu)
        {
            if (!itemsHeld.TryGetValue(nguyenlieu.ItemRequire, out int amountHeld))
            {
                return false;
            }
            if (amountHeld < nguyenlieu.AmountRequire)
            {
                return false;
            }
        }
        return true;
    }
    public void DisplayWarning()
    {
        warningText.text = "Not enough ingredients!";
        warningPanel.SetActive(true);
        Invoke("HideWarning", 3f); // Ẩn cảnh báo sau 3 giây
    }

    public void HideWarning()
    {
        warningPanel.SetActive(false);
    }
}

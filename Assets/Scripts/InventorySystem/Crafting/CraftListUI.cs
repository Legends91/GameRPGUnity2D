using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftListUI : MonoBehaviour
{
    [SerializeField] private Image _recipeSprite;
   // [SerializeField] private TextMeshProUGUI _recipeName;
    [SerializeField] private Button _button;
    private CraftingDisplay _parentDisplay;
    private CraftingRecipe _recipe;
    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }
    public void Init(CraftingRecipe recipe, CraftingDisplay parentDisplay)
    {
        _parentDisplay = parentDisplay;

        _recipe = recipe;
        _recipeSprite.sprite = _recipe.CraftedItem.Icon;
    //    _recipeName.text = _recipe.CraftedItem.TenVp;
    }

    public void OnButtonClicked()
    {
        if (_parentDisplay ==  null) return;
        _parentDisplay.UpdateChosenRecipe(_recipe);
    }
}

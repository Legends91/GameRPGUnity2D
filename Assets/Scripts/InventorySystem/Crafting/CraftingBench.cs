using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingBench : MonoBehaviour, IInteractable
{
    [SerializeField] private List<CraftingRecipe> _knowRecipes;
    [SerializeField] private PlayerInventoryHolder _playerInventory;

    public List<CraftingRecipe> KnowRecipes => _knowRecipes;
    public static UnityAction<CraftingBench> OnCraftingDisplayRequest;
    #region Interaction Interact;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnCraftingDisplayRequest?.Invoke(this);
        _playerInventory = interactor.GetComponent<PlayerInventoryHolder>();
        if (_playerInventory != null)
        {
         /*   if (CheckIfCanCraft())
            {
                foreach (var nguyenlieu in _activeRecipe.Nguyenlieu)
                {
                    _playerInventory.InventorySystem.RemoveItemsFromInventory(nguyenlieu.ItemRequire, nguyenlieu.AmountRequire);
                }
                _playerInventory.AddToInventory(_activeRecipe.CraftedItem, _activeRecipe.CraftedAmount, true);
            } */
            EndInteraction();
            interactSuccessful = true;
        }
      else
        {
            interactSuccessful = false;
        }
    }

 /*   public bool CheckIfCanCraft()
    {
        var itemsHeld = _playerInventory.InventorySystem.GetAllItemsHold();
        foreach (var nguyenlieu in _activeRecipe.Nguyenlieu)
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
    } */

    public void EndInteraction()
    {

    }
    #endregion
}

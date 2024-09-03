using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius;
    public bool IsInteracting { get; private set; }
    public InventoryUIController Controller;
    [SerializeField] private bool isChestOpen = false;
    Collider2D[] colliders;

    private void Update()
    {
        colliders = Physics2D.OverlapCircleAll(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        // Kiểm tra xem các vật tương tác có nằm trong phạm vi InteractionPoint hay không
        bool anyInteractableInRange = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            var interactable = colliders[i].GetComponent<IInteractable>();
            if (interactable != null)
            {
                anyInteractableInRange = true;
                break;
            }
        }

        // Nếu không có vật tương tác nào nằm trong phạm vi InteractionPoint khi hộp đang mở, đóng hộp lại
        if (isChestOpen && !anyInteractableInRange)
        {
            Controller.CloseChest();
            Controller.CloseCrafting();
            isChestOpen = false;
        }
    }

    public void OpenChest(InputAction.CallbackContext chest)
    {
        if (chest.performed)
        {
            isChestOpen = !isChestOpen;

            if (isChestOpen)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    var interactable = colliders[i].GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        StartInteraction(interactable);
                    }
                }
            }
            else
            {
                Controller.CloseChest();
                Controller.CloseCrafting();
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        IsInteracting = false;
    }

    private void OnDrawGizmos()
    {
        if (InteractionPoint == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionPoint.position, InteractionPointRadius);
    }
}

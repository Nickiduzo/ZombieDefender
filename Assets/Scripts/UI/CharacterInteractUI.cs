using TMPro;
using UnityEngine;

public class CharacterInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionContainer;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private CharacterInteract playerInteract;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject inventoryItem;
    [SerializeField] private Transform inventoryFill;

    private void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryInteract();
        }
    }

    private void InventoryInteract()
    {
        if(inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
    }

    private void Show(IInteractable interactable)
    {
        interactionContainer.SetActive(true);
        interactText.text = "[E] - " + interactable.GetInteractText();
    }

    private void Hide()
    {
        interactionContainer?.SetActive(false);
    }
}

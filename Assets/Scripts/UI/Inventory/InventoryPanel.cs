using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private Transform panel;
    [SerializeField] private CharacterInventory characterInventory;

    [SerializeField] private List<GameObject> inventoryStuff;

    private void Start()
    {
        inventoryStuff = new List<GameObject>();
        if(characterInventory == null)
        {
            Debug.Log("Character Inventory is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
                SwitchOff();
            }
            else
            {
                inventoryUI.SetActive(true);
                SwitchOn();
            }
        }
    }

    private void SwitchOn()
    {
        List<Item> items = characterInventory.GetInventory();
        foreach (var pattern in items)
        {
            GameObject temp = Instantiate(itemUI, panel);
            ItemUI stuff = temp.GetComponent<ItemUI>();
            stuff.SetText(pattern.Type.ToString(), pattern.Amount);
            inventoryStuff.Add(temp);
        }
    }

    private void SwitchOff()
    {
        foreach (var pattern in inventoryStuff)
        {
            Destroy(pattern);
        }
    }
}

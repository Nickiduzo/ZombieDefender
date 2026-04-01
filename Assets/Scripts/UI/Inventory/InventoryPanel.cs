using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private Transform panel;
    
    [SerializeField] private CharacterInventory characterInventory;
    [SerializeField] private CharacterMoney characterMoney;

    [SerializeField] private TextMeshProUGUI amountOfMoney;

    [SerializeField] private List<GameObject> inventoryStuff;

    private void OnEnable()
    {
        CharacterMoney.OnMoneyChanged += UpdateMoney;
    }

    private void OnDisable()
    {
        CharacterMoney.OnMoneyChanged -= UpdateMoney;
    }

    private void Start()
    {
        UpdateMoney(characterMoney.money);

        inventoryStuff = new List<GameObject>();
        if (characterInventory == null)
        {
            Debug.Log("Character Inventory is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryUI.activeSelf)
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

    private void UpdateMoney(int amount)
    {
        amountOfMoney.text = amount.ToString();
    }

    private void SwitchOn()
    {
        List<Item> items = characterInventory.GetInventory();
        foreach (var pattern in items)
        {
            GameObject temp = Instantiate(itemUI, panel);
            ItemUI stuff = temp.GetComponent<ItemUI>();
            stuff.SetText(pattern.type.ToString(), pattern.amount);
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

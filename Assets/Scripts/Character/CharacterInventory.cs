using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour, IDataPersistance
{
    private List<Item> inventory;

    private void Awake()
    {
        inventory = new List<Item>();
    }

    public void AddToInventory(Item inItem)
    {
        var existingItem = inventory.Find(item => item.type == inItem.type);
        if (existingItem != null)
        {
            existingItem.amount++;
        }
        else
        {
            inventory.Add(inItem);
        }
    }

    public void RemoveFromInventory(Item inItem)
    {
        if (inventory.Contains(inItem))
        {
            inventory.Remove(inItem);
        }
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public void LoadData(GameData data)
    {
        inventory = data.playerInventory != null 
            ? new List<Item>(data.playerInventory) 
            : new List<Item>();
    }

    public void SaveData(ref GameData data)
    {
        data.playerInventory = new List<Item>(inventory);
    }
}

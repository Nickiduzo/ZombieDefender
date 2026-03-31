using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Item> GetInventoryStuff()
    {
        CharacterInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>();
        if(inventory != null)
        {
            return inventory.GetInventory();
        }
        else
        {
            Debug.LogError("Player does not have a CharacterInventory component.");
            return new List<Item>();
        }
    }
}

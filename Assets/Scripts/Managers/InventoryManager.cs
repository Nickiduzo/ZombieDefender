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

    public List<InItem> GetInventoryStuff()
    {
        CharacterInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>();
        if(inventory != null)
        {
            return inventory.GetStuffAmount();
        }
        else
        {
            Debug.LogError("Player does not have a CharacterInventory component.");
            return new List<InItem>();
        }
    }
}

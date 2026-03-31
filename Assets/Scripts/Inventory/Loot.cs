using System;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IInteractable
{
    [SerializeField] private float distanceToLoot = 3f;

    [SerializeField] private List<Item> items;

    private Transform player;

    public int Id { get; set; }

    private void Awake()
    {
        items = new List<Item>();
    }

    public void Initialization()
    {
        Item stick = new Item(ItemType.Stick, 1);
        Item stone = new Item(ItemType.Stone, 1);

        items.Add(stick);
        items.Add(stone);

        Id = Guid.NewGuid().GetHashCode();
    }

    private void TakeLoot()
    {
        CharacterInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>();
        if(inventory != null)
        {
            foreach(Item item in items)
            {
                inventory.AddToInventory(item);
            }
        }
        Collapse(Id);
    }

    private void Collapse(int id)
    { 
        if(id == Id)
        {
            Debug.Log("Player took loot");
            Destroy(gameObject);
        }
    }

    public void Interact(Transform interactorTransform)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, interactorTransform.position);
        if(distanceToPlayer <= distanceToLoot)
        {
            TakeLoot();
        }
        else
        {
            Debug.LogWarning("I can't take it");
        }
    }

    public string GetInteractText()
    {
        return name;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

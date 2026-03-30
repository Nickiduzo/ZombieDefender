using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<InItem> stuffAmount;

    private void Awake()
    {
        if(stuffAmount is null)
        {
            stuffAmount = new List<InItem>();
        }
    }


    private void AddToInventory(InItem inItem)
    {
        if (stuffAmount is null) return;

        if(stuffAmount.Contains(inItem))
        {
            stuffAmount[stuffAmount.IndexOf(inItem)].IncreaseAmount();
        }
        else
        {
            stuffAmount.Add(inItem);
        }
    }

    private void RemoveFromInventory(InItem inItem)
    {
        if (stuffAmount is null) return;

        if (stuffAmount.Contains(inItem))
        {
            stuffAmount.Remove(inItem);
        }
    }

    public List<InItem> GetStuffAmount()
    {
        return stuffAmount;
    }
}

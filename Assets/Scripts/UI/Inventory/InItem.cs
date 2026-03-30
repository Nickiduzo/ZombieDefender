using System;
using TMPro;
using UnityEngine;

public class InItem : MonoBehaviour
{
    private string amountText;
    private float amount;

    public int Id { get; set; }

    public InItem(int amount)
    {
        this.amount = amount;
        amountText = amount.ToString();
        Id = Guid.NewGuid().GetHashCode();
    }

    public void IncreaseAmount()
    {
        amount++;
        amountText = amount.ToString();
    }

    public void DecreaseAmount()
    {
        if(amount > 0)
        {
            amount--;
        }
        else
        {
            Debug.Log("Not enought stuff");
        }
    }
}

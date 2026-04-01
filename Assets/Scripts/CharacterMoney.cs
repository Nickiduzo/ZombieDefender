using System;
using UnityEngine;

public class CharacterMoney : MonoBehaviour, IDataPersistance
{
    public static event Action<int> OnMoneyChanged;

    public int money;

    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void RemoveMoney(int amount)
    {
        if(money > amount)
        {
            money -= amount;
            OnMoneyChanged?.Invoke(money);
        }
        else
        {
            Debug.LogWarning("Not enough money to remove.");
        }
    }

    public void LoadData(GameData data)
    {
        money = data.playerMoney;
    }

    public void SaveData(ref GameData data)
    {
        data.playerMoney = money;
    }
}

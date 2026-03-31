using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerExperience;
    public int playerLevel;
    public float playerMoney;
    public List<Item> playerInventory;

    public GameData()
    {
        this.playerExperience = 0;
        this.playerLevel = 0;
        this.playerMoney = 0;
        playerInventory = new List<Item>(); 
    }
}

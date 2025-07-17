using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerExperience;
    public int playerLevel;

    public GameData()
    {
        this.playerExperience = 0;
        this.playerLevel = 0;
    }
}

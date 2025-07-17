using System;
using UnityEngine;

public class CharacterExperience : MonoBehaviour, IDataPersistance
{
    public static event Action<int> OnTakeExp;

    [SerializeField] private EXPData expData;
    [SerializeField] private CharacterExperienceUI characterExp;

    private int currentLevel;
    private float currentExp;

    private void Start()
    {
        EnemySpawner.OnRemove += ExperienceHandler;
    }

    // increase and omptimize that logic for different enemy types(bosses, enemies)
    private void ExperienceHandler(int level)
    {
        float result = 100 * (level - currentLevel); 

        OnTakeExp?.Invoke(((int)result));

        UpdateExp(result);
    }

    private void UpdateExp(float exp)
    {
        currentExp += exp;

        while (currentExp >= expData.GetAmountExpByLvl(currentLevel))
        {
            currentExp -= expData.GetAmountExpByLvl(currentLevel);
            currentLevel++;

            if (currentLevel >= expData.levels.Length)
            {
                currentLevel = expData.levels.Length;
                currentExp = 0;
                break;
            }
        }

        PlayerManager.instance.Lvl = currentLevel;
        characterExp.UpdateExpInfo(currentExp, expData.GetAmountExpByLvl(currentLevel), currentLevel);
    }

    private void OnDisable()
    {
        EnemySpawner.OnRemove -= ExperienceHandler;
    }

    public void LoadData(GameData data)
    {
        currentLevel = data.playerLevel;
        currentExp = data.playerExperience;

        UpdateExp(0);
        characterExp.UpdateExpInfo(currentExp, expData.GetAmountExpByLvl(currentLevel), currentLevel);
        PlayerManager.instance.Lvl = currentLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.playerLevel = currentLevel;
        data.playerExperience = currentExp;
    }
}

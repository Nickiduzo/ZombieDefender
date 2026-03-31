using UnityEngine;

public class MoneyManager : MonoBehaviour, IDataPersistance
{
    public static MoneyManager instance;
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

    public void AddMoney(float amount)
    {
    }

    public void DecreaseMoney(float amount)
    {
    }

    public void LoadData(GameData data)
    {
    }

    public void SaveData(ref GameData data)
    {
    }
}

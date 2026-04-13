using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private List<QuestState> activeQuests = new List<QuestState>();

    public event Action OnQuestListChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<QuestState> GetQuests()
    {
        return activeQuests;
    }

    public void AddQuest(QuestData quest)
    {
        if(activeQuests.Exists(q => q.data == quest))
        {
            Debug.LogWarning("Quest already active: " + quest.title);
            return;
        }

        activeQuests.Add(new QuestState(quest));

        Debug.Log("Added quest: " + quest.title);

        OnQuestListChanged?.Invoke();
    }

    public void AddItemProgress(QuestData quest, int amount)
    {
        var q = activeQuests.Find(x => x.data == quest);

        if (q == null) return;

        q.currentAmountItems += amount;

        if(q.currentAmountItems >= quest.amountItems)
        {
            q.isCompletedItemQuest = true;
            Debug.Log("Quest completed: " + quest.title);
        }

        OnQuestListChanged?.Invoke();
    }

    public void AddKillProgress(QuestData quest, int amount)
    {
        var q = activeQuests.Find(x => x.data == quest);
        
        if (q == null) return;
        
        q.currentAmountKills += amount;
        
        if (q.currentAmountKills >= quest.amountKills)
        {
            q.isCompletedKillQuest = true;
            Debug.Log("Quest completed: " + quest.title);
        }
        
        OnQuestListChanged?.Invoke();
    }
}

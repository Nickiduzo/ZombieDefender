using UnityEngine;

public class QuestState : MonoBehaviour
{
    public QuestData data;
    
    public int currentAmountKills;
    public int currentAmountItems;
    public bool isCompletedItemQuest;
    public bool isCompletedKillQuest;

    public QuestState(QuestData data)
    {
        this.data = data;
        currentAmountKills = 0;
        currentAmountItems = 0;

        isCompletedItemQuest = false;
        isCompletedKillQuest = false;
    }

    public bool IsCompleted()
    {
        return isCompletedItemQuest && isCompletedKillQuest;
    }
}

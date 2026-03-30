using UnityEngine;

public class QuestState : MonoBehaviour
{
    public QuestData data;
    public int currentAmount;
    public bool isCompleted;

    public QuestState(QuestData data)
    {
        this.data = data;
        currentAmount = 0;
        isCompleted = false;
    }
}

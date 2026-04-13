using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("Description")]
    public string title;
    [TextArea] public string[] description;

    [Header("Requirements")]
    public int requiredLevel;
    public bool isAlreadyCompleted;
    public int amountKills;
    public int amountItems;

    [Header("Rewards")]
    public int moneyReward;
    public float expReward;
}

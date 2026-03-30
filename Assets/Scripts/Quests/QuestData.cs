using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Quest Data")]
public class QuestData : ScriptableObject
{
    public string title;
    [TextArea] public string description;
    public int requiredAmount;
    public int requredLevel;
}

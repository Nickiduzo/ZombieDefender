using System.Collections;
using TMPro;
using UnityEngine;

public class SavantNPC : NPC, ITalkable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogPanelUI dialogPanel;

    [SerializeField] private QuestData[] quests;

    public bool isBeginTalked = false;

    public override void Interact(Transform target)
    {
        base.Interact(target);
        if (!isBeginTalked)
        {
            isBeginTalked = true;
            Talk(dialogData);
        }
        else
        {
            Talk(quests[0]);
        }
    }
    public void Talk(DialogData data)
    {
        dialogPanel.DisplayNextParagraph(dialogData);
    }

    public void Talk(QuestData data)
    {
        dialogPanel.DisplayNextParagraph(data);
    }
}

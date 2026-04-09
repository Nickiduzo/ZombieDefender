using System.Collections;
using TMPro;
using UnityEngine;

public class SavantNPC : NPC, ITalkable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogPanelUI dialogPanel;

    private bool isTalking = false;
    private bool isAlreadyTalked = false;

    public override void Interact(Transform target)
    {
        base.Interact(target);
        Talk(dialogData);
    }
    public void Talk(DialogData data)
    {
        dialogPanel.DisplayNextParagraph(dialogData);
    }
}

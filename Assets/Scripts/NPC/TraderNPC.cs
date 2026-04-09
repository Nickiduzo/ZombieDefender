using UnityEngine;

public class NewBehaviourScript : NPC, ITalkable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogPanelUI dialogPanel;

    private bool isTalking = false;
    private bool isAlreadyTalked = false;

    override public void Interact(Transform target)
    {
        base.Interact(target);
        Talk(dialogData);
    }

    public void Talk(DialogData data)
    {
        dialogPanel.DisplayNextParagraph(dialogData);
    }
}

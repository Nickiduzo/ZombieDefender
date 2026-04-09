using UnityEngine;

public class SmithNPC : NPC, ITalkable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogPanelUI dialogPanel;

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

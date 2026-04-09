using UnityEngine;

public class AlchemicNPC : NPC, ITalkable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogPanelUI dialogPanel;
    
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

using System.Collections;
using TMPro;
using UnityEngine;

public class SavantNPC : NPC, ITalkable
{
    [SerializeField] private DialogData dialogData;

    private bool isTalking = false;
    private bool isAlreadyTalked = false;

    public override void Interact(Transform target)
    {
        Talk(dialogData);
    }
    public void Talk(DialogData data)
    {
        throw new System.NotImplementedException();
    }
}

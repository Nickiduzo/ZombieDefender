using UnityEngine;

[CreateAssetMenu(fileName ="DialogData", menuName ="Dialog Data/Dialog")]
public class DialogData : ScriptableObject
{
    public string characterName;
    public string[] lines;
    //public DialogChoice[] choices;
}

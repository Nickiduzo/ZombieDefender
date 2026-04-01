using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Button skipButton;

    private Queue<string> sentences = new Queue<string>();

    private bool converstaionEnded;

    public void DisplayNextParagraph(DialogData data)
    {
        if(sentences.Count == 0)
        {
            if(!converstaionEnded)
            {

            }
        }
    }

    private void StartConversation()
    {

    }

    private void EndConversation()
    {

    }
}

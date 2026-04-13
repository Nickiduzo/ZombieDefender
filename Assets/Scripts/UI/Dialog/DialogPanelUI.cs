using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private float typingSpeed = 10f;
     
    [SerializeField] private Button skipButton;

    private Queue<string> sentences = new Queue<string>();

    private bool converstaionEnded;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public void DisplayNextParagraph(DialogData data)
    {
        if(sentences.Count == 0)
        {
            if(!converstaionEnded)
            {
                StartConversation(data);   
            }
            else if (converstaionEnded && !isTyping)
            {
                EndConversation();
                return;
            }
        }

        if(!isTyping)
        {
            p = sentences.Dequeue();
    
            typeDialogCoroutine = StartCoroutine(TypeDialogText(p));
        }
        else
        {
            FinishParagraphEarly();
            return;
        }

        if(sentences.Count == 0)
        {
            converstaionEnded = true;
        }
    }

    public void DisplayNextParagraph(QuestData data)
    {
        if(sentences.Count == 0)
        {
            if(!converstaionEnded)
            {
                StartConversation(data);
            }
            else if (converstaionEnded && !isTyping)
            {
                EndConversation();
                return;
            }
        }

        if(!isTyping)
        {
            p = sentences.Dequeue();
    
            typeDialogCoroutine = StartCoroutine(TypeDialogText(p));
        }
        else
        {
            FinishParagraphEarly();
            return;
        }

        if(sentences.Count == 0)
        {
            converstaionEnded = true;
            QuestManager.Instance.AddQuest(data);
        }
    }

    private void StartConversation(DialogData data)
    {
        if (!gameObject.activeSelf)
        {
            DialogManager.Instance.SetDialogState(true);
            gameObject.SetActive(true);
        }

        npcName.text = data.characterName;

        for (int i = 0; i < data.lines.Length; i++)
        {
            sentences.Enqueue(data.lines[i]);
        }  
    }
    
    private void StartConversation(QuestData data)
    {
        if (!gameObject.activeSelf)
        {
            DialogManager.Instance.SetDialogState(true);
            gameObject.SetActive(true);
        }

        npcName.text = data.title;

        for (int i = 0; i < data.description.Length; i++)
        {
            sentences.Enqueue(data.description[i].ToString());
        }
    }

    private void EndConversation()
    {
        converstaionEnded = false;
        DialogManager.Instance.SetDialogState(false);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogText(string p)
    {
        isTyping = true;

        dialogText.text = "";

        string originaltext = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            dialogText.text = originaltext;

            displayedText = dialogText.text.Insert(alphaIndex, HTML_ALPHA);

            dialogText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typingSpeed);
        }

        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogCoroutine);

        dialogText.text = p;

        isTyping = false;
    }
}

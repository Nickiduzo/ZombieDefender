using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    private bool dialogState = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDialogState(bool state)
    {
        dialogState = state;
    }

    public bool GetCurrentDialogState()
    {
        return dialogState;
    } 
}

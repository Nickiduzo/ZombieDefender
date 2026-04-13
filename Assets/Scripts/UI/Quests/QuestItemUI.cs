using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questProgress;

    public void Setup(QuestState state)
    {
        questTitle.text = state.data.title;

    }
}

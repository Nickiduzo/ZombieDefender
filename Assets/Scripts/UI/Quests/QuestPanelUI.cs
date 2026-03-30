using UnityEngine;

public class QuestPanelUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject questItemPrefab;

    private void OnDisable()
    {
        QuestManager.Instance.OnQuestListChanged -= Refresh;
    }

    private void Start()
    {
        if(QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestListChanged += Refresh;
            Refresh();
        }
    }

    private void Refresh()
    {
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        var quests = QuestManager.Instance.GetQuests();

        foreach(var quest in quests)
        {
            var obj = Instantiate(questItemPrefab, container);
            obj.GetComponent<QuestItemUI>().Setup(quest);
        }
    }
}

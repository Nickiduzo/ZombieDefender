using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAmount;

    public void SetText(string name, int amount)
    {
        textName.text = name;
        textAmount.text = amount.ToString();
    }
}

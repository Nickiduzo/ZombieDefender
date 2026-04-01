using UnityEngine;
using UnityEngine.UIElements;

public class DevPanel : MonoBehaviour
{
    // dev panel for test light and fast game issues

    [SerializeField] private GameObject player;
    [SerializeField] private CharacterMoney moneySystem;
    [SerializeField] private GameObject lootPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
                DropLoot();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            IncreaseMoney();
        }
    }
    private void DropLoot()
    {
        Loot temp = Instantiate(lootPrefab, player.transform.position + player.transform.forward * 2, Quaternion.identity).GetComponent<Loot>();
        if (temp != null)
        {
            temp.Initialization();
        }
    }

    private void IncreaseMoney()
    {
        moneySystem.AddMoney(100);
    }
}

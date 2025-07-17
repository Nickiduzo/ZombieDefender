using TMPro;
using UnityEngine;

public class DamageInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerDamage;
    [SerializeField] private TextMeshProUGUI enemyDamage;
    [SerializeField] private TextMeshProUGUI experience;

    [SerializeField] private Animator playerTextAnim;
    [SerializeField] private Animator enemyTextAnim;
    [SerializeField] private Animator experienceTextAnim;

    private void OnEnable()
    {
        Enemy.OnDamageUI += ShowPlayerDamage;
        Dummy.OnDamage += ShowPlayerDamage;

        CharacterHealth.OnDamageUI += ShowEnemyDamage;

        CharacterExperience.OnTakeExp += ShowExperience;
    }

    private void ShowPlayerDamage(int damage)
    {
        playerDamage.text = damage.ToString();

        int indexAnimation = Random.Range(0, 4);

        playerDamage.gameObject.SetActive(true);

        playerTextAnim.SetTrigger(indexAnimation.ToString());
    }

    private void ShowExperience(int exp)
    {
        experience.text = exp.ToString() + " exp";

        int indexAnimation = Random.Range(0, 4);

        experience.gameObject.SetActive(true);

        experienceTextAnim.SetTrigger(indexAnimation.ToString());
    }

    private void ShowEnemyDamage(int damage)
    {
        enemyDamage.text = "- "+ damage.ToString();

        int indexAnimation = Random.Range(0, 4);

        enemyDamage.gameObject.SetActive(true);

        enemyTextAnim.SetTrigger(indexAnimation.ToString());
    }

    private void OnDisable()
    {
        Enemy.OnDamageUI -= ShowPlayerDamage;
        Dummy.OnDamage -= ShowPlayerDamage;

        CharacterHealth.OnDamageUI -= ShowEnemyDamage;

        CharacterExperience.OnTakeExp -= ShowExperience;
    }
}

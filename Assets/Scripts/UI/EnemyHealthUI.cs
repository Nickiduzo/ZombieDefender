using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    private float _maxSliderValue;
    private Enemy _enemy;
    private Transform _cachedTransform;
    private Transform _targetTransform;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();

        if (_enemy != null)
        {
            _healthSlider.maxValue = _enemy.maxHealth;
        }

        _maxSliderValue = _healthSlider.maxValue;
        _cachedTransform = transform;
        _targetTransform = Camera.main.transform;
    
        _enemy.OnHealthChanged += UpdateHealthSlider;
    }

    private void Update()
    {
        _cachedTransform.LookAt(_cachedTransform.position + _targetTransform.forward);
    }
    public void UpdateHealthSlider(int newValue, int maxValue)
    {
        _healthSlider.value = (float)newValue / maxValue * _maxSliderValue;
    
        if(_healthSlider.value <= 0)
        {
            _healthSlider.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        _enemy.OnHealthChanged -= UpdateHealthSlider;
    }
}

using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set;}
    public event Action OnPlayerHealth;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();

            if (_currentHealth <= 0)
            {
                OnPlayerHealth?.Invoke();
            }

        }
    }
    
    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
        }
    }
}

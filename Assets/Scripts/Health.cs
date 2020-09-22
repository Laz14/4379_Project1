using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _health = 3;
    private int _maxHealth;
    [SerializeField] HealthBar _healthBar;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;

        float healthPercentage = (float)_health / _maxHealth;
        if (_healthBar != null)
        {
            _healthBar.SetHealthBarValue(healthPercentage);
        }

        if (_health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
    }
}

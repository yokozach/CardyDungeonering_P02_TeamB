using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int _maxHealth = 10;
    [SerializeField] public int _currentHealth = 10;
    [SerializeField] public int _currentDefense = 0;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (_currentDefense >= 1)
        {
            _currentDefense -= damage;
            if (_currentDefense < 0)
            {
                _currentHealth += _currentDefense;
                _currentDefense = 0;
            }
        }
        else
        {
            _currentHealth -= damage;
        }
        Debug.Log("Took: " + damage);
    }
    public void Kill()
    {
        Debug.Log("Dead");
    }
}

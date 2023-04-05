using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int _maxHealth = 10;
    [SerializeField] public int _currentHealth = 10;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("Took: " + damage);
    }
    public void Kill()
    {
        Debug.Log("Dead");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int _maxHP = 10;
    [SerializeField] public int _curHP = 10;
    [SerializeField] public int _curDef = 0;

    private PlayerController playerController;
    private CardEvent_Enemy enemy;


    void Awake()
    {
        _curHP = _maxHP;

    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        enemy = GetComponent<CardEvent_Enemy>();

    }

    public void TakeDamage(int dmg)
    {
        // Deal dmg to shield & HP
        if (_curDef >= 1)
        {
            _curDef -= dmg;
            if (_curDef < 0)
            {
                _curHP += _curDef;
                _curDef = 0;
                if (_curHP <= 0)
                {
                    Kill();
                }
                else
                {
                    if (playerController != null) playerController._hurt = true;
                    if (enemy != null) enemy._hurt = true;
                }
            }
            else
            {
                if (playerController != null) playerController._hurtShield = true;
                if (enemy != null) enemy._hurtShield = true;
            }
        }
        else
        {
            _curHP -= dmg;
            if (_curHP <= 0) Kill();
        }
        Debug.Log("Took: " + dmg);

    }
    public void Kill()
    {
        Debug.Log("Dead");
        if (playerController != null) playerController._killed = true;
        if (enemy != null) enemy._killed = true;

    }

}

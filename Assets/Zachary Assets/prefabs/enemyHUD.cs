using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHUD : MonoBehaviour
{
    private GameController _controller;
    public Health _enemyHealth;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;

    void Start()
    {
        _enemyHealth = _controller._enemyHealth;
    }


    public void ShieldCalc()
    {
        float shieldPercentage = Mathf.Clamp01((float)_enemyHealth._curDef / (float)_enemyHealth._maxDef);
        shieldBar.fillAmount = shieldPercentage;
        

    }
    public void HealthCalc()
    {
        float healthPercentage = Mathf.Clamp01((float)_enemyHealth._curHP / (float)_enemyHealth._maxHP);
        healthBar.fillAmount = healthPercentage;
      
    }

}

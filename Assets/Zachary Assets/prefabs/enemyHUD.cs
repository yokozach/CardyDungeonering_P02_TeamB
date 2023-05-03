using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHUD : MonoBehaviour
{
    private GameController _controller;
    public Health _enemyHealth = null;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;
    [SerializeField] RectTransform _enemyHUDValues;
    private RectTransform _enemyHUDTransform;

    private void Start()
    {
        _enemyHUDTransform = _enemyHUDValues;
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

    public void TurnOffAndOn(bool set)
    {
        this.gameObject.SetActive(set);
    }

    public void AnimationStart()
    {
        _enemyHUDValues.anchoredPosition = new Vector2(0, 100);
        _enemyHUDValues.position = Vector2.MoveTowards(_enemyHUDValues.position, _enemyHUDTransform.position, 5f);
    }

    IEnumerator
}

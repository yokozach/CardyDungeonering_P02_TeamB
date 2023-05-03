using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyHUD : MonoBehaviour
{
    [Header("Components")]
    private GameController _controller;
    public string _enemyName;
    public Health _enemyHealth = null;
    public CardEvent_Enemy _enemyStats;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;
    [SerializeField] TextMeshProUGUI curNameText;
    [SerializeField] TextMeshProUGUI curHPText;
    [SerializeField] TextMeshProUGUI curSHText;
    [SerializeField] TextMeshProUGUI atkText;

    public void ShieldCalc()
    {
        float shieldPercentage = Mathf.Clamp01((float)_enemyHealth._curDef / (float)_enemyHealth._maxDef);
        shieldBar.fillAmount = shieldPercentage;
        curSHText.text = _enemyHealth._curDef.ToString();

    }
    public void HealthCalc()
    {
        float healthPercentage = Mathf.Clamp01((float)_enemyHealth._curHP / (float)_enemyHealth._maxHP);
        healthBar.fillAmount = healthPercentage;
        curHPText.text = _enemyHealth._curHP.ToString();

    }

    public void TurnOffAndOn(bool set)
    {
        this.gameObject.SetActive(set);
    }

    public void SetHUDName(string name)
    {
        _enemyName = name;
        curNameText.text = name;
    }

    public void SetAttackValues()
    {
        atkText.text =  "ATK: " + _enemyStats._minAttackRange + " - " + _enemyStats._maxAttackRange;
    }

}

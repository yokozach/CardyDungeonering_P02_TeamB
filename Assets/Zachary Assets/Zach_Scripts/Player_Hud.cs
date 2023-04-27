using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Hud : MonoBehaviour
{

    [SerializeField] CentralManager centralManager;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;
    [SerializeField] TMP_Text healthNum;
    [SerializeField] TMP_Text defenseNum;
    [SerializeField] TMP_Text attackNum;

private Health playerHP;

    private void Awake()
    {
    }

    private void Start()
    {
        playerHP = centralManager._playerController.GetComponent<Health>();
        HealthCalc();
        ShieldCalc();
        AttackDisplay();
    }


    public void ShieldCalc()
    {
        float shieldPercentage = Mathf.Clamp01((float)playerHP._curDef / (float)playerHP._maxDef);
        shieldBar.fillAmount = shieldPercentage; 
        defenseNum.text = playerHP._curDef.ToString();

    }
    public void HealthCalc()
    {
        float healthPercentage = Mathf.Clamp01((float)playerHP._curHP / (float)playerHP._maxHP);
        healthBar.fillAmount = healthPercentage;
        healthNum.text = playerHP._curHP.ToString();
    }
    public void AttackDisplay()
    {
        attackNum.text = centralManager._playerStats._attackValueDisplay.ToString();
    }
}
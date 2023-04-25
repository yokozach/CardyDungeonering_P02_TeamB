using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Player_Hud : MonoBehaviour
{


    public Image HealhBar;
    public Image ShielhBar;
    public TMP_Text healthNum;
    public TMP_Text defenseNum;
    public TMP_Text attackNum;
    public Health playerHP;
    public PlayerStats playerAttack;

    private void Start()
    {
        defenseNum.text = playerHP._curDef.ToString();
        healthNum.text = playerHP._curHP.ToString();
        attackNum.text = playerAttack._playerBaseAttack.ToString();
    }


    public void shieldcalc()
    {
        ShielhBar.fillAmount = playerHP._curDef / playerHP._maxDef;
        defenseNum.text = playerHP._curDef.ToString();

    }
    public void healthCalc()
    {
        HealhBar.fillAmount = playerHP._curHP / playerHP._maxHP;
        healthNum.text = playerHP._curHP.ToString();
    }


    public void AttackDisplay()
    {

        attackNum.text = playerAttack._playerBaseAttack.ToString();
    }
}
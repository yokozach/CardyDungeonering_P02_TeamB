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
    public Health playerObject;
    public PlayerStats playObj;


    void Update()
    {


        HealhBar.fillAmount = playerObject._curHP / playerObject._maxHP;
        ShielhBar.fillAmount = playerObject._curDef / playerObject._maxDef;

        attackNum.text = playObj._playerAttack.ToString();
        healthNum.text = playerObject._curHP.ToString();
        defenseNum.text = playerObject._curDef.ToString();
    }
}
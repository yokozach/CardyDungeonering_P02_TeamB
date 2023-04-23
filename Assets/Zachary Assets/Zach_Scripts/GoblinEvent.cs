using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoblinEvent : MonoBehaviour
{
    public int enemyHP = 0;
    public int enemmyShield = 0;
    public int enemyAttack = 0;
    public TMP_Text HPdisplay;
    public TMP_Text ShieldDisplay;
    public TMP_Text Attackdisplay;
    [SerializeField] GameObject _card;
    [SerializeField] EventTile _tileVar;

    public void Update()
    {

        HPdisplay.text = enemyHP.ToString();
        ShieldDisplay.text = enemmyShield.ToString();
        Attackdisplay.text = enemyAttack.ToString();


        if (enemyHP <= 0)
        {

            death();
        }


    }

    public void damage()
    {

        if (enemmyShield >0)
        {
            enemmyShield -= 2;
        }
        if (enemmyShield <= 0)
        {
            enemyHP -= 2;
        }
    }

    void death()
    {
        _tileVar._battleStart = false;
        Destroy(_card);

    }

}

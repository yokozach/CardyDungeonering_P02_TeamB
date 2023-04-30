using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Hud : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] CentralManager centralManager;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;
    [SerializeField] TMP_Text healthNum;
    [SerializeField] TMP_Text defenseNum;
    [SerializeField] TMP_Text attackNum;

    [Header("Base Stat Txt")]
    [SerializeField] public TextMeshProUGUI baseAttackTxt;
    [SerializeField] public TextMeshProUGUI baseDefenseTxt;
    [SerializeField] public TextMeshProUGUI baseHitsTxt;
    [SerializeField] public TextMeshProUGUI baseCritTxt;
    [SerializeField] public TextMeshProUGUI basePierceTxt;
    [SerializeField] public TextMeshProUGUI baseSharpTxt;
    [SerializeField] public TextMeshProUGUI baseHeavyTxt;

    [Header("Buff Stat Indicators")]
    [SerializeField] public TextMeshProUGUI buffAttackTxt;
    [SerializeField] public TextMeshProUGUI buffDefenseTxt;
    [SerializeField] public TextMeshProUGUI buffHitsTxt;
    [SerializeField] public TextMeshProUGUI buffCritTxt;
    [SerializeField] public TextMeshProUGUI buffPierceTxt;
    [SerializeField] public TextMeshProUGUI buffSharpTxt;
    [SerializeField] public TextMeshProUGUI buffHeavyTxt;

    [Header("Stat Button")]
    [SerializeField] private Button StatsBtn;

    private Health playerHP;
    private PlayerStats playerStats;

    private void Awake()
    {
        StatsBtn.interactable = false;
    }

    private void Start()
    {
        playerHP = centralManager._playerController.GetComponent<Health>();
        playerStats = centralManager._playerStats;
        HealthCalc();
        ShieldCalc();
        StatDisplay();

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
    public void StatDisplay()
    {
        attackNum.text = centralManager._playerStats._finalAtt.ToString();

        baseAttackTxt.text = "Att: " + playerStats._baseAtt;
        baseDefenseTxt.text = "Def: " + playerStats._baseDef;
        baseHitsTxt.text = "Hit: " + playerStats._baseHit;
        baseCritTxt.text = "Crit: " + playerStats._baseCrit*100 + "%";
        if (playerStats._basePierce) basePierceTxt.alpha = 1f; else basePierceTxt.alpha = 0.25f;
        if (playerStats._baseSharp) baseSharpTxt.alpha = 1f; else baseSharpTxt.alpha = 0.25f;
        if (playerStats._baseHeavy) baseHeavyTxt.alpha = 1f; else baseHeavyTxt.alpha = 0.25f;

        if (playerStats._buffAtt > 0)
        {
            buffAttackTxt.text = "+" + playerStats._buffAtt;
            buffAttackTxt.alpha = 1f;
        }
        else buffAttackTxt.alpha = 0;

        if (playerStats._buffDef > 0) 
        { 
            buffDefenseTxt.text = "+" + playerStats._buffDef;
            buffDefenseTxt.alpha = 1f;
        }
        else buffDefenseTxt.alpha = 0;

        if (playerStats._buffHit > 0)
        {
            buffHitsTxt.text = "+" + playerStats._buffHit;
            buffHitsTxt.alpha = 1f;
        }
        else buffHitsTxt.alpha = 0;

        if (playerStats._buffCrit > 0) 
        { 
            buffCritTxt.text = "+" + playerStats._buffCrit*100 + "%";
            buffCritTxt.alpha = 1f;
        }
        else buffCritTxt.alpha = 0;
        
        if (playerStats._buffPierce) buffPierceTxt.alpha = 1f; else buffPierceTxt.alpha = 0f;
        if (playerStats._buffSharp) buffSharpTxt.alpha = 1f; else buffSharpTxt.alpha = 0f;
        if (playerStats._buffHeavy) buffHeavyTxt.alpha = 1f; else buffHeavyTxt.alpha = 0f;

    }

    public void ToggleButtonActive()
    {
        if (StatsBtn.IsInteractable()) StatsBtn.interactable = false;
        else StatsBtn.interactable = true;
    }

}
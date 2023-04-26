using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCard_Heal : InvCard
{
    public enum HealType
    {
        HealHP,
        RegenHP,
        MaximizeHP,
        RepairSH,
        RegenSH,
        MaximizeSH,
    }
    
    [Header("Heal Data")]
    [SerializeField] HealType healType;
    [SerializeField] int minHeal;
    [SerializeField] int maxHeal;
    [SerializeField] int turnDuration;

    private Health _playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = centralManager._playerController.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateCardEffects()
    {
        if (healType == HealType.HealHP) 
        {
            // Add health to the player between minHeal and maxHeal
            int amount = Random.Range(minHeal, maxHeal + 1);
            _playerHealth.HealHP(amount);
        }
        else if (healType == HealType.RegenHP)
        {
            // Add regen health to the player between minHeal and max Heal
            Vector2 amount = new Vector2(minHeal, maxHeal);
            _playerHealth.AddRegen(amount, turnDuration);
        }
        else if (healType == HealType.MaximizeHP)
        {
            // Add max health to the player between minHeal and maxHeal
            int amount = Random.Range(minHeal, maxHeal + 1);
            _playerHealth.IncreaseMaxHP(amount);
        }
        else if (healType == HealType.RepairSH)
        {
            // Add shield to the player between minHeal and maxHeal
            int amount = Random.Range(minHeal, maxHeal + 1);
            _playerHealth.HealSH(amount);
        }
        else if (healType == HealType.RegenSH)
        {
            // NOT YET IMPLEMENTED (There shouldn't be any of this type yet anyways)
            int amount = Random.Range(minHeal, maxHeal + 1);
        }
        else if (healType == HealType.MaximizeSH)
        {
            // Add max shield to the player between minHeal and maxHeal
            int amount = Random.Range(minHeal, maxHeal + 1);
            _playerHealth.IncreaseMaxShield(amount);
        }

        RemoveCardFromInv();

    }

}

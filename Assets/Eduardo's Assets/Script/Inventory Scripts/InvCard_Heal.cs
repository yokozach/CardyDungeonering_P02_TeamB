using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCard_Heal : InvCard
{
    public enum HealType
    {
        HP,
        SH
    }
    
    [Header("Heal Data")]
    [SerializeField] HealType healType;
    [SerializeField] int minHeal;
    [SerializeField] int maxHeal;

    private PlayerController PC;
    private Health _playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
        _playerHealth = PC.gameObject.GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateCardEffects()
    {
        if (healType == HealType.HP) 
        {
            // Add health to the player between minHeal and maxHeal
            int amount = Random.Range(minHeal, maxHeal + 1);
            _playerHealth.HealHP(amount);
        }
        else if (healType == HealType.SH)
        {
            // Add shield to the player between minHeal and maxHeal
            int amount = Random.Range(minHeal, maxHeal + 1);
            _playerHealth.HealSH(amount);
        }
    }

}

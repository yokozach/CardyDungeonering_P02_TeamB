using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCard_Attack : InvCard
{
    [Header("Attack Data")]
    [SerializeField] PlayerStats PlayerStats;
    [SerializeField] int attDmg = 3;

    [Header("Traits")]
    [SerializeField] int hits = 1;
    [SerializeField] bool pierce = false;
    [SerializeField] bool sharp = false;
    [SerializeField] bool heavy = false;
    [Range(0, 1)] [SerializeField] float Crit = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateCardEffects()
    {

        PlayerStats.ChangeBaseStats(attDmg, 0, hits, pierce, sharp, heavy, Crit);
        centralManager._sfxPlayer.Audio_EquipWeapon();

        RemoveCardFromInv();

    }

}

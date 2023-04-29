using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCard_Buff : InvCard
{
    public enum BuffType
    {
        Attack,
        Defense,
        MultiHit,
        Pierce,
        Sharp,
        Heavy,
        Crit

    }
    
    [Header("Buff Data")]
    [SerializeField] BuffType _buffType;
    [SerializeField] float _buffValue;
    [SerializeField] int _turnsItLasts;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateCardEffects()
    {
        centralManager._sfxPlayer.Audio_Buff();
        if (_buffType == BuffType.Attack) AttackStatBuff();
        if (_buffType == BuffType.Defense) DefenseStatBuff();
        if (_buffType == BuffType.MultiHit) MultiHitBuff();
        if (_buffType == BuffType.Pierce) PierceBuff();
        if (_buffType == BuffType.Sharp) SharpBuff();
        if (_buffType == BuffType.Heavy) HeavyBuff();
        if (_buffType == BuffType.Crit) CritBuff();
        centralManager._playerStats.CalculateBuffEffects();
        RemoveCardFromInv();
    }

    public void AttackStatBuff()
    {
        centralManager. _playerStats.AddAttBuff((int)_buffValue, _turnsItLasts);
    }

    public void DefenseStatBuff()
    {
        centralManager._playerStats.AddDefBuff((int)_buffValue, _turnsItLasts);
    }

    public void MultiHitBuff()
    {
        centralManager._playerStats.AddMultiHitBuff((int)_buffValue, _turnsItLasts);
    }

    public void PierceBuff()
    {
        centralManager._playerStats.AddPierceBuff(true, _turnsItLasts);
    }

    public void SharpBuff()
    {
        centralManager._playerStats.AddSharpBuff(true, _turnsItLasts);
    }

    public void HeavyBuff()
    {
        centralManager._playerStats.AddHeavyBuff(true, _turnsItLasts);
    }

    public void CritBuff()
    {
        centralManager._playerStats.AddCritBuff(_buffValue, _turnsItLasts);
    }


}

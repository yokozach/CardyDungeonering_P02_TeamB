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
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] BuffType _buffType;
    [SerializeField] float _buffValue;
    [SerializeField] int _turnsItLasts;

    // Start is called before the first frame update
    void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateCardEffects()
    {
        if (_buffType == BuffType.Attack) AttackStatBuff();
        if (_buffType == BuffType.Defense) DefenseStatBuff();
        if (_buffType == BuffType.MultiHit) MultiHitBuff();
        if (_buffType == BuffType.Pierce) PierceBuff();
        if (_buffType == BuffType.Sharp) SharpBuff();
        if (_buffType == BuffType.Heavy) HeavyBuff();
        if (_buffType == BuffType.Crit) CritBuff();
    }

    public void AttackStatBuff()
    {
        _playerStats.AddAttBuff((int)_buffValue, _turnsItLasts);
    }

    public void DefenseStatBuff()
    {
        _playerStats.AddDefBuff((int)_buffValue, _turnsItLasts);
    }

    public void MultiHitBuff()
    {
        _playerStats.AddMultiHitBuff((int)_buffValue, _turnsItLasts);
    }

    public void PierceBuff()
    {
        _playerStats.AddPierceBuff(true, _turnsItLasts);
    }

    public void SharpBuff()
    {
        _playerStats.AddSharpBuff(true, _turnsItLasts);
    }

    public void HeavyBuff()
    {
        _playerStats.AddHeavyBuff(true, _turnsItLasts);
    }

    public void CritBuff()
    {
        _playerStats.AddCritBuff(_buffValue, _turnsItLasts);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CentralManager centralManager;
    private Player_Hud playerHud;

    [Header("Base Stats")]
    [SerializeField] public int _baseAtt = 3;
    [SerializeField] public int _baseDef = 0;
    [SerializeField] public int _baseHit = 1;
    [Range(0, 1)] [SerializeField] public float _baseCrit = 0.01f;
    [SerializeField] public bool _basePierce = false;
    [SerializeField] public bool _baseSharp = false;
    [SerializeField] public bool _baseHeavy = false;

    [Header("Buff Stats")]
    [SerializeField] public int _buffAtt;
    [SerializeField] public int _buffDef;
    [SerializeField] public int _buffHit;
    [Range(0, 1)] [SerializeField] public float _buffCrit;
    [SerializeField] public bool _buffPierce;
    [SerializeField] public bool _buffSharp;
    [SerializeField] public bool _buffHeavy;

    [Header("Totals")]
    [SerializeField] public int _finalAtt;
    [SerializeField] public int _finalDef;
    [SerializeField] public int _finalHit;
    [Range(0, 1)] [SerializeField] public float _finalCrit;
    [SerializeField] public bool _finalPierce;
    [SerializeField] public bool _finalSharp;
    [SerializeField] public bool _finalHeavy;

    [Header("Multipliers")]
    [SerializeField] public float _sharpMultiplier;
    [SerializeField] public float _heavyMultiplier;
    [SerializeField] public float _critMultiplier;

    private void Awake()
    {
        playerHud = centralManager._playerHUD;

    }

    private void Start()
    {
        CalculateBuffEffects();
    }

    public void ChangeBaseStats(int att = 3, int def = 0, int numOfAtt = 1, float crit = 0.01f, bool pierce = false, bool sharp = false, bool heavy = false)
    {
        _baseAtt = att;
        _baseDef = def;
        _baseHit = numOfAtt;
        _baseCrit = crit;
        _basePierce = pierce;
        _baseSharp = sharp;
        _baseHeavy= heavy;

        _finalAtt = _baseAtt + _buffAtt;
        _finalDef = _baseDef + _buffDef;
        _finalHit = _baseHit + _buffHit;
        _finalCrit = _baseCrit + _buffCrit;
        if (_basePierce || _buffPierce) _finalPierce = true; else _finalPierce = false;
        if (_baseSharp || _buffSharp) _finalSharp = true; else _finalSharp = false;
        if (_baseHeavy || _buffHeavy) _finalHeavy = true; else _finalHeavy = false;

        centralManager._playerHUD.StatDisplay();

    }

    #region Buff Lists

    private List<AttackBuff> attackBuffs = new List<AttackBuff>();
    private List<DefenseBuff> defenseBuffs = new List<DefenseBuff>();
    private List<MultiHitBuff> multiHitBuffs = new List<MultiHitBuff>();
    private List<PierceBuff> pierceBuffs = new List<PierceBuff>();
    private List<SharpBuff> sharpBuffs = new List<SharpBuff>();
    private List<HeavyBuff> heavyBuffs = new List<HeavyBuff>();
    private List<CritBuff> critBuffs = new List<CritBuff>();

    #endregion

    #region Add Buff Methods

    public void AddAttBuff(int buffValue, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        AttackBuff newBuff = new AttackBuff(buffValue, buffTurns);
        attackBuffs.Add(newBuff);
        CalculateAttBuffs();
    }
    public void AddDefBuff(int buffValue, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        DefenseBuff newBuff = new DefenseBuff(buffValue, buffTurns);
        defenseBuffs.Add(newBuff);
        CalculateDefBuffs();
    }
    public void AddMultiHitBuff(int buffValue, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        MultiHitBuff newBuff = new MultiHitBuff(buffValue, buffTurns);
        multiHitBuffs.Add(newBuff);
        CalculateHitBuffs();
    }
    
    public void AddCritBuff(float buffPercentage, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        CritBuff newBuff = new CritBuff(buffPercentage, buffTurns);
        critBuffs.Add(newBuff);
        CalculateCritBuffs();
    }
    public void AddPierceBuff(bool buffState, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        PierceBuff newBuff = new PierceBuff(buffState, buffTurns);
        pierceBuffs.Add(newBuff);
        CalculatePierceBuffs();
    }

    public void AddSharpBuff(bool buffState, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        SharpBuff newBuff = new SharpBuff(buffState, buffTurns);
        sharpBuffs.Add(newBuff);
        CalculateSharpBuffs();
    }
    public void AddHeavyBuff(bool buffState, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        HeavyBuff newBuff = new HeavyBuff(buffState, buffTurns);
        heavyBuffs.Add(newBuff);
        CalculateHeavyBuffs();
    }


    #endregion

    // Calculate buffs & updates buff values
    public void CalculateBuffEffects()
    {
        CalculateAttBuffs();
        CalculateDefBuffs();
        CalculateHitBuffs();
        CalculateCritBuffs();
        CalculatePierceBuffs();
        CalculateSharpBuffs();
        CalculateHeavyBuffs();
        centralManager._playerHUD.StatDisplay();
    }

    // Reduce offensive buff turn counters by 1
    public void ReduceOffBuffTurns ()
    {
        DecreaseAttBuffTurns();
        DecreaseHitBuffTurns();
        DecreaseCritBuffTurns();
        DecreasePierceBuffTurns();
        DecreaseSharpBuffTurns();
        DecreaseHeavyBuffTurns();
        centralManager._playerHUD.StatDisplay();
    }

    public void ReduceDefBuffTurns()
    {
        DecreaseDefBuffTurns();
        centralManager._playerHUD.StatDisplay();
    }

    #region  Buff Calculators

    public void CalculateAttBuffs()
    {
        _buffAtt = 0;
        // Loop through all attack buffs and apply them to the buff att value
        for (int i = 0; i < attackBuffs.Count; i++)
        {
            AttackBuff buff = attackBuffs[i];
            _buffAtt += buff.value;
        }
        _finalAtt = _baseAtt + _buffAtt;
    }

    public void CalculateDefBuffs()
    {
        _buffDef = 0;
        // Loop through all defense buffs and apply them to the buff def value
        for (int i = 0; i < defenseBuffs.Count; i++)
        {
            DefenseBuff buff = defenseBuffs[i];
            _buffDef += buff.value;
        }
        _finalDef = _baseDef + _buffDef;
    }

    public void CalculateHitBuffs()
    {
        _buffHit = 0;
        // Loop through all multi-hit buffs and apply them to the buff hit value
        for (int i = 0; i < multiHitBuffs.Count; i++)
        {
            MultiHitBuff buff = multiHitBuffs[i];
            _buffHit += buff.value;
        }
        _finalHit = _baseHit + _buffHit;
    }

    public void CalculateCritBuffs()
    {
        _buffCrit = 0;
        // Loop through all crit buffs and apply them to the buff crit value
        for (int i = 0; i < critBuffs.Count; i++)
        {
            CritBuff buff = critBuffs[i];
            _buffCrit += buff.percent;
        }
        _finalCrit = _baseCrit + _buffCrit;
    }

    public void CalculatePierceBuffs()
    {
        _buffPierce = false;
        // Check if any pierce buffs are active
        for (int i = 0; i < pierceBuffs.Count; i++)
        {
            PierceBuff buff = pierceBuffs[i];
            _buffPierce = buff.pierce;
            if (buff.pierce) break;
        }
        if (_basePierce || _buffPierce) _finalPierce = true; else _finalPierce = false;
    }

    public void CalculateSharpBuffs()
    {
        _buffSharp = false;
        // Check if any sharp buffs are active
        for (int i = 0; i < sharpBuffs.Count; i++)
        {
            SharpBuff buff = sharpBuffs[i];
            _buffSharp = buff.sharp;
            if (buff.sharp) break;
        }
        if (_baseSharp || _buffSharp) _finalSharp = true; else _finalSharp = false;
    }

    public void CalculateHeavyBuffs()
    {
        _buffHeavy = false;
        // Check if any heavy buffs are active
        for (int i = 0; i < heavyBuffs.Count; i++)
        {
            HeavyBuff buff = heavyBuffs[i];
            _buffHeavy = buff.heavy;
            if (buff.heavy) break;
        }
        if (_baseHeavy || _buffHeavy) _finalHeavy = true; else _finalHeavy = false;
    }

    #endregion

    #region Buff Turn Counters

    public void DecreaseAttBuffTurns()
    {
        int listCount = attackBuffs.Count;
        // Loop through all att buffs and reduce turns left by 1
        for (int i = 0; i < attackBuffs.Count; i++)
        {
            AttackBuff buff = attackBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                attackBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != attackBuffs.Count) CalculateAttBuffs();
    }

    public void DecreaseDefBuffTurns()
    {
        int listCount = defenseBuffs.Count;
        // Loop through all def buffs and reduce turns left by 1
        for (int i = 0; i < defenseBuffs.Count; i++)
        {
            DefenseBuff buff = defenseBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                attackBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != defenseBuffs.Count) CalculateDefBuffs();
    }

    public void DecreaseHitBuffTurns()
    {
        int listCount = multiHitBuffs.Count;
        // Loop through all multi-hit buffs and reduce turns left by 1
        for (int i = 0; i < multiHitBuffs.Count; i++)
        {
            MultiHitBuff buff = multiHitBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                multiHitBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != multiHitBuffs.Count) CalculateHitBuffs();
    }

    public void DecreaseCritBuffTurns()
    {
        int listCount = critBuffs.Count;
        // Loop through all crit buffs and reduce turns left by 1
        for (int i = 0; i < critBuffs.Count; i++)
        {
            CritBuff buff = critBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                critBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != critBuffs.Count) CalculateCritBuffs();
    }

    public void DecreasePierceBuffTurns()
    {
        int listCount = pierceBuffs.Count;
        // Loop through all def buffs and reduce turns left by 1
        for (int i = 0; i < pierceBuffs.Count; i++)
        {
            PierceBuff buff = pierceBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                pierceBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != pierceBuffs.Count) CalculatePierceBuffs();
    }

    public void DecreaseSharpBuffTurns()
    {
        int listCount = sharpBuffs.Count;
        // Loop through all def buffs and reduce turns left by 1
        for (int i = 0; i < sharpBuffs.Count; i++)
        {
            SharpBuff buff = sharpBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                sharpBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != sharpBuffs.Count) CalculateSharpBuffs();
    }

    public void DecreaseHeavyBuffTurns()
    {
        int listCount = heavyBuffs.Count;
        // Loop through all def buffs and reduce turns left by 1
        for (int i = 0; i < heavyBuffs.Count; i++)
        {
            HeavyBuff buff = heavyBuffs[i];
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                heavyBuffs.RemoveAt(i);
                i--;
            }
        }
        if (listCount != heavyBuffs.Count) CalculateHeavyBuffs();
    }

    #endregion

    #region Buff Classes

    private class AttackBuff
    {
        public int value;
        public int turnsLeft;

        public AttackBuff(int value, int turnsLeft)
        {
            this.value = value;
            this.turnsLeft = turnsLeft;
        }
    }

    private class DefenseBuff
    {
        public int value;
        public int turnsLeft;

        public DefenseBuff(int value, int turnsLeft)
        {
            this.value = value;
            this.turnsLeft = turnsLeft;
        }
    }

    private class MultiHitBuff
    {
        public int value;
        public int turnsLeft;

        public MultiHitBuff(int value, int turnsLeft)
        {
            this.value = value;
            this.turnsLeft = turnsLeft;
        }
    }

    private class PierceBuff
    {
        public bool pierce;
        public int turnsLeft;

        public PierceBuff(bool state, int turnsLeft)
        {
            this.pierce = state;
            this.turnsLeft = turnsLeft;
        }
    }

    private class SharpBuff
    {
        public bool sharp;
        public int turnsLeft;

        public SharpBuff(bool state, int turnsLeft)
        {
            this.sharp = state;
            this.turnsLeft = turnsLeft;
        }
    }

    private class HeavyBuff
    {
        public bool heavy;
        public int turnsLeft;

        public HeavyBuff(bool state, int turnsLeft)
        {
            this.heavy = state;
            this.turnsLeft = turnsLeft;
        }
    }

    private class CritBuff
    {
        [Range(0, 1)] public float percent;
        public int turnsLeft;

        public CritBuff(float value, int turnsLeft)
        {
            this.percent = value;
            this.turnsLeft = turnsLeft;
        }
    }

    #endregion

}

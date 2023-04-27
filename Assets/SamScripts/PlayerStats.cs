using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CentralManager centralManager;

    [Header("ATT & DEF")]
    [SerializeField] public int _playerBaseAttack = 3; // Base attack power
    [SerializeField] public int _playerBaseDefense = 0; // Base defense power

    [HideInInspector] public int _totalPlayerAttack; // Includes base & other variables
    [HideInInspector] public int _totalPlayerDefense; // Includes base & other variables

    [HideInInspector] public int _attackValueDisplay; // Use to display visuals indicators
    [HideInInspector] public int _defenseValueDisplay; // Use to display visuals indicators

    [Header("Attack Styles")]
    [Range(0, 10)] [SerializeField] public int _numberOfAttacks = 1; // Number of times to inflict dmg in one turn
    [SerializeField] public bool _pierce = false; // Ignores enemy shield & directly dmgs enemy HP
    [SerializeField] public bool _sharp = false; // Extra dmg against HP
    [SerializeField] public float _sharpMultiplier = 1.5f; // Percentage of extra dmg dealt
    [SerializeField] public bool _heavy = false; // Extra dmg against shield
    [SerializeField] public float _heavyMultiplier = 1.5f; // Percentage of extra dmg dealt

    [HideInInspector] public int _totalNumberOfAttacks;

    [Header("Crit Stats")]
    [Range(0, 1)] [SerializeField] public float _baseCritChance = 0.01f; // Base crit chance
    [SerializeField] public float _CritMultiplier = 1.5f; // Percentage of extra dmg dealt if a crit lands

    [Range(0, 1)] [HideInInspector] public float _totalCritChance;

    private void Awake()
    {
        _totalPlayerAttack = _playerBaseAttack;
        _totalPlayerDefense = _playerBaseDefense;
        _totalNumberOfAttacks = _numberOfAttacks;

        _attackValueDisplay = _totalPlayerAttack;
        _defenseValueDisplay = _totalPlayerDefense;
    }

    public void ChangeBaseStats(int att = 3, int def = 0, int numOfAtt = 1, bool pierce = false, bool sharp = false, bool heavy = false, float crit = 0.01f)
    {
        _playerBaseAttack = att;
        _playerBaseDefense = def;
        _numberOfAttacks = numOfAtt;
        _pierce = pierce;
        _sharp = sharp;
        _heavy = heavy;
        _baseCritChance = crit;

        _totalPlayerAttack = _playerBaseAttack;
        _totalPlayerDefense = _playerBaseDefense;
        _totalNumberOfAttacks = _numberOfAttacks;

        UpdateAttackValueDisplay();
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
        UpdateAttackValueDisplay();
    }
    public void AddDefBuff(int buffValue, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        DefenseBuff newBuff = new DefenseBuff(buffValue, buffTurns);
        defenseBuffs.Add(newBuff);
        UpdateDefenseValueDisplay();
    }
    public void AddMultiHitBuff(int buffValue, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        MultiHitBuff newBuff = new MultiHitBuff(buffValue, buffTurns);
        multiHitBuffs.Add(newBuff);
    }
    public void AddPierceBuff(bool buffState, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        PierceBuff newBuff = new PierceBuff(buffState, buffTurns);
        pierceBuffs.Add(newBuff);
    }
    public void AddSharpBuff(bool buffState, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        SharpBuff newBuff = new SharpBuff(buffState, buffTurns);
        sharpBuffs.Add(newBuff);
    }
    public void AddHeavyBuff(bool buffState, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        HeavyBuff newBuff = new HeavyBuff(buffState, buffTurns);
        heavyBuffs.Add(newBuff);
    }
    public void AddCritBuff(float buffPercentage, int buffTurns)
    {
        // Create a new attack buff object and add it to the list of attack buffs
        CritBuff newBuff = new CritBuff(buffPercentage, buffTurns);
        critBuffs.Add(newBuff);
    }

    #endregion

    // Applies buffs & runs the counters
    public void RunAllBuffEffects()
    {
        RunAttackBuffTurns();
        RunDefenseBuffTurns();
        RunMultiHitBuffTurns();
        RunPierceBuffTurns();
        RunSharpBuffTurns();
        RunHeavykBuffTurns();
        RunCritBuffTurns();
    }

    // Resets the buff effects for next use
    public void ResetBuffEffects()
    {
        _totalPlayerAttack = _playerBaseAttack;
        _totalPlayerDefense = _playerBaseDefense;
        _totalNumberOfAttacks = _numberOfAttacks;
        _pierce = false;
        _sharp = false;
        _heavy = false;
        _baseCritChance = _totalCritChance;
    }

    #region Display Calculator Methods

    public void UpdateAttackValueDisplay()
    {
        _attackValueDisplay = _playerBaseAttack;
        // Loop through all attack buffs and calculate total attack value
        for (int i = 0; i < attackBuffs.Count; i++)
        {
            AttackBuff buff = attackBuffs[i];
            _attackValueDisplay += buff.value;
        }
        centralManager._playerHUD.AttackDisplay();
    }

    public void UpdateDefenseValueDisplay()
    {
        _attackValueDisplay = _playerBaseAttack;
        // Loop through all defense buffs and calculate total defense value
        for (int i = 0; i < defenseBuffs.Count; i++)
        {
            DefenseBuff buff = defenseBuffs[i];
            _attackValueDisplay += buff.value;
        }
        // FindObjectOfType<Player_Hud>().AttackDefense();
    }

    #endregion

    #region Buff Counters

    public void RunAttackBuffTurns()
    {
        // Loop through all attack buffs and apply them to the player's attack value
        for (int i = 0; i < attackBuffs.Count; i++)
        {
            AttackBuff buff = attackBuffs[i];
            _totalPlayerAttack += buff.value;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                attackBuffs.RemoveAt(i);
                i--;
            }
        }
    }
    public void RunDefenseBuffTurns()
    {
        // Loop through all defense buffs and apply them to the player's defense value
        for (int i = 0; i < defenseBuffs.Count; i++)
        {
            DefenseBuff buff = defenseBuffs[i];
            _totalPlayerDefense += buff.value;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                defenseBuffs.RemoveAt(i);
                i--;
            }
        }
    }
    public void RunMultiHitBuffTurns()
    {
        // Loop through all multi-hit buffs and apply them to the player's multi-hit value
        for (int i = 0; i < multiHitBuffs.Count; i++)
        {
            MultiHitBuff buff = multiHitBuffs[i];
            _totalNumberOfAttacks += buff.value;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                multiHitBuffs.RemoveAt(i);
                i--;
            }
        }
    }
    public void RunPierceBuffTurns()
    {
        // Loop through all pierce buffs & enable pierce bool
        for (int i = 0; i < pierceBuffs.Count; i++)
        {
            PierceBuff buff = pierceBuffs[i];
            _pierce = buff.pierce;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                pierceBuffs.RemoveAt(i);
                i--;
            }
        }
    }
    public void RunSharpBuffTurns()
    {
        // Loop through all sharp buffs & enable sharp bool
        for (int i = 0; i < sharpBuffs.Count; i++)
        {
            SharpBuff buff = sharpBuffs[i];
            _pierce = buff.sharp;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                sharpBuffs.RemoveAt(i);
                i--;
            }
        }
    }
    public void RunHeavykBuffTurns()
    {
        // Loop through all heavy buffs & enable heavy bool
        for (int i = 0; i < heavyBuffs.Count; i++)
        {
            HeavyBuff buff = heavyBuffs[i];
            _heavy = buff.heavy;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                heavyBuffs.RemoveAt(i);
                i--;
            }
        }
    }
    public void RunCritBuffTurns()
    {
        // Loop through all crit buffs & apply them to the player's crit chance
        for (int i = 0; i < critBuffs.Count; i++)
        {
            CritBuff buff = critBuffs[i];
            _totalCritChance = buff.percent;

            // Decrement the number of turns left for the buff; if it has expired, remove it from the list
            buff.turnsLeft--;
            if (buff.turnsLeft <= 0)
            {
                critBuffs.RemoveAt(i);
                i--;
            }
        }
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

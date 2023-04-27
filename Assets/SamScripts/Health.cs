using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Health : MonoBehaviour, IDamageable
{
    public enum UnitType
    {
        Player,
        Enemy
    }

    [Header("Component")]
    [SerializeField] CentralManager centralManager;

    [Header("Unit Data")]
    [SerializeField] public UnitType unitType;
    [SerializeField] public int _maxHP = 10;
    [SerializeField] public int _curHP = 10;
    [SerializeField] public int _maxDef = 10;
    [SerializeField] public int _curDef = 0;

    private PlayerController playerController;
    private PlayerStats playerStats;
    private CardEvent_Enemy enemy;
    private CardEvent_Enemy enemyStats;

    private List<Regen> regenList = new List<Regen>();
    private bool _critOccured;

    void Awake()
    {
        _curHP = _maxHP;
    }

    void Start()
    {
        playerController = centralManager._playerController;
        playerStats = centralManager._playerStats;
        enemy = GetComponent<CardEvent_Enemy>();
        if(playerController != null)
        {
            DontDestroyOnLoad(this);
        }

    }

    public void TakeDamage(int dmg)
    {
        // Identifies if gameObject is an enemy or player
        if (unitType == UnitType.Enemy) StartCoroutine(EnemyTakeDamage(dmg));
        else if (unitType == UnitType.Player) PlayerTakeDamage(dmg);

    }


    private void PlayerTakeDamage(int dmg)
    {
        // Reduces dmg by player defense
        if (playerController != null) dmg -= playerStats._totalPlayerDefense;

        if (_curDef >= 1)
        {
            centralManager._sfxPlayer.Audio_DmgShield();
            _curDef -= dmg;
            if (_curDef < 0) _curDef = 0;
            centralManager._playerHUD.ShieldCalc();
            playerController._hurtShield = true;
        }
        else
        {
            _curHP -= dmg;
            if (_curHP < 0) _curHP = 0;
            centralManager._playerHUD.HealthCalc();

            if (_curHP <= 0)
            {
                Kill();
                return;
            }

            if (_critOccured)
            {
                _critOccured = false;
                centralManager._sfxPlayer.Audio_DmgCrit();
                playerController._hurtCrit = true;
            }
            else
            {
                centralManager._sfxPlayer.Audio_DmgHealth();
                playerController._hurt = true;
            }
        }

    }

    private IEnumerator EnemyTakeDamage(int dmg)
    {
        Debug.Log(playerStats._totalNumberOfAttacks);
        for (int i = 0; i < playerStats._totalNumberOfAttacks; i++)
        {
            // Calculate if dmg inflicted was a critical hit
            float randomValue = Random.Range(0f, 1f);
            if (playerStats._totalCritChance > randomValue)
            {
                dmg = Mathf.RoundToInt(dmg * playerStats._CritMultiplier);
                _critOccured = true;
            }

            // Deal dmg to shield if any; deal dmg to HP if pierce enabled or no shield remaining
            if (_curDef >= 1 && !playerStats._pierce)
            {
                if (playerStats._heavy) dmg = Mathf.RoundToInt(dmg * playerStats._heavyMultiplier);

                centralManager._sfxPlayer.Audio_DmgShield();
                _curDef -= dmg;
                if (_curDef < 0) _curDef = 0;
                // centralManager._enemyHUD.Shieldcalc();
                enemy._hurtShield = true;
            }
            else
            {
                if (playerStats._sharp) dmg = Mathf.RoundToInt(dmg * playerStats._sharpMultiplier);

                _curHP -= dmg;
                if (_curHP < 0) _curHP = 0;
                // centralManager._enemyHUD.HealthCalc();

                if (_curHP <= 0)
                {
                    Kill();
                    break;
                }

                if (_critOccured)
                {
                    _critOccured = false;
                    centralManager._sfxPlayer.Audio_DmgCrit();
                    enemy._hurtCrit = true;
                }
                else
                {
                    centralManager._sfxPlayer.Audio_DmgHealth();
                    enemy._hurt = true;
                }

            }

            yield return new WaitForSeconds(0.75f);

        }

    }

    public void Kill()
    {
        if (unitType == UnitType.Player)
        {
            playerController._killed = true;
            // centralManager._enemyHUD.HealthCalc();
        }
        else if (unitType == UnitType.Enemy) 
        {
            StartCoroutine(EnemyDeathWaitTimer(1));
            enemy._killed = true;
            centralManager._playerHUD.HealthCalc();
            enemy.EndEvent(this.gameObject);
        }
        centralManager._sfxPlayer.Audio_Death();

    }

    public void HealHP(int value)
    {
        _curHP += value;
        if (_curHP > _maxHP) _curHP = _maxHP;

        if (unitType == UnitType.Player) centralManager._playerHUD.HealthCalc();
        else if (unitType == UnitType.Enemy) ; // centralManager._enemyHUD.HealthCalc();

        centralManager._sfxPlayer.Audio_Heal();
    }
            
    IEnumerator EnemyDeathWaitTimer(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        //play death animation
    }

    public void HealSH(int value)
    {
        _curDef += value;

        if (_curDef > _maxDef) _curDef = _maxDef;

        if (unitType == UnitType.Player) centralManager._playerHUD.ShieldCalc();
        else if (unitType == UnitType.Enemy) ; // centralManager._enemyHUD.ShieldCalc();

        centralManager._sfxPlayer.Audio_ShieldUp();
    }

    public void IncreaseMaxHP(int value)
    {
        _maxHP += value;

        if (_curHP > _maxHP) _curHP = _maxHP;

        if (unitType == UnitType.Player) centralManager._playerHUD.HealthCalc();
        else if (unitType == UnitType.Enemy) ; // centralManager._enemyHUD.HealthCalc();

        centralManager._sfxPlayer.Audio_Heal();
    }

    public void IncreaseMaxShield(int value)
    {
        _maxDef += value;

        if (_curDef > _maxDef) _curDef = _maxDef;

        if (unitType == UnitType.Player) centralManager._playerHUD.ShieldCalc();
        else if (unitType == UnitType.Enemy) ; // centralManager._enemyHUD.ShieldCalc();

        centralManager._sfxPlayer.Audio_ShieldUp();
    }

    #region Regen Methods

    public void AddRegen(Vector2 regenValue, int regenTurns)
    {
        // Create a new regen object and add it to the list of regen effects
        Regen newRegen = new Regen(regenValue, regenTurns);
        regenList.Add(newRegen);

        centralManager._sfxPlayer.Audio_Heal();
    }

    public void RunRegenTurns()
    {
        // Loop through all regen effects and apply them to the player's health
        for (int i = 0; i < regenList.Count; i++)
        {
            Regen regen = regenList[i];
            int amount = Random.Range((int)regen.value.x, (int)regen.value.y + 1);
            HealHP(amount);

            // Decrement the number of turns left for the regen effect; if it has expired, remove it from the list
            regen.turnsLeft--;
            if (regen.turnsLeft <= 0)
            {
                regenList.RemoveAt(i);
                i--;
            }
        }
        centralManager._playerHUD.HealthCalc();
    }

    private class Regen
    {
        public Vector2 value;
        public int turnsLeft;

        public Regen(Vector2 value, int turnsLeft)
        {
            this.value = value;
            this.turnsLeft = turnsLeft;
        }
    }

    #endregion


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent_Enemy : IEvent
{
    private GameController _controller;
    [Header("Combat Values")]
    [SerializeField] public bool active;
    [Range(1, 25)] [SerializeField] public int _minAttackRange = 1;
    [Range(1, 25)] [SerializeField] public int _maxAttackRange = 1;

    // Bools for player animator; All auto disable when turned true except for _lowHP & _dead
    // Don't change _dead bool directly; use _killed instead
    // public bool _appear;
    public bool _killed;
    public bool _hurt;
    public bool _hurtCrit;
    public bool _hurtShield;
    // public bool _shield;
    // public bool _healed;
    // public bool _lowHP;
    public bool _dead;

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        centralManager = FindObjectOfType<CentralManager>();
        _controller = centralManager._gameController;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initiate Combat State
    public override void PlayEvent()
    {
        centralManager._sfxPlayer.Audio_EnemyEncounter();
        centralManager._enemyHealth = health;
        centralManager._enemyController = this;

        centralManager._enemyHUD.SetHUDName(GetComponent<MainCard>().ReturnCardName()); ;
        centralManager._enemyHUD._enemyHealth = health;
        centralManager._enemyHUD._enemyStats = this;

        _controller._enemyHealth = health;
        _controller._enemyStats = this;

        //recalculates the new enemies stats
        centralManager._enemyHUD.HealthCalc();
        centralManager._enemyHUD.ShieldCalc();
        centralManager._enemyHUD.SetAttackValues();

        // Might have to change some code below to optomize the game (specifically FindObjectOfType; they take a lot of power)
        GameFSM _gameController = (GameFSM)FindObjectOfType(typeof(GameFSM));
        _gameController.ChangeToBattle();
    }

    public override void EndEvent(GameObject mainCard)
    {
        centralManager._deckManager.AddRandomCard();
        base.EndEvent(mainCard);
    }

}

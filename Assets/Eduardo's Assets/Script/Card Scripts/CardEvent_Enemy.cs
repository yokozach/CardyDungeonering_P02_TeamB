using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent_Enemy : IEvent
{
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

        // Might have to change some code below to optomize the game (specifically FindObjectOfType; they take a lot of power)
        GameFSM _gameController = (GameFSM)FindObjectOfType(typeof(GameFSM));
        GameController _controller = (GameController)FindObjectOfType(typeof(GameController));
        _controller._enemyHealth = health;
        _controller._enemyStats = this;

        _gameController.ChangeToBattle();
    }

    public override void EndEvent(GameObject mainCard)
    {
        centralManager._deckManager.AddRandomCard();
        base.EndEvent(mainCard);
    }

    public Vector2Int AttackRange
    {
        get { return new Vector2Int(_minAttackRange, _maxAttackRange); }
    }



}

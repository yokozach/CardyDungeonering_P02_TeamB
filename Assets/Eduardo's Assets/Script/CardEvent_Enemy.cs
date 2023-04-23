using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent_Enemy : IEvent
{
    [Header("Combat Values")]
    [SerializeField] public bool active;
    [Range(1, 25)] [SerializeField] private int _minAttackRange = 1;
    [Range(1, 25)] [SerializeField] private int _maxAttackRange = 1;

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
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initiate Combat State
    public override void PlayEvent()
    {

    }

    public Vector2Int AttackRange
    {
        get { return new Vector2Int(_minAttackRange, _maxAttackRange); }
    }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] string _enemyName;
    private CardEvent_Enemy _enemy;
    private Animator _anim;
    private SpriteRenderer _renderer;

    private float _lockedTill;

    private void Awake()
    {
        _enemy = GetComponentInParent<CardEvent_Enemy>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var state = GetState();

        // _enemy._appear = false;
        _enemy._killed = false;
        _enemy._hurtShield = false;
        _enemy._hurt = false;
        _enemy._hurtCrit = false;
        // _enemy._shield = false;
        // _enemy._healed = false;

        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities

        if (_enemy._dead) return GetAnimHash("Dead");

        if (_enemy._killed)
        {
            _enemy._dead = true;
            return LockState(GetAnimHash("Vanish"), 0.7f);
        }

        // if (_enemy._appear) return LockState(GetAnimHash("Appear"), ___);

        if (_enemy._hurtShield) return LockState(GetAnimHash("HurtShield"), 0.5f);

        if (_enemy._hurt) return LockState(GetAnimHash("Hurt"), 0.4f);

        if (_enemy._hurtCrit) return LockState(GetAnimHash("HurtCrit"), 0.69f);

        // if (_enemy._shield) return LockState(GetAnimHash("ShieldUp"), ___);

        // if (_enemy._healed) return LockState(GetAnimHash("Heal"), ___);

        //if (_enemy._lowHP) return GetAnimHash("LowHP");

        if (_enemy.active) return GetAnimHash("Unstable");
        else return GetAnimHash("Idle");

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    private int GetAnimHash(string stateName)
    {
        return Animator.StringToHash(string.Format("{0}_{1}", _enemyName, stateName));
    }

    #region Cached Properties

    private int _currentState;

    //private static readonly int Idle = Animator.StringToHash("Enemy_Idle");
    //private static readonly int Unstable = Animator.StringToHash("Enemy_Unstable");
    //private static readonly int Hurt = Animator.StringToHash("Enemy_Hurt");
    //private static readonly int HurtCrit = Animator.StringToHash("Enemy_HurtCrit");
    //private static readonly int HurtShield = Animator.StringToHash("Enemy_HurtShield");
    //private static readonly int Heal = Animator.StringToHash("Enemy_Heal");
    //private static readonly int Vanish = Animator.StringToHash("Enemy_Vanish");
    //private static readonly int Appear = Animator.StringToHash("Enemy_Appear");
    //private static readonly int LowHP = Animator.StringToHash("Enemy_LowHP");
    //private static readonly int ShieldUp = Animator.StringToHash("Enemy_ShieldUp");
    //private static readonly int Dead = Animator.StringToHash("Enemy_Dead");

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private PlayerController _player;
    // private Health playerHP;
    private Animator _anim;
    private SpriteRenderer _renderer;

    private float _lockedTill;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
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

        _player._entering = false;

        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities
        if (_player._killed) return Explode;

        if (_player._entering) return LockState(Enter, 1.5f);

        if (_player._hurt)
        {
            // Add method to read attack dmg type to use normal hurt, crit hurt, or shield hurt.
            return Hurt;
        }

        if (_player._shield) return ShieldUp;

        if (_player._healed) return Heal;

        if (_player.lowHP) return LowHP;

        if (_player.playerActive) return IdleMotion; 
        else return IdleStop;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int IdleStop = Animator.StringToHash("Player_Stop");
    private static readonly int IdleMotion = Animator.StringToHash("Player_Idle");
    private static readonly int Hurt = Animator.StringToHash("Player_Hurt");
    private static readonly int CritHurt = Animator.StringToHash("Player_CritHurt");
    private static readonly int HurtShield = Animator.StringToHash("Player_ShieldHurt");
    private static readonly int Heal = Animator.StringToHash("Player_Heal");
    private static readonly int Explode = Animator.StringToHash("Player_Explode");
    private static readonly int Enter = Animator.StringToHash("Player_Enter");
    private static readonly int Appear = Animator.StringToHash("Player_Appear");
    private static readonly int LowHP = Animator.StringToHash("Player_LowHP");
    private static readonly int ShieldUp = Animator.StringToHash("Player_ShieldUp");
    private static readonly int Teleport = Animator.StringToHash("Player_Teleport");

    #endregion

}
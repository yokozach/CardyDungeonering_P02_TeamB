using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private PlayerController _player;
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
        _player._exiting = false;
        _player._appear = false;
        _player._teleport = false;
        _player._killed = false;
        _player._hurtShield = false;
        _player._hurt = false;
        _player._hurtCrit = false;
        _player._shield = false;
        _player._healed = false;

        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities

        if (_player._dead) return Dead;

        if (_player._killed)
        {
            _player._dead = true;
            return LockState(Explode, 0.8f);
        }

        if (_player._entering)
        {
            _player._exited = false;
            return LockState(Enter, 1.1f);
        }

        if (_player._exited) return Exited;

        if (_player._exiting) 
        { 
            _player._exited = true;
            return LockState(Exit, 1f);
        }

        if (_player._appear) return LockState(Appear, 0.5f);
        
        if (_player._teleport) return LockState(Teleport, 0.5f);

        if (_player._hurtShield) return LockState(HurtShield, 0.5f);

        if (_player._hurt) return LockState(Hurt, 0.5f);

        if (_player._hurtCrit) return LockState(HurtCrit, 0.9f);

        if (_player._shield) return LockState(ShieldUp, 0.8f);

        if (_player._healed) return LockState(Heal, 0.8f);

        if (_player._lowHP) return LowHP;

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

    private static readonly int IdleStop = Animator.StringToHash("Player_IdleStop");
    private static readonly int IdleMotion = Animator.StringToHash("Player_Idle");
    private static readonly int Hurt = Animator.StringToHash("Player_Hurt");
    private static readonly int HurtCrit = Animator.StringToHash("Player_HurtCrit");
    private static readonly int HurtShield = Animator.StringToHash("Player_HurtShield");
    private static readonly int Heal = Animator.StringToHash("Player_Heal");
    private static readonly int Explode = Animator.StringToHash("Player_Explode");
    private static readonly int Enter = Animator.StringToHash("Player_Enter");
    private static readonly int Exit = Animator.StringToHash("Player_Exit");
    private static readonly int Exited = Animator.StringToHash("Player_Exited");
    private static readonly int Appear = Animator.StringToHash("Player_Appear");
    private static readonly int LowHP = Animator.StringToHash("Player_LowHP");
    private static readonly int ShieldUp = Animator.StringToHash("Player_ShieldUp");
    private static readonly int Teleport = Animator.StringToHash("Player_Teleport");
    private static readonly int Dead = Animator.StringToHash("Player_Dead");

    #endregion

}
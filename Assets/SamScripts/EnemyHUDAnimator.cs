using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUDAnimator : MonoBehaviour
{
    private Animator _anim;

    private float _lockedTill;
    private bool enter = false;
    private bool idle = false;
    private bool exit = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        var state = GetState();

        enter = false;
        exit = false;

        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }
    public void ToggleDisplay()
    {
        if (Time.time == _lockedTill) return;

        if (idle) exit = true;
        else enter = true;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities

        if (enter)
        {
            idle = true;
            return LockState(Enter, 0.83f);
        }

        if (exit)
        {
            idle = false;
            return LockState(Exit, 0.83f);
        }

        if (idle) return Active;
        else
        {
            return Inactive;
        }

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Enter = Animator.StringToHash("EnemyHUDDropIn");
    private static readonly int Active = Animator.StringToHash("Active");
    private static readonly int Exit = Animator.StringToHash("EnemyHUDDropOut");
    private static readonly int Inactive = Animator.StringToHash("Idle");

    #endregion
}

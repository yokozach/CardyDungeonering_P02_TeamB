using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimator : MonoBehaviour
{

    private Animator _anim;

    private float _lockedTill;
    private bool open = false;
    private bool close = false;
    private bool disappear = false;
    private bool gone = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var state = GetState();

        open = false;

        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    public void OpenChest()
    {
        open = true;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities

        if (open)
        {
            close = true;
            return LockState(Open, 1.1f);
        }

        if (close)
        {
            close = false;
            disappear = true;
            return LockState(Close, 1.1f);
        }

        if (disappear)
        {
            disappear = false;
            gone = true;
            return LockState(Disappear, 0.5f);
        }

        if (gone) return Gone;
        else return Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }

    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Chest_Idle");
    private static readonly int Open = Animator.StringToHash("Chest_Open");
    private static readonly int Close = Animator.StringToHash("Chest_Close");
    private static readonly int Disappear = Animator.StringToHash("Chest_Disappear");
    private static readonly int Gone = Animator.StringToHash("Chest_Gone");

    #endregion

}

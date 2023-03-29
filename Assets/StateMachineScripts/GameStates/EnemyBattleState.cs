using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public EnemyBattleState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Enemy Battle State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        if (StateDuration >= 2.5f)
        {
            _stateMachine.ChangeState(_stateMachine.WinState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public TransitionState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Transition State");
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
    }
}

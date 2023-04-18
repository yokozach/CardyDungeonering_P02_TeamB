using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseCardState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public PlayerChooseCardState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Player Choose a Card State");
        _controller._playerController.playerActive = true;
    }

    public override void Exit()
    {
        base.Exit();
        _controller._playerController.playerActive = false;
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

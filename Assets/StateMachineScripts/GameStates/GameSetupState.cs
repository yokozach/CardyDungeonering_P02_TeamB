using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    //contrstuctor
    public GameSetupState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        //TODO Put what we need for setup
        Debug.Log("Entering Setup State");
        _controller._playerAttackButton.SetActive(false);
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        if (StateDuration >= 2f)
        {
            _stateMachine.ChangeState(_stateMachine.PlayerChooseCardState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

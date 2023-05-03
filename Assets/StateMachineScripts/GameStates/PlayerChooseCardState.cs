using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseCardState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private bool _canMoveYet = false;
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
        if(_controller._stairs._enemiesNeededToWin == _controller._enemiesDefeated && _controller._stairs._revealed)
        {
            _controller._nextFloorButton.SetActive(true);
        }
    }

    public override void Exit()
    {
        Debug.Log("Leaving Choose Card");
        _controller._playerController.SetPlayerActiveState(false);
        _canMoveYet = false;
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        if(StateDuration >= 2f && _canMoveYet == false)
        {
            _controller._playerController.SetPlayerActiveState(true);
            _canMoveYet = true;
        }
    }
}

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
        _controller._playerController.SetPlayerActiveState(true);
    }

    public override void Exit()
    {
        Debug.Log("Leaving");
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        if(_controller._whatEvent1._battleStart == true || _controller._whatEvent2._battleStart == true || _controller._whatEvent3._battleStart == true)
        {
            Debug.Log("Switch to battle state");
            _controller._playerController.SetPlayerActiveState(false);
            _stateMachine.ChangeState(_stateMachine.PlayerBattleState);
        }
        if(_controller._whatEvent4._stairs == true)
        {
            Debug.Log("swtich to win state");
            _controller._playerController.SetPlayerActiveState(false);
            _stateMachine.ChangeState(_stateMachine.WinState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public WinState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Win State");
        _controller._youWin.SetActive(true);
        _controller._returnToMainMenuButton.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _controller._youLose.SetActive(false);
        _controller._returnToMainMenuButton.SetActive(false);
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

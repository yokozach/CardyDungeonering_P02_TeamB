using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    public int _enemyHealth;
    public PlayerStats _player;
    public PlayerBattleState(GameFSM stateMachine, GameController controller)
    {
        //hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Player Battle State");
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
        //Check if Enemy health is <= 0 then switch states if true
        if(_enemyHealth <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.PlayerChooseCardState);
        }
    }

    public void PlayerAttack()
    {
        _enemyHealth -= _player._playerAttack;
        _stateMachine.ChangeState(_stateMachine.EnemyBattleState);
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    public int _enemyHealth = 100;
    public PlayerStats _player;
    private int _battleTurn = 0;
    public GoblinEvent _currentEnemy;
    
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
        if (_battleTurn == 0)
        {
            //Find Current Enemy
            if(_controller._whatEvent1._battleStart)
            {
                _currentEnemy = _controller._goblin1;
            }
            else if(_controller._whatEvent2._battleStart)
            {
                _currentEnemy = _controller._goblin2;
            }
            else if (_controller._whatEvent3._battleStart)
            {
                _currentEnemy = _controller._goblin3;
            }

        }
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
        if(_currentEnemy.enemyHP <= 0)
        {
            _battleTurn = 0;
            _controller._whatEvent4._enemiesDefeated++;
            _stateMachine.ChangeState(_stateMachine.PlayerChooseCardState);
        }
        
        //Check if Player Tapped then put Player Attack here 
        if(_controller._checkTouched._touched == true)
        {
            PlayerAttack();
        }
    }

    public void PlayerAttack()
    {
        Debug.Log("Attacked");
        _currentEnemy.damage();
        _battleTurn++;
        _stateMachine.ChangeState(_stateMachine.EnemyBattleState);
    } 
}

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
        _controller._enemyTurnImage.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _controller._enemyTurnImage.SetActive(false);
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        //Check if player health <= 0 and if true switch state to lose state
        if(_controller._playerHP._curHP <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.LoseState);
        }

        if(_controller._enemyHealth._curHP <= 0)
        {
            Debug.Log("enemyDefeated in enemyState");
            //_controller._battleTurn = 0;
            //_controller._enemiesDefeated++;
            if (_controller._enemiesDefeated >= _controller._stairs._enemiesNeededToWin)
            {
                _controller._stairs.ActivateStairs();
            }
            _stateMachine.ChangeState(_stateMachine.PlayerBattleState);
        }

        //Enemy Attack After thinking 
        else if (StateDuration >= 2.5f)
        {
            EnemyBasicAttack();
        }
    }

    public void EnemyBasicAttack()
    {
        Debug.Log("EnemyAttack");
        int _damage = Random.Range(_controller._enemyStats._minAttackRange, _controller._enemyStats._maxAttackRange + 1);
        _controller._playerHP.TakeDamage(_damage);
        _stateMachine.ChangeState(_stateMachine.PlayerBattleState);
    }
}

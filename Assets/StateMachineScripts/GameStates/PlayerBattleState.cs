using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    public Health _enemyHealth;
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

        _controller._playerHP.RunRegenTurns();

        _controller._playerAttackButton.SetActive(true);
        _controller._playerTurnImage.SetActive(true);
        if (_controller._battleTurn == 0)
        {
            //Find Current Enemy
            _enemyHealth = _controller._enemyHealth;
            _controller._cam.SetTarget2(_enemyHealth.gameObject);
            _controller._cam.ToggleFocus();

            _controller._enemyHUD.SetActive(true);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _controller._playerAttackButton.SetActive(false);
        _controller._playerTurnImage.SetActive(false);
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        //Check if Enemy health is <= 0 then switch states if true
        if(_enemyHealth._curHP <= 0)
        {
            _controller._enemyHUD.SetActive(false);
            _controller._battleTurn = 0;
            _controller._enemiesDefeated++;
            Debug.Log("enemyDefeated in playerState");
            Debug.Log("Cur Enemies Defeated: " + _controller._enemiesDefeated);
            Debug.Log("Needed AMount: " + _controller._stairs._enemiesNeededToWin);
            if (_controller._enemiesDefeated >= _controller._stairs._enemiesNeededToWin)
            {
                _controller._stairs.ActivateStairs();
            }
            _controller._cam.ToggleFocus();
            _controller._cam.SetTarget2(null);
            _stateMachine.ChangeState(_stateMachine.PlayerChooseCardState);
        }
        if(_controller._playerHP._curHP <= 0)
        {
            Debug.Log("Player Died");
            _controller._cam.SetTarget1(_enemyHealth.gameObject);
            _stateMachine.ChangeState(_stateMachine.LoseState);
        }
    }

    public void PlayerAttack()
    {
        Debug.Log("Attacked");
        _enemyHealth.TakeDamage(_controller._playerStats._totalPlayerAttack);
        _controller._battleTurn++;
        if (_enemyHealth._curHP > 0)
        {
            _stateMachine.ChangeState(_stateMachine.EnemyBattleState);
        }
    } 
}

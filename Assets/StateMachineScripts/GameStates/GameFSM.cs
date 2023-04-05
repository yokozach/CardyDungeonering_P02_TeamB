using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;

    //state variables here
    public GameSetupState SetupState { get; private set; }
    public PlayerChooseCardState PlayerChooseCardState { get; private set; }
    public PlayerBattleState PlayerBattleState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public EnemyBattleState EnemyBattleState { get; private set; }
    public TransitionState TransitionState { get; private set; }
 
    private void Awake()
    {
        _controller = GetComponent<GameController>();
        //state instantiation here
        SetupState = new GameSetupState(this, _controller);
        PlayerChooseCardState = new PlayerChooseCardState(this, _controller);
        PlayerBattleState = new PlayerBattleState(this, _controller);
        WinState = new WinState(this, _controller);
        LoseState = new LoseState(this, _controller);
        EnemyBattleState = new EnemyBattleState(this, _controller);
        TransitionState = new TransitionState(this, _controller);
    }

    private void Start()
    {
        ChangeState(PlayerChooseCardState);
    }
}

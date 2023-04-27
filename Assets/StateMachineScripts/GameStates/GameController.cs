using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerController _playerController;
    [SerializeField] public Health _playerHP;
    public bool _inBattle = false;
    [SerializeField] public InputManager _checkTouched;
    [SerializeField] public GameObject _youWin = null;
    [SerializeField] public GameObject _youLose = null;
    [SerializeField] public CardEvent_Stairs _stairs = null;
    [SerializeField] public GameObject _playerAttackButton = null;
    [SerializeField] public GameObject _returnToMainMenuButton = null;
    [SerializeField] public GameObject _playerTurnImage = null;
    [SerializeField] public GameObject _enemyTurnImage = null;
    public Health _enemyHealth = null;
    public int _battleTurn = 0;
    public int _enemiesDefeated = 0;
}

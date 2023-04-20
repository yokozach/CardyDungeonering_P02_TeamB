using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerController _playerController;
    [SerializeField] public Health _playerHP;
    public bool _inBattle = false;
    [SerializeField] public InputManager _checkTouched;
    [SerializeField] public EventTile _whatEvent1;
    [SerializeField] public EventTile _whatEvent2;
    [SerializeField] public EventTile _whatEvent3;
    [SerializeField] public EventTile _whatEvent4;
    [SerializeField] public GoblinEvent _goblin1 = null;
    [SerializeField] public GoblinEvent _goblin2 = null;
    [SerializeField] public GoblinEvent _goblin3 = null;
    [SerializeField] public GameObject _youWin = null;
    [SerializeField] public GameObject _youLose = null;
}

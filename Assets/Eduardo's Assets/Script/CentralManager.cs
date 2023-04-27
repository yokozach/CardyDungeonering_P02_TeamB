using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    [Header("Cam")]
    public CameraController _camController;

    [Header("Managers")]
    public GameController _gameController;
    public GridManager _gridManager;
    public DeckManager _deckManager;
    public InputManager inputManager;

    [Header("Visual & Audio")]
    public PanelFader _introFade;
    public Background _background;
    public MusicPlayer _musicPlayer;
    public AudioClipPlayer_Floors _sfxPlayer;

    [Header("Player")]
    public Player_Hud _playerHUD;
    public PlayerController _playerController;
    public PlayerStats _playerStats;
    public InventoryController _inventoryController;

    [Header("Cards")]
    public CardEvent_Stairs _stairs;
    public List<CardEvent_Chest> _chests;
    public List<CardEvent_Event> _events;
    public List<CardEvent_Enemy> _enemies;

    private void Awake()
    {
        if (PlayerData.shareData)
        {
            Health playerHealth = _playerController.GetComponent<Health>();
            playerHealth._curHP = PlayerData.curHP;
            playerHealth._curDef = PlayerData.curShield;
            playerHealth._maxHP = PlayerData.maxHP;
            playerHealth._maxDef = PlayerData.maxShield;

            _playerStats._playerBaseAttack = PlayerData.baseAttack;
            _playerStats._playerBaseDefense = PlayerData.baseDefense;
            _playerStats._numberOfAttacks = PlayerData.numberOfAttacks;
            _playerStats._pierce = PlayerData.pierce;
            _playerStats._sharp = PlayerData.sharp;
            _playerStats._heavy = PlayerData.heavy;
            _playerStats._baseCritChance = PlayerData.baseCritChance;

            PlayerData.shareData = false;

        }

    }

}

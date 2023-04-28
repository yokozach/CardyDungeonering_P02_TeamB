using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI _cardDescriptionText;

    [Header("Player")]
    public Player_Hud _playerHUD;
    public PlayerController _playerController;
    public PlayerStats _playerStats;
    public InventoryController _inventoryController;

    [Header("Current Enemy")]
    // public Enemy_Hud _enemyHUD;
    public CardEvent_Enemy _enemyController;
    public Health _enemyHealth;

    [Header("Cards")]
    public CardEvent_Stairs _stairs;
    public List<CardEvent_Chest> _chests;
    public List<CardEvent_Event> _events;
    public List<CardEvent_Enemy> _enemies;

    private void Awake()
    {
        if (PlayerData.shareData)
        {
            PlayerData.shareData = false;

            // Sets Players inventory
            _deckManager.addCardsAtStart = 1;
            _deckManager.InstantiateDeck(PlayerData.deckNumInv);

            // Sets Player HP & SH
            Health playerHealth = _playerController.GetComponent<Health>();
            playerHealth._curHP = PlayerData.curHP;
            playerHealth._curDef = PlayerData.curShield;
            playerHealth._maxHP = PlayerData.maxHP;
            playerHealth._maxDef = PlayerData.maxShield;

            // Sets Player Combat Ability
            _playerStats._playerBaseAttack = PlayerData.baseAttack;
            _playerStats._playerBaseDefense = PlayerData.baseDefense;
            _playerStats._numberOfAttacks = PlayerData.numberOfAttacks;
            _playerStats._pierce = PlayerData.pierce;
            _playerStats._sharp = PlayerData.sharp;
            _playerStats._heavy = PlayerData.heavy;
            _playerStats._baseCritChance = PlayerData.baseCritChance;

        }

    }

}

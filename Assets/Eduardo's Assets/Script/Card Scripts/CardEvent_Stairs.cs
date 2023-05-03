using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEvent_Stairs : IEvent
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Sprite _litStairs;
    [SerializeField] Sprite _finalStairs;
    [SerializeField] public int _enemiesNeededToWin = 3;
    [SerializeField] bool _anotherScene;
    [SerializeField] SceneLoader _sceneManager = null;
    [SerializeField] string _nextScene = null;
    public bool _revealed;
    public bool _active;

    private Health playerHealth;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = centralManager._playerController.GetComponent<Health>();
        playerStats = centralManager._playerStats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Stairs Encountered
    public override void PlayEvent()
    {
        _revealed = true;
    }

    // Visually activates stairs & indicate it's now accessible by players
    public void ActivateStairs()
    {
        _active = true;
        if (_anotherScene) _renderer.sprite = _litStairs; else _renderer.sprite = _finalStairs;

    }

    // Check if mission is fulfilled; Next floor if done
    public void CheckMissionStatus()
    {
        if (_active == true)
        {
            if (_anotherScene && _nextScene != null)
            {
                // Add code to change state machine to next floor transition!
                if(_sceneManager != null)
                {
                    centralManager._playerController.playerActive = false;
                    centralManager._playerController._exiting = true;
                    StartCoroutine(centralManager._introFade.FadePanelIn());
                    PlayerData.StorePlayerData(playerHealth._curHP, playerHealth._curDef, playerHealth._maxHP, playerHealth._maxDef,
                        playerStats._baseAtt, playerStats._baseDef, playerStats._baseHit, playerStats._baseCrit, playerStats._basePierce, 
                        playerStats._baseSharp, playerStats._baseHeavy, centralManager._inventoryController.inventoryCards);
                    PlayerData.StoreOtherData(centralManager._deckManager.ReturnCurAvailableDeck(), centralManager._musicPlayer.ReturnLoopHistory());
                    StartCoroutine(StairsWaitTimer(1f));
                }
            }
            else
            {
                // Add code to change to win state
                GameFSM _gameController = (GameFSM)FindObjectOfType(typeof(GameFSM));
                _gameController.ChangeToWin();
            }
        }
        else
        {
            Debug.Log("Mission must be completed first!");
            // Update misison UI to flash & indicate mission must be completed!
        }
    }

    IEnumerator StairsWaitTimer(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        _sceneManager.LoadScene(_nextScene);
    }

}

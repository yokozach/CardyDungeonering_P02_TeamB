using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] public bool playerActive = false; // is player actionable?
    [SerializeField] public bool invOpen = false; // is player in inventory?
    [SerializeField] private Vector2Int startGridPos = new Vector2Int(2, 2); // Where on the grid the player starts
    [SerializeField] private float moveCooldown = 0.2f; // Cooldown for movement

    private float moveDis = 0.05f; // movement distance of player to move from tile to tile
    private float curCooldown; // Cooldown timer for movement
    private Vector2 curGridPos; // current grid tile player is on

    [Header("Player Controls")]
    [SerializeField] private InputAction move;

    private bool isMoving = false; // flag to check if the player is currently moving

    [Header("Components")]
    [SerializeField] private CentralManager centralManager; // Central Hub for significant components
    [SerializeField] private InputManager inputManager; // InputManager script

    private Health health;
    private PlayerAnimator playerAnim;

    // Bools for player animator; All auto disable when turned true except for _lowHP & _dead
    // Don't change _dead bool directly; use _killed instead
    public bool _entering;
    public bool _exiting;
    public bool _exited;
    public bool _appear;
    public bool _teleport;
    public bool _killed;
    public bool _hurt;
    public bool _hurtCrit;
    public bool _hurtShield;
    public bool _shield;
    public bool _healed;
    public bool _lowHP;
    public bool _dead;

    private void Awake()
    {
        move.Enable();
        move.performed += context => { StartCoroutine(MovePlayer(context.ReadValue<Vector2>())); };
        InputManager.instance.swipePerformed += context => { StartCoroutine(MovePlayer(context)); };
        curGridPos = startGridPos;

    }

    void Start()
    {
        // Find components in scene if not directly linked
        if (health == null) health = GetComponent<Health>();
        if (playerAnim == null) playerAnim = GetComponentInChildren<PlayerAnimator>();
        if (inputManager == null) inputManager = FindObjectOfType<InputManager>();

        moveDis = centralManager._gridManager.ReturnSpacing() / 30;

    }

    void Update()
    {
        if (curCooldown > 0)
            curCooldown -= Time.deltaTime;

    }

    public IEnumerator EnteringFloor(float wait, Vector2 startPos)
    {
        yield return new WaitForSeconds(wait);

        _entering = true;
        SetPlayerPos(startPos);

        yield return new WaitForSeconds(1.35f);
        playerActive = true;
        centralManager._camController.ToggleFocus();
        centralManager._camController.SetTarget1(gameObject);
        centralManager._camController.SetTarget2(null);
        centralManager._inventoryController.ToggleButtonActive();

    }

    bool CheckTile(Vector2 direction)
    {
        // Checks if the next tile exists within grid bounderies
        if (curGridPos.x + direction.x < 0 || curGridPos.x + direction.x > centralManager._gridManager.ReturnColCount() - 1 || curGridPos.y - direction.y < 0 || curGridPos.y - direction.y > centralManager._gridManager.ReturnRowCount() - 1)
        {
            Debug.Log("Next Tile Doesn't exist!");
            return true;
        }

        // Checks if next tile is occupied with a card
        if (centralManager._gridManager.ReturnTileDictionary()[curGridPos + new Vector2(direction.x, -direction.y)].ReturnCurrentCard() != null)
        {
            MainCard curMainCard = centralManager._gridManager.ReturnTileDictionary()[curGridPos + new Vector2(direction.x, -direction.y)].ReturnCurrentCard().GetComponent<MainCard>();
            if (curMainCard.ReturnEvent().ReturnType() == IEvent.TileType.Stairs && curMainCard.ReturnEvent())
            {
                if (curMainCard.ReturnStairs()._revealed == true)
                {
                    curMainCard.ReturnStairs().CheckMissionStatus();
                }
                else
                {
                    Debug.Log("Stairs Found");
                    curMainCard.Reveal();
                    return true;
                }
            }
            else
            {
                Debug.Log("Next Tile is Occupied!");
                curMainCard.Reveal();
                return true;
            }

        }

        return false;

    }

    private IEnumerator MovePlayer(Vector2 direction)
    {        
        if (isMoving || curCooldown > 0 || !playerActive || invOpen)
            yield break;

        // Checks if the tile attempted to move onto is empty, occupied, invalid, etc.
        if (CheckTile(direction))
            yield break;

        // Flag that player is currently moving & sets cooldown
        isMoving = true;
        curCooldown = moveCooldown;

        // Adjust Grid Position
        curGridPos += new Vector2(direction.x, -direction.y);

        // Plays Move Sfx
        centralManager._sfxPlayer.Audio_Step();

        // Animates movement of player towards desired tile
        for (int i=0; i<30; i++)
        {
            yield return null;
            transform.position += new Vector3(direction.x * moveDis, direction.y * moveDis, transform.position.z);
        }

        // Fixes player onto the tile they are supposed to be on
        Tile newTile = centralManager._gridManager.ReturnTileDictionary()[curGridPos];
        transform.position = newTile.transform.position;

        // Reset movement flag
        isMoving = false;

    }

    public Vector2 ReturnCurGridPos()
    {
        return curGridPos;
    }

    public void SetPlayerActiveState(bool state)
    {
        playerActive = state;
    }

    public void TogglePlayerActiveState()
    {
        if (playerActive) playerActive = false;
        else playerActive = true;
    }

    public void SetGridPos(int row, int col)
    {
        curGridPos = new Vector2(row, col);
    }

    public Vector2Int ReturnPlayerStartPos()
    {
        return startGridPos;
    }

    public void SetPlayerPos(Vector2 pos)
    {
        transform.position = pos;
    }

    public bool ReturnPlayerActive()
    {
        return playerActive;
    }

}

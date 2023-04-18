using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] public bool playerActive = true; // is player actionable?
    [SerializeField] private Vector2Int startGridPos = new Vector2Int(2, 2); // Where on the grid the player starts
    [SerializeField] private float moveCooldown = 0.2f; // Cooldown for movement

    private float moveDis = 0.05f; // movement distance of player to move from tile to tile
    private float curCooldown; // Cooldown timer for movement
    private Vector2 curGridPos; // current grid tile player is on

    [Header("Player Controls")]
    [SerializeField] private InputAction move;

    private bool isMoving = false; // flag to check if the player is currently moving

    [Header("Components")]
    [SerializeField] private InputManager inputManager; // InputManager script
    [SerializeField] private GridManager gridManager; // GridManager script

    private void Awake()
    {
        move.Enable();
        move.performed += context => { StartCoroutine(MovePlayer(context.ReadValue<Vector2>())); };
        InputManager.instance.swipePerformed += context => { StartCoroutine(MovePlayer(context)); };
    }

    void Start()
    {
        // Find components in scene if not directly linked
        if (inputManager == null) inputManager = FindObjectOfType<InputManager>();
        if (gridManager == null) gridManager = FindObjectOfType<GridManager>();

        curGridPos = startGridPos;
        moveDis = gridManager.ReturnSpacing() / 30;
        
    }

    void Update()
    {
        if (curCooldown > 0)
            curCooldown -= Time.deltaTime;
    }

    bool CheckTile(Vector2 direction)
    {
        // Checks if the next tile exists within grid bounderies
        if (curGridPos.x + direction.x < 0 || curGridPos.x + direction.x > gridManager.ReturnColCount() - 1 || curGridPos.y - direction.y < 0 || curGridPos.y - direction.y > gridManager.ReturnRowCount() - 1)
        {
            Debug.Log("Next Tile Doesn't exist!");
            return true;
        }

        // Checks if next tile is occupied with a card
        if (gridManager.ReturnTileDictionary()[curGridPos + new Vector2(direction.x, -direction.y)].ReturnCurrentCard() != null)
        {
            Debug.Log("Next Tile is Occupied!");
            gridManager.ReturnTileDictionary()[curGridPos + new Vector2(direction.x, -direction.y)].ReturnCurrentCard().GetComponent<MainCard>().ReturnOpenTile().Open();
            return true;
        }

        return false;

    }

    private IEnumerator MovePlayer(Vector2 direction)
    {        
        if (isMoving || curCooldown > 0 || !playerActive)
            yield break;

        // Checks if the tile attempted to move onto is empty, occupied, invalid, etc.
        if (CheckTile(direction))
            yield break;

        // Flag that player is currently moving & sets cooldown
        isMoving = true;
        curCooldown = moveCooldown;

        // Adjust Grid Position
        curGridPos += new Vector2(direction.x, -direction.y);

        // Animates movement of player towards desired tile
        for (int i=0; i<30; i++)
        {
            yield return null;
            transform.position += new Vector3(direction.x * moveDis, direction.y * moveDis, transform.position.z);
        }

        // Fixes player onto the tile they are supposed to be on
        Tile newTile = gridManager.ReturnTileDictionary()[curGridPos];
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

}

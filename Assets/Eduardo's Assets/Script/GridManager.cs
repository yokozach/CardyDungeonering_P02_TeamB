using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField] private int _colCount = 5; // Number of columns
    [SerializeField] private int _rowCount = 5; // Number of rows
    
    [SerializeField] private float _widthSpacing = 1, _heightSpacing = 1; // Spacing between each tile

    [SerializeField] private Tile _tilePrafab; // Tile Prefab

    [SerializeField] private PlayerController playerController;

    [Header("Set-up")]

    [SerializeField] private GameObject _emptyCardPrefab;
    [SerializeField] private List<MainCard> _cards;
    private MainCard _currentCard;

    private Dictionary<Vector2, Tile> _tiles;

    private float _currentWS;
    private float _currentHS;
    private Vector3 newOrigin;

    private void Start()
    {
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // If grid values are <0, make them at 1
        if (_colCount <= 0) _colCount = 1;
        if (_rowCount <= 0) _rowCount = 1;
        if (_widthSpacing <= 0) _widthSpacing = 1;
        if (_heightSpacing <= 0) _heightSpacing = 1;

        _currentWS = 0;
        _currentHS = 0;

        // Calculate where Grid Generator should be to produce grid along center origin
        _currentWS += (_widthSpacing * (_colCount-1) / -2);
        _currentHS += (_heightSpacing * (_rowCount-1) / 2);

        // Move Grid Generator accordingly
        newOrigin = new Vector3(_currentWS, _currentHS, 0);
        transform.position = newOrigin;

        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _colCount; x++)
        {
            for (int y = 0; y < _rowCount; y++)
            {
                var spawnedTile = Instantiate(_tilePrafab, new Vector3(_currentWS, _currentHS), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.init(isOffset);


                _tiles[new Vector2(x, y)] = spawnedTile;

                _currentHS -= _heightSpacing;

            }

            if (x != _colCount)
                _currentHS = newOrigin.y;

            _currentWS += _widthSpacing;

        }

        //_cam.transform.position = new Vector3((float)_currentHS / 2 - 0.5f, (float)_currentWS / 2 - 0.5f, -10);
        SetUpCards();

    }

    void SetUpCards()
    {
        for (int i=0; i < _cards.Count; i++)
        {
            _currentCard = _cards[i];
            Tile retrievedTile = _tiles[new Vector2(_currentCard.ReturnCardRow(), _currentCard.ReturnCardCol())];

            // Sets current card to the tile & moves it if unoccupied
            if (retrievedTile.ReturnCurrentCard() == null)
            {
                retrievedTile.SetCurrentCard(_currentCard.gameObject);
                _currentCard.gameObject.transform.position = new Vector3(retrievedTile.transform.position.x, retrievedTile.transform.position.y, 0);
            }
            else
            {
                Debug.Log($"Tile {_currentCard.ReturnCardRow()} {_currentCard.ReturnCardCol()} is already occupied.");
            }   
        }

        for (int x = 0; x < _colCount; x++)
        {
            for (int y = 0; y < _rowCount; y++)
            {
                if (playerController.ReturnCurGridPos() != new Vector2(x, y))
                {
                    Tile retrievedTile = _tiles[new Vector2(x, y)];

                    if (retrievedTile.ReturnCurrentCard() == null)
                    {
                        GameObject _currentEmptyCard = Instantiate(_emptyCardPrefab, new Vector3(retrievedTile.transform.position.x, retrievedTile.transform.position.y), Quaternion.identity);
                        retrievedTile.SetCurrentCard(_currentEmptyCard);
                    }

                }

            }
        }

            SetUpPlayer();
    }

    void SetUpPlayer()
    {
        Tile startTile = ReturnTileDictionary()[playerController.ReturnCurGridPos()];
        playerController.SetPlayerPos(startTile.transform.position);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public float ReturnSpacing()
    {
        return _widthSpacing;
    }

    public Dictionary<Vector2, Tile> ReturnTileDictionary()
    {
        return _tiles;
    }

    public int ReturnColCount()
    {
        return _colCount;
    }

    public int ReturnRowCount()
    {
        return _rowCount;
    }

}

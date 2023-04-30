using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CentralManager centralManager;

    [Header("Parameters")]
    [SerializeField] private int _colCount = 5; // Number of columns
    [SerializeField] private int _rowCount = 5; // Number of rows
    [SerializeField] private float _widthSpacing = 1, _heightSpacing = 1; // Spacing between each tile
    [SerializeField] private Tile _tilePrafab; // Tile Prefab

    [Header("Set-up")]
    [SerializeField] private GameObject _emptyCardPrefab;
    [SerializeField] private bool _randomized;
    [SerializeField] private List<Vector2Int> _blockedPos;

    [Header("Fixed Cards")]
    [SerializeField] private List<MainCard> _fixedCards;
    [SerializeField] private List<Vector2Int> _fixedGridPos;

    [Header("Variable Cards")]
    [SerializeField] private List<MainCard> _variableCards;
    [SerializeField] private List<Vector2Int> _variableGridPos;

    private MainCard _currentCard;
    private Dictionary<Vector2, Tile> _tiles;
    private List<Vector2Int> _savedBlockedPos = new List<Vector2Int>();

    private float _currentWS;
    private float _currentHS;
    private Vector3 newOrigin;

    private void Start()
    {
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

                // Check if the current position corresponds to a fixed card position
                bool isFixed = false;
                int fixedIndex = -1;
                for (int i = 0; i < _fixedGridPos.Count; i++)
                {
                    if (_fixedGridPos[i].x == x && _fixedGridPos[i].y == y)
                    {
                        isFixed = true;
                        fixedIndex = i;
                        break;
                    }
                }

                if (isFixed)
                {
                    // If this position is a fixed card position, assign the corresponding fixed card to this position
                    MainCard fixedCard = _fixedCards[fixedIndex];
                    spawnedTile.SetCurrentCard(fixedCard.gameObject);
                    fixedCard.gameObject.transform.position = new Vector3(spawnedTile.transform.position.x, spawnedTile.transform.position.y, 0);
                }

                _tiles[new Vector2(x, y)] = spawnedTile;

                _currentHS -= _heightSpacing;

            }

            if (x != _colCount)
                _currentHS = newOrigin.y;

            _currentWS += _widthSpacing;

        }

        //_cam.transform.position = new Vector3((float)_currentHS / 2 - 0.5f, (float)_currentWS / 2 - 0.5f, -10);
        FindObjectOfType<CameraController>().StartFloor();
        if (_randomized) SetUpCardsRandomly();
        else SetUpCardsFixed();
    }

    void SetUpCardsRandomly()
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        for (int x = 0; x < _colCount; x++)
        {
            for (int y = 0; y < _rowCount; y++)
            {
                if (centralManager._playerController.ReturnCurGridPos() != new Vector2Int(x, y))
                {
                    availablePositions.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < availablePositions.Count; i++)
        {
            for (int j = 0; j < _blockedPos.Count; j++)
            {
                if (availablePositions[i] == _blockedPos[j])
                {
                    _savedBlockedPos.Add(availablePositions[i]);
                    availablePositions.RemoveAt(i);
                }

            }
        }

        if (_randomized)
        {
            // Shuffle the list of available positions
            for (int i = availablePositions.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                Vector2Int temp = availablePositions[i];
                availablePositions[i] = availablePositions[j];
                availablePositions[j] = temp;
            }
        }

        for (int i = 0; i < _variableCards.Count; i++)
        {
            _currentCard = _variableCards[i];

            // Find an available position that is not occupied by the player
            int j = 0;
            bool positionFound = false;
            while (j < availablePositions.Count && !positionFound)
            {
                Vector2Int position = availablePositions[j];
                Tile retrievedTile = GetTileAtPosition(position);

                if (retrievedTile.ReturnCurrentCard() == null)
                {
                    // Position is available, so assign the current card to this position
                    retrievedTile.SetCurrentCard(_currentCard.gameObject);
                    _currentCard.gameObject.transform.position = new Vector3(retrievedTile.transform.position.x, retrievedTile.transform.position.y, 0);

                    // Remove this position from the list of available positions
                    availablePositions.RemoveAt(j);

                    positionFound = true;
                }
                else
                {
                    j++;
                }
            }

            if (!positionFound)
            {
                Debug.Log($"No available position found for {_currentCard}");
            }
        }

        for (int i = 0; i < _savedBlockedPos.Count; i++)
        {
            availablePositions.Add(_savedBlockedPos[i]);
        } 

        // Create empty cards for any remaining available positions
        foreach (Vector2Int position in availablePositions)
        {
            Tile retrievedTile = GetTileAtPosition(position);

            if (retrievedTile.ReturnCurrentCard() == null)
            {
                GameObject _currentEmptyCard = Instantiate(_emptyCardPrefab, new Vector3(retrievedTile.transform.position.x, retrievedTile.transform.position.y), Quaternion.identity);
                retrievedTile.SetCurrentCard(_currentEmptyCard);
            }
        }

        SetUpPlayer();
    }

    void SetUpCardsFixed()
    {
        for (int i = 0; i < _fixedCards.Count; i++)
        {
            _currentCard = _fixedCards[i];

            // Tile retrievedTile = _tiles[new Vector2(_currentCard.ReturnCardCol(), _currentCard.ReturnCardRow())];

            Tile retrievedTile = _tiles[_fixedGridPos[i]];

            // Sets current card to the tile & moves it if unoccupied
            if (retrievedTile.ReturnCurrentCard() == null)
            {
                retrievedTile.SetCurrentCard(_currentCard.gameObject);
                _currentCard.gameObject.transform.position = new Vector3(retrievedTile.transform.position.x, retrievedTile.transform.position.y, 0);
            }
            else
            {
                Debug.Log($"Tile {_fixedGridPos[i]} is already occupied, so {_currentCard} cannot be placed.");
            }
        }

        for (int i=0; i < _variableCards.Count; i++)
        {
            _currentCard = _variableCards[i];

            // Tile retrievedTile = _tiles[new Vector2(_currentCard.ReturnCardCol(), _currentCard.ReturnCardRow())];

            Tile retrievedTile = _tiles[_variableGridPos[i]];

            // Sets current card to the tile & moves it if unoccupied
            if (retrievedTile.ReturnCurrentCard() == null)
            {
                retrievedTile.SetCurrentCard(_currentCard.gameObject);
                _currentCard.gameObject.transform.position = new Vector3(retrievedTile.transform.position.x, retrievedTile.transform.position.y, 0);
            }
            else
            {
                Debug.Log($"Tile {_variableGridPos[i]} is already occupied, so {_currentCard} cannot be placed.");
            }   
        }

        for (int x = 0; x < _colCount; x++)
        {
            for (int y = 0; y < _rowCount; y++)
            {
                if (centralManager._playerController.ReturnCurGridPos() != new Vector2(x, y))
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
        Tile startTile = ReturnTileDictionary()[centralManager._playerController.ReturnCurGridPos()];
        StartCoroutine(centralManager._playerController.EnteringFloor(1.5f, startTile.transform.position));
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

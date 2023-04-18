using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField] private int _colCount;
    [SerializeField] private int _rowCount;
    
    [SerializeField] private float _widthSpacing = 1, _heightSpacing = 1;

    [SerializeField] private Tile _tilePrafab;

    [SerializeField] private Transform _cam;

    [Header("Set-up")]

    [SerializeField] private List<GameObject> _cards;
    private TempCard _currentCard;

    private Dictionary<Vector2, Tile> _tiles;

    private float _currentWS;
    private float _currentHS;

    private float _maxTilePosX;
    private float _maxTilePosY;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _currentWS = 0;
        _currentHS = 0;

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

                for (int i = 0; i < _cards.Count; i++)
                {
                    _currentCard = _cards[i].GetComponent<TempCard>();
                    if (_currentCard.ReturnCardRow() == x && _currentCard.ReturnCardCol() == y)
                    {
                        spawnedTile.SetCurrentCard(_cards[i]);
                    }
                }

                _currentHS += _heightSpacing;

                _maxTilePosX = spawnedTile.transform.position.x;
                _maxTilePosY = spawnedTile.transform.position.y;

            }

            if (x != _colCount)
                _currentHS = 0;

            _currentWS += _widthSpacing;

        }

        //_cam.transform.position = new Vector3((float)_currentHS / 2 - 0.5f, (float)_currentWS / 2 - 0.5f, -10);
        _cam.transform.position = new Vector3(_maxTilePosX/2, _maxTilePosY/2, -10);

    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }


}

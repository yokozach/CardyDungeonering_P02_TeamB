using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    /// <summary>
    /// Provide a name for the card, a column num, and a row num
    /// </summary>

    [Header("Card Data")]
    [SerializeField] private string _cardName = "CardName";

    [SerializeField] private int _cardCol = 0;
    [SerializeField] private int _cardRow = 0;

    private GameObject _cardObject;
    private OpenTile _openTile;

    private void Start()
    {
        _cardObject = GetComponent<GameObject>();
        _openTile = GetComponentInChildren<OpenTile>();
    }

    public void SetCardPosition(Vector2 newPosition)
    {
        _cardObject.transform.position = newPosition;
    }

    public void SetCardActive(bool state)
    {
        _cardObject.SetActive(state);
    }

    public int ReturnCardRow()
    {
        return _cardRow;
    }

    public int ReturnCardCol()
    {
        return _cardCol;
    }

    public OpenTile ReturnOpenTile()
    {
        return _openTile;
    }

}

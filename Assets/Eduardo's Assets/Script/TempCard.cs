using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCard : MonoBehaviour
{
    /// <summary>
    /// Following script is a temporary card holder script for tiles to hold.
    /// Provide a name for the card, a column num, and a row num
    /// </summary>

    [Header("Card Data")]
    [SerializeField] private string _cardName = "CardName";

    [SerializeField] private int _cardCol = 0;
    [SerializeField] private int _cardRow = 0;

    private GameObject _cardObject;

    private void Start()
    {
        _cardObject = GetComponent<GameObject>();
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

}

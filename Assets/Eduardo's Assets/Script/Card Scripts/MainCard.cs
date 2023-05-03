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
    [SerializeField] private IEvent _event;

    [Header("Card Visuals")]
    [SerializeField] private GameObject _cover;
    [SerializeField] private GameObject _face;

    private GameObject _cardObject;
    private CardEvent_Stairs _stairs;

    private void Start()
    {
        _cardObject = GetComponent<GameObject>();
        _event = GetComponent<IEvent>();

        _cover.SetActive(true);
        _face.SetActive(false);

        _stairs = GetComponent<CardEvent_Stairs>();

    }

    public void SetCardPosition(Vector2 newPosition)
    {
        _cardObject.transform.position = newPosition;
    }

    public void SetCardActive(bool state)
    {
        _cardObject.SetActive(state);
    }

    public void Reveal()
    {
        _event.PrepEvent(_face, _cover);

    }

    public string ReturnCardName()
    {
        return _cardName;
    }

    public IEvent ReturnEvent()
    {
        return _event;
    }

    public CardEvent_Stairs ReturnStairs()
    {
        return _stairs;
    }


}

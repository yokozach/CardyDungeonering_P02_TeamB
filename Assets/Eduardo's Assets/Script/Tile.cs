using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    [Header("Slot Data")]
    [SerializeField] private GameObject _currentCard;
    [SerializeField] private bool _revealed;

    public void init(bool isOffset) 
    {
        _renderer.color = isOffset ? _offsetColor: _baseColor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetCurrentCard(GameObject assignedCard)
    {
        _currentCard = assignedCard;
    }

    public GameObject ReturnCurrentCard()
    {
        return _currentCard;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InvCard : MonoBehaviour
{
    public enum CardType
    {
        Attack,
        Healing,
        Technique,
        Buff,
        Explorative
    }

    [Header("Card Data")]
    public string cardName;
    public CardType cardType;
    public int deckNumber;
    public string cardDescription;

    [Header("Animation")]
    public float animationSpeed = 1.5f; // speed multiplier for the animation
    public bool isMoving = false; // If card is animated moving

    private RectTransform rectTransform; // reference to the RectTransform component of the card
    private Vector2 originalPosition; // store the original position of the card
    private Vector3 originalScale = new Vector3(1, 1, 1); // original scale of the card
    private Vector2 centerPosition; // position to move the card to when selected
    private Vector3 centerScale = new Vector3(3, 3, 3); // scale to use when the card is selected

    private GameObject invDeck;
    private bool isSelected = false; // flag to track if the card is currently selected

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // get the RectTransform component of the card
        
    }

    private void Start()
    {

    }

    public void ToggleSelect()
    {
        if (invDeck == null) invDeck = transform.parent.gameObject; // Get inventory holder (parent)

        if (isMoving) return;

        if (isSelected)
        {
            // if the card is already selected, deselect it
            StartCoroutine(MoveCard(originalPosition, originalScale));
            invDeck.transform.parent.GetComponent<InventoryController>().SetSelectedCard(null);
            invDeck.transform.parent.GetComponent<InventoryController>().DeactivateSelectPanel();
            isSelected = false;
        }
        else
        {
            // if the card is not selected, select it
            StartCoroutine(MoveCard(centerPosition, centerScale));
            invDeck.transform.parent.GetComponent<InventoryController>().SetSelectedCard(gameObject);
            invDeck.transform.parent.GetComponent<InventoryController>().ActivateSelectPanel();
            ReorderCards();
            isSelected = true;
        }
    }

    public void SelectCard()
    {
        if (invDeck == null) invDeck = transform.parent.gameObject; // Get inventory holder (parent)

        if (isMoving) return;

        if (!isSelected)
        {
            // if the card is not selected, select it
            StartCoroutine(MoveCard(centerPosition, centerScale));
            invDeck.transform.parent.GetComponent<InventoryController>().SetSelectedCard(gameObject);
            invDeck.transform.parent.GetComponent<InventoryController>().ActivateSelectPanel();
            ReorderCards();
            isSelected = true;
        }
    }

    public void SetInventoryOff()
    {
        rectTransform.anchoredPosition = originalPosition;
        transform.localScale = originalScale;
        isSelected = false;
    }

    public virtual void UseSelectedCard()
    {
        ActivateCardEffects();
    }

    public abstract void ActivateCardEffects();

    private IEnumerator MoveCard(Vector2 targetPosition, Vector3 targetScale)
    {
        isMoving = true;

        float moveTime = 0.5f; // time to move the card, in seconds
        float elapsedTime = 0f; // elapsed time since the movement started
        Vector2 startingPosition = rectTransform.anchoredPosition; // starting position of the card
        Vector3 startingScale = transform.localScale; // starting scale of the card

        while (elapsedTime < moveTime)
        {
            // calculate the new position and scale of the card using Lerp
            Vector2 newPosition = Vector2.Lerp(startingPosition, targetPosition, elapsedTime / moveTime * animationSpeed);
            Vector3 newScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / moveTime * animationSpeed);
            rectTransform.anchoredPosition = newPosition;
            transform.localScale = newScale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // make sure the card is precisely at the target position and scale
        rectTransform.anchoredPosition = targetPosition;
        transform.localScale = targetScale;

        if (!isSelected && targetPosition == originalPosition)
        {
            // if the card is not selected and has been moved back to its original position, update the original position and scale
            originalPosition = rectTransform.anchoredPosition;
            originalScale = transform.localScale;
        }

        isMoving = false;
    }

    private void ReorderCards()
    {
        transform.SetAsLastSibling();
    }

    public void SetNewOriginalPosition(Vector2 newPosition)
    {
        originalPosition = newPosition;
    }

    public void SetCenterPosition(Vector2 newCenterPos)
    {
        centerPosition = newCenterPos;
    }
}
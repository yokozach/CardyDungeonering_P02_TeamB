using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("Inventory Info")]
    [SerializeField] private Canvas cardCanvas;
    [SerializeField] public GameObject inventoryHolder;
    [SerializeField] private Button invBtn;
    [SerializeField] private GameObject currentSelectedCard;
    [SerializeField] public List<GameObject> inventoryCards = new List<GameObject>();
    [SerializeField] private List<Vector2> cardPositions = new List<Vector2>();
    [SerializeField] private int maxCapacity = 10;

    [Header("Other Components")]
    [SerializeField] PlayerController PC;
    public GameObject selectPanel;

    private void Start()
    {
        SortCardsByPosition();
    }

    public void AddCard(GameObject card)
    {
        if (inventoryCards.Count < maxCapacity)
        {
            inventoryCards.Add(card);
        }
    }

    public void RemoveCard(int index)
    {
        if (index >= 0 && index < inventoryCards.Count)
        {
            GameObject card = inventoryCards[index];
            inventoryCards.RemoveAt(index);
            Destroy(card);
        }
    }

    public void SortCardsByPosition()
    {
        for (int i = 0; i < Mathf.Min(inventoryCards.Count, cardPositions.Count); i++)
        {
            inventoryCards[i].GetComponent<InvCard>().SetNewOriginalPosition(cardPositions[i]);
            inventoryCards[i].transform.position = cardCanvas.transform.TransformPoint(cardPositions[i]);
        }
    }

    public void DisplayCards()
    {
        if (currentSelectedCard != null)
            if (currentSelectedCard.GetComponent<InvCard>().isMoving) return;

        if (cardCanvas.gameObject.activeSelf)
        {
            cardCanvas.gameObject.SetActive(false);
            selectPanel.SetActive(false);
            PC.invOpen = false;

            if (currentSelectedCard != null)
            {
                DeactivateSelectPanel(); 
                currentSelectedCard.GetComponent<InvCard>().SetInventoryOff();
            }

        }
        else
        {
            cardCanvas.gameObject.SetActive(true);
            PC.invOpen = true;
        }
    }

    public void ToggleCurrentCardSelect()
    {
        if (currentSelectedCard != null) currentSelectedCard.GetComponent<InvCard>().ToggleSelect();
    }

    public void ToggleButtonActive()
    {
        if (invBtn.IsInteractable()) invBtn.interactable = false;
        else invBtn.interactable = true;
    }

    public void SetSelectedCard(GameObject selectedCard)
    {
        currentSelectedCard = selectedCard;
    }

    public void ActivateSelectPanel()
    {
        selectPanel.SetActive(true);
        selectPanel.transform.SetAsLastSibling();
    }

    public void DeactivateSelectPanel()
    {
        selectPanel.transform.SetAsFirstSibling();
        selectPanel.SetActive(false);
    }

}

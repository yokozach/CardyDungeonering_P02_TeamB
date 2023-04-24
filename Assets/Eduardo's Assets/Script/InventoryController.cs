using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Canvas cardCanvas;
    [SerializeField] private List<GameObject> inventoryCards = new List<GameObject>();
    [SerializeField] private List<Vector2> cardPositions = new List<Vector2>();
    [SerializeField] private int maxCapacity = 10;

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
            inventoryCards[i].transform.position = cardCanvas.transform.TransformPoint(cardPositions[i]);
        }
    }

    public void DisplayCards(bool state)
    {
        for (int i = 0; i < inventoryCards.Count; i++)
        {
            inventoryCards[i].SetActive(state);
        }
    }

}

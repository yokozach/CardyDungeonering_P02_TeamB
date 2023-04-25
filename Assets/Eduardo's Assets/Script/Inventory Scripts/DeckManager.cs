using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> deck = new List<GameObject>();
    [SerializeField] private InventoryController inventoryController;

    void Start()
    {
        InitializeDeck();
        AddRandomCard();
        AddRandomCard();
        AddRandomCard();
        AddRandomCard();
        AddRandomCard();
    }

    // Get the InvCard script and set its deckNumber to the corresponding index in the list
    void InitializeDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            InvCard invCard = deck[i].GetComponent<InvCard>();
            if (invCard != null)
            {
                invCard.deckNumber = i;
            }
        }
    }

    // Instantiate a random card from the list of card prefabs and add it to the inventory
    void AddRandomCard()
    {
        if (inventoryController.inventoryCards.Count >= 10)
            return;

        int randomIndex = Random.Range(0, deck.Count);
        GameObject newCard = Instantiate(deck[randomIndex], Vector3.zero, Quaternion.identity);
        newCard.transform.parent = inventoryController.inventoryHolder.transform;
        inventoryController.AddCard(newCard);
        inventoryController.SortCardsByPosition();
        newCard.transform.localScale = Vector3.one;
        newCard.GetComponent<InvCard>().SetCenterPosition(new Vector2(0, 300));
    }

    // Instantiate a specific card from the list of card prefabs and add it to the inventory
    void AddSpecificCard(int cardIndex)
    {
        if (inventoryController.inventoryCards.Count >= 10)
            return;

        if (cardIndex >= 0 && cardIndex < deck.Count)
        {
            GameObject newCard = Instantiate(deck[cardIndex], Vector3.zero, Quaternion.identity);
            newCard.transform.parent = inventoryController.inventoryHolder.transform;
            inventoryController.AddCard(newCard);
            inventoryController.SortCardsByPosition();
            newCard.transform.localScale = Vector3.one;
            newCard.GetComponent<InvCard>().SetCenterPosition(new Vector2(0, 300));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private CentralManager centralManager;
    [SerializeField] private int addCardsAtStart;
    [SerializeField] private List<GameObject> deck = new List<GameObject>();

    private List<GameObject> availableDeck = new List<GameObject>();
    private bool _sfxAtStart;

    void Start()
    {
        // Add cards to availableDeck until their count reaches zero
        foreach (GameObject cardPrefab in deck)
        {
            InvCard invCard = cardPrefab.GetComponent<InvCard>();
            if (invCard != null && invCard.countInDeck > 0)
            {
                for (int i = 0; i < invCard.countInDeck; i++)
                {
                    availableDeck.Add(cardPrefab);
                }
            }
        }

        for (int i=0; i < addCardsAtStart; i++)
        {
            AddRandomCard();

        }
        _sfxAtStart = true;
    }

    // Instantiate a random card from the list of card prefabs and add it to the inventory (Remove from availableDeck)
    void AddRandomCard()
    {
        if (centralManager._inventoryController.inventoryCards.Count >= 10)
            return;

        // If deck is empty, return without adding a card
        if (availableDeck.Count == 0)
        {
            return;
        }

        // Randomly choose card from availableDeck, instantiate, then remove it from the availableDeck
        int randomIndex = Random.Range(0, availableDeck.Count);
        GameObject newCard = Instantiate(availableDeck[randomIndex], Vector3.zero, Quaternion.identity);
        availableDeck.RemoveAt(randomIndex);

        // Move card into inventory position as well as inventoryScript
        newCard.transform.parent = centralManager._inventoryController.inventoryHolder.transform;
        centralManager._inventoryController.AddCard(newCard);
        centralManager._inventoryController.SortCardsByPosition();
        newCard.transform.localScale = Vector3.one;
        newCard.GetComponent<InvCard>().SetCenterPosition(new Vector2(0, 300));
        if (_sfxAtStart) centralManager._sfxPlayer.Audio_CardCollected();
    }

    // Instantiate a specific card from the list of card prefabs and add it to the inventory (Chooses a specific card in the overall deck)
    void AddSpecificCard(int cardIndex)
    {
        if (centralManager._inventoryController.inventoryCards.Count >= 10)
            return;

        if (cardIndex >= 0 && cardIndex < deck.Count)
        {
            GameObject newCard = Instantiate(deck[cardIndex], Vector3.zero, Quaternion.identity);
            newCard.transform.parent = centralManager._inventoryController.inventoryHolder.transform;
            centralManager._inventoryController.AddCard(newCard);
            centralManager._inventoryController.SortCardsByPosition();
            newCard.transform.localScale = Vector3.one;
            newCard.GetComponent<InvCard>().SetCenterPosition(new Vector2(0, 300));
            if (_sfxAtStart) centralManager._sfxPlayer.Audio_CardCollected();
        }
    }

}

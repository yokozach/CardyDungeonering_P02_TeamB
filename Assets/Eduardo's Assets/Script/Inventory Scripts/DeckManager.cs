using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private CentralManager centralManager;
    [SerializeField] private int addCardsAtStart;
    [SerializeField] private List<GameObject> deck = new List<GameObject>();
    
    private bool _sfxAtStart;
    
    void Start()
    {
        for (int i=0; i < addCardsAtStart; i++)
        {
            AddRandomCard();

        }
        _sfxAtStart = true;
    }

    // Instantiate a random card from the list of card prefabs and add it to the inventory
    void AddRandomCard()
    {
        if (centralManager._inventoryController.inventoryCards.Count >= 10)
            return;

        int randomIndex = Random.Range(0, deck.Count);
        GameObject newCard = Instantiate(deck[randomIndex], Vector3.zero, Quaternion.identity);
        newCard.transform.parent = centralManager._inventoryController.inventoryHolder.transform;
        centralManager._inventoryController.AddCard(newCard);
        centralManager._inventoryController.SortCardsByPosition();
        newCard.transform.localScale = Vector3.one;
        newCard.GetComponent<InvCard>().SetCenterPosition(new Vector2(0, 300));
        if (_sfxAtStart) centralManager._sfxPlayer.Audio_CardCollected();
    }

    // Instantiate a specific card from the list of card prefabs and add it to the inventory
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

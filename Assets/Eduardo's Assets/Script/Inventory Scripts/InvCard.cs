using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}

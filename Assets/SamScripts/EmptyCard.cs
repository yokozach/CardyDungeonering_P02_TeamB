using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCard : MonoBehaviour
{
    [SerializeField] GameObject _card;
    public void DestroyCard()
    {
        Destroy(_card);
    }
}

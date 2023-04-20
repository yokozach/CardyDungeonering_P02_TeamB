using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTile : MonoBehaviour
{
    
    [SerializeField] public EventTile eventTile;
    public void Open()
    {
        eventTile.gameObject.SetActive(true);
        eventTile.PlayEvent();
        gameObject.SetActive(false);

    }


}

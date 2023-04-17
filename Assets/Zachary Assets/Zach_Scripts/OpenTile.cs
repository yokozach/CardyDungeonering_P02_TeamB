using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTile : MonoBehaviour
{


    [SerializeField] public EventTile eventTile;
    private void OnMouseDown()
    {
        eventTile.PlayEvent();
        gameObject.SetActive(false);


    }


}

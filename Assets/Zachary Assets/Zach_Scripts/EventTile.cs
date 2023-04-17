using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour
{

    [SerializeField] public GameObject EventObject;
   public void PlayEvent()
    {
        EventObject.SetActive(true);
        
    }

    public void EndEvent()
    {
        gameObject.SetActive(false);
    }

}

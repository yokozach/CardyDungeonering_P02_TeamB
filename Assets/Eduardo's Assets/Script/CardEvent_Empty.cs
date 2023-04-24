using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent_Empty : IEvent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Tile is empty; end encounter & move on
    public override void PlayEvent()
    {
        EndEvent(this.gameObject);
    }

}

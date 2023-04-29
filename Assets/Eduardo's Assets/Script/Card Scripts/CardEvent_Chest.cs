using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent_Chest : IEvent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Chest revealed; Draw a random card from card pool
    public override void PlayEvent()
    {
        StartCoroutine(GrantCard());
    }

    private IEnumerator GrantCard()
    {
        yield return new WaitForSeconds(1f);
        centralManager._deckManager.AddRandomCard();
        centralManager._sfxPlayer.Audio_CardCollected();
        EndEvent(this.gameObject);
    }

}

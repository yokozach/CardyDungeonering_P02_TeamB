using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent_Chest : IEvent
{

    [SerializeField] ChestAnimator chestAnim;

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
        centralManager._playerController.playerActive = false;

        centralManager._camController.SetTarget1(centralManager._playerController.gameObject);
        centralManager._camController.SetTarget2(gameObject);
        centralManager._camController.ToggleFocus();

        chestAnim.OpenChest();
        yield return new WaitForSeconds(1.1f);

        centralManager._deckManager.AddRandomCard();
        centralManager._sfxPlayer.Audio_CardCollected();
        yield return new WaitForSeconds(1.6f);

        centralManager._camController.SetTarget2(null);
        centralManager._camController.ToggleFocus();

        centralManager._playerController.playerActive = true;
        EndEvent(this.gameObject);
    }

}

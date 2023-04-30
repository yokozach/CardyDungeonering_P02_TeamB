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
<<<<<<< Updated upstream
        yield return new WaitForSeconds(1f);
=======
        centralManager._playerController.playerActive = false;
        chestAnim.OpenChest();
        yield return new WaitForSeconds(1.1f);

>>>>>>> Stashed changes
        centralManager._deckManager.AddRandomCard();
        centralManager._sfxPlayer.Audio_CardCollected();
        yield return new WaitForSeconds(1.6f);

        centralManager._playerController.playerActive = true;
        EndEvent(this.gameObject);
    }

}

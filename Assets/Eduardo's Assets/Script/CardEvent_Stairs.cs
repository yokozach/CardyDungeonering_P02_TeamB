using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEvent_Stairs : IEvent
{
    public bool _revealed;
    public bool _active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Stairs Encountered
    public override void PlayEvent()
    {
        _revealed = true;
    }

    // Check if mission is fulfilled; Next floor if done
    public void CheckMissionStatus()
    {
        if (_active == true)
        {
            Debug.Log("Floor Cleared!");
            // Add code to change state machine to next floor transition!
        }
        else
        {
            Debug.Log("Mission must be completed first!");
            // Update misison UI to flash & indicate mission must be completed!
        }
    }


}

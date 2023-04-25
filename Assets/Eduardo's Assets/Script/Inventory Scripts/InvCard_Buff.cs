using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCard_Buff : InvCard
{
    [Header("Buff Data")]
    [SerializeField] PlayerStats PlayerStats;


    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateCardEffects()
    {
        throw new System.NotImplementedException();
    }

}

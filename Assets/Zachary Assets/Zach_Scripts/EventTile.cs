using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour
{

    [SerializeField] GameObject _card;
    [SerializeField] bool _isEmpty = false;
    [SerializeField] bool _isEnemy = false;
    [SerializeField] bool _isStairs = false;
    public bool _battleStart = false;
    public bool _stairs = false;
    public int _enemiesDefeated = 0;
    private GameController _controller;
    public void PlayEvent()
    {
        if (_isEnemy)
        {
            _battleStart = true;
        }
        else if (_isEmpty)
        {
            _stairs = true;
            EndEvent();
        }
        else if (_isStairs && _enemiesDefeated ==3)
        {
            _stairs = true;
            //EndEvent();
        }
        //return this.gameObject;
        
    }

    public void EndEvent()
    {
        Debug.Log("End Event Called");
        Destroy(_card);
    }

}

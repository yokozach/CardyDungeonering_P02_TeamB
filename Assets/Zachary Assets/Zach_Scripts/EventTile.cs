using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour
{

    [SerializeField] GameObject _card;
    [SerializeField] bool _isEnemy = false;
    [SerializeField] bool _isEmpty = false;
    [SerializeField] bool _isStairs = false;
    [SerializeField] public int _goblinNum;
    public bool _battleStart = false;
    public bool _stairs = false;
    public int _enemiesDefeated = 0;
    private GameController _controller;
    public void PlayEvent()
    {
        Debug.Log("play Event");
        if (_isEnemy)
        {
            Debug.Log("Battle");
            _battleStart = true;
        }
        else if (_isEmpty)
        {
            Debug.Log("Empty");
            _stairs = true;
            EndEvent();
        }
        else if (_isStairs && _enemiesDefeated ==3)
        {
            Debug.Log("Stairs");
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

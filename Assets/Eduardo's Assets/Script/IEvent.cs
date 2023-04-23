using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEvent : MonoBehaviour
{

    public enum TileType
    {
        Enemy,
        Chest,
        Event,
        Empty,
        Stairs
    }

    public TileType tileType;

    public virtual void PrepEvent(GameObject _face, GameObject _cover)
    {
        _face.SetActive(true);
        _cover.SetActive(false);
        PlayEvent();
    }

    public abstract void PlayEvent();

    public virtual void EndEvent(GameObject mainCard)
    {
        Destroy(mainCard);
    }

    public virtual TileType ReturnType()
    {
        return tileType;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer_Floors : MonoBehaviour
{
    [SerializeField] AudioClip _step;
    [SerializeField] AudioClip _dmgHealth;
    [SerializeField] AudioClip _dmgShield;
    [SerializeField] AudioClip _death;
    [SerializeField] AudioClip _enemyEncounter;
    [SerializeField] AudioClip _itemCollected;
    [SerializeField] AudioClip _event;

    public void Audio_Step()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_step, 1);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_DmgHealth()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_dmgHealth, 1);
        audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_DmgShield()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_dmgShield, 1);
        audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_Death()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_death, 1);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_EnemyEncounter()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_enemyEncounter, 1);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_ItemCollected()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_itemCollected, 1);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_Event()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_event, 1);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }
}

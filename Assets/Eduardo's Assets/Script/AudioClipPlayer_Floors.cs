using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer_Floors : MonoBehaviour
{
    [SerializeField] AudioClip _step;
    [SerializeField] AudioClip _heal;
    [SerializeField] AudioClip _shieldUp;
    [SerializeField] AudioClip _buff;
    [SerializeField] AudioClip _dmgHealth;
    [SerializeField] AudioClip _dmgShield;
    [SerializeField] AudioClip _dmgCrit;
    [SerializeField] AudioClip _death;
    [SerializeField] AudioClip _enemyEncounter;
    [SerializeField] AudioClip _equipWeapon;
    [SerializeField] AudioClip _cardCollected;
    [SerializeField] AudioClip _event;

    public void Audio_Step()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_step, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1);
    }
    public void Audio_Heal()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_heal, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1);
    }

    public void Audio_ShieldUp()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_shieldUp, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1);
    }

    public void Audio_Buff()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_buff, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1);
    }

    public void Audio_DmgHealth()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_dmgHealth, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.75f, 1);
    }

    public void Audio_DmgShield()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_dmgShield, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.75f, 1);
    }

    public void Audio_DmgCrit()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_dmgCrit, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.75f, 1);
    }

    public void Audio_Death()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_death, 0.75f);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_EnemyEncounter()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_enemyEncounter, 0.75f);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_EquipWeapon()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_equipWeapon, 0.75f);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1);
    }

    public void Audio_CardCollected()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_cardCollected, 0.5f);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_Event()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_event, 0.75f);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_CustomClip(AudioClip clip, float volume, float minPitch)
    {
        AudioSource audioSource = AudioHelper._playClip2D(clip, volume);
        audioSource.pitch = UnityEngine.Random.Range(minPitch, 1);
    }

}

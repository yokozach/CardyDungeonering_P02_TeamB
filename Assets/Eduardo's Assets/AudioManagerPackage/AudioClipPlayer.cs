using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    [SerializeField] AudioClip _Ex1;
    [SerializeField] AudioClip _Ex2;

    public void Audio_Ex1()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_Ex1, 1);
        //audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

    public void Audio_Ex2()
    {
        AudioSource audioSource = AudioHelper._playClip2D(_Ex2, 1);
        audioSource.pitch = UnityEngine.Random.Range(0.5f, 1);
    }

}

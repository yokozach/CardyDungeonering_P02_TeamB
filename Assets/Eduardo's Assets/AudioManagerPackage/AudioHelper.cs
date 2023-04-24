using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public static AudioSource _playClip2D(AudioClip clip, float volume)
    {
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();

        Object.Destroy(audioObject, clip.length);
        return audioSource;
    }
}

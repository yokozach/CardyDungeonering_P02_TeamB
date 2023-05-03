using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Audio Lists")]
    [SerializeField] private AudioClip[] loopTracks;
    [SerializeField] private AudioClip[] nonLoopTracks;

    [Header("Track Settings")]
    [SerializeField] private bool randomize;
    [SerializeField] private int loopHistorySize = 2;

    [Header("Audio Effects")]
    [SerializeField] bool muffled = false;
    [SerializeField] int muffleFrequency = 500;

    private AudioSource audioSource;
    private AudioClip lastClip;
    private List<AudioClip> loopHistory = new List<AudioClip>();
    private AudioLowPassFilter lowPassFilter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();

        // Chooses to loop a single random track or randomize between nonLoop tracks
        if (audioSource.clip == null)
        {
            if (!randomize && loopTracks.Length > 0)
            {
                audioSource.loop = true;
                int randomClip = Random.Range(0, loopTracks.Length);
                audioSource.clip = loopTracks[randomClip];
            }
            else if (nonLoopTracks.Length > 0)
            {
                if (!randomize) randomize = true;
                audioSource.loop = false;
                int randomClip = Random.Range(0, nonLoopTracks.Length);
                audioSource.clip = nonLoopTracks[randomClip];
            }
            audioSource.Play();
            lastClip = audioSource.clip;
            loopHistory.Insert(0, lastClip);
            // Debug.Log(string.Join(", ", loopHistory));
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (muffled)
        {
            lowPassFilter.cutoffFrequency = muffleFrequency;
        }
        else
        {
            lowPassFilter.cutoffFrequency = 22000;
        }

        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            if (randomize) 
            {
                ChooseNewClip();
            }
        }

    }

    // Method to choose a new clip to play
    private void ChooseNewClip()
    {
        // Keeps track of previous clips play to prevent clips from playing back to back frequently
        int maxLoopHistorySize = Mathf.Min(loopHistorySize, nonLoopTracks.Length - 1);
        if (loopHistory.Count > maxLoopHistorySize)
        {
            loopHistory.RemoveAt(maxLoopHistorySize);
        }

        // Debug.Log(string.Join(", ", loopHistory));

        // Chooses random clip from nonLoopTracks; if in loopHistory, picks another song
        int randomClip;
        do
        {
            randomClip = Random.Range(0, nonLoopTracks.Length);
        } while (loopHistory.Contains(nonLoopTracks[randomClip]));

        // Inserts chosesn song into loopHistory & then plays the track
        lastClip = nonLoopTracks[randomClip];
        loopHistory.Insert(0, lastClip);
        audioSource.clip = lastClip;
        audioSource.Play();
    }

    public List<AudioClip> ReturnLoopHistory()
    {
        return loopHistory;
    }

    public void SetLoopHistory(List<AudioClip> prevLoopHistory)
    {
        loopHistory = prevLoopHistory;
    }

    public void SetMuffled(bool state)
    {
        muffled = state;
    }

}

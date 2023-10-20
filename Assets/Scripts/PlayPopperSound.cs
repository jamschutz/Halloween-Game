using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPopperSound : MonoBehaviour
{
    public AudioClip[] clips;
    public int numAudioSources;

    AudioSource[] audioSources;
    int currentAudioIndex;

    void Awake()
    {
        audioSources = new AudioSource[numAudioSources];

        for (int i = 0; i < audioSources.Length; i++) 
        {
            audioSources [i] = gameObject.AddComponent<AudioSource> ();
            audioSources[i].playOnAwake = false;
        }

        currentAudioIndex = 0;

        // show error if no clips assigned
        if(clips.Length == 0)
            Debug.LogError($"There are no clips assigned to the DialogAudio component on game object {gameObject.name}. Did you forget to add audio clips?");
    }

    public void Play()
    {
        if (audioSources [currentAudioIndex].isPlaying) 
        {
            audioSources [currentAudioIndex].Stop ();
        }

        audioSources [currentAudioIndex].clip = GetRandomClip();
        audioSources [currentAudioIndex].Play ();

        SetNextAudioIndex ();
    }

    AudioClip GetRandomClip()
    {
        int clipID = Random.Range (0, clips.Length);
        return clips [clipID];
    }

    void SetNextAudioIndex ()
    {
        currentAudioIndex++;
        if (currentAudioIndex >= audioSources.Length)
            currentAudioIndex = 0;
    }
}

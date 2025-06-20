using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "MusicPlaylist", menuName = "Scriptable Objects/MusicPlaylist")]
public class MusicPlaylist : ScriptableObject
{
    [System.Serializable]
    public class Track
    {
        public AudioClip clip;
        public float sixteenth; 
    }

    [Header("Fade")]
    public float fadingTime;
    public float fadingValue;
    public float delayBeforeFade;

    [Header("Track options")]
    public List<Track> tracks;

    public AudioSource currentTrack;

    public int currentTrackIndex = 0;

    [Header("Skip beginning")]
    public bool skipBeginning = true;
    public int cooldown = 0; 

    public void SwitchToStartTrack()
    {
        if (0 < tracks.Count)
        {
            currentTrackIndex = 0;
        }
    }

    public void SwitchToNextTrack()
    {
        currentTrackIndex++;
        currentTrackIndex = currentTrackIndex % tracks.Count;

        //Play();
    }

    public void Play(AudioSource audioSource)
    {
        if (currentTrackIndex < tracks.Count)
        {
            audioSource.clip = tracks[currentTrackIndex].clip;
            audioSource.Play();
        }
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public float GetSixteenth()
    {
        if (currentTrackIndex < tracks.Count)
        {
            return tracks[currentTrackIndex].sixteenth;
        }
        else
        {
            return 0f;
        }
    }

    public void SkipBeginning()
    {
        if (skipBeginning == true)
        {
            currentTrackIndex++;
            skipBeginning = false;
        }
    }
}

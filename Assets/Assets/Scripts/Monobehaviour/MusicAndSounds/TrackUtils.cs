using UnityEngine;

public static class TrackUtils
{
    public static float TrackLength(in AudioSource audioSource)
    {
        if (audioSource && audioSource.clip)
        {
            return audioSource.clip.length;
        }

        return 0f;
    }

    public static float CurrentTrackTime(in AudioSource audioSource)
    {
        if (audioSource && audioSource.clip)
        {
            return audioSource.time;
        }

        return 0f;
    }
}

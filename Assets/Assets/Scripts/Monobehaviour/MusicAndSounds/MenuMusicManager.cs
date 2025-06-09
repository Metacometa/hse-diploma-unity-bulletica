using UnityEngine;

class MenuMusicManager : MonoBehaviour
{
    private MusicManager musicManager;

    void Awake()
    {
        musicManager = GetComponent<MusicManager>();

        if (musicManager)
        {
            musicManager.StartToPlayMenuMusic();
        }

        musicManager.soundParameters.musicVolume = 0.15f;
        musicManager.soundParameters.volume = 0.15f;
    }
}
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

        musicManager.soundParameters.musicVolume = 0.05f;
        musicManager.soundParameters.volume = 0.05f;
    }
}
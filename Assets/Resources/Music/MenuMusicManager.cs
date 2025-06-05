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
    }
}
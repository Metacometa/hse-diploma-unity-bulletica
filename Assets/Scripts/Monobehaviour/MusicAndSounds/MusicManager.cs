using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private MusicPlaylist fightingPlaylist;
    [SerializeField] private MusicPlaylist ambientPlaylist;
    [SerializeField] private MusicPlaylist bossPlaylist;
    [SerializeField] private MusicPlaylist menuPlaylist;

    [Space]
    [SerializeField] public SoundParameters soundParameters;
    [Space]

    [SerializeField] private float tolerance;

    public MusicPlaylist playlist;
    private AudioSource audioSource;

    private bool playing;

    private Coroutine timingCoroutine;
    private Coroutine fadingCoroutine;
    private Coroutine bossCoroutine;

    private float startVolume;

    //public int fightingPlaylistTimeToSwitch = 0;
    public int trackSwitcherCooldown = 5;
    public int trackSwitcherCounter = 0;

    private UISettingsPanelScreenManager uiSettings;

    //
    bool volumeOnChanging = false;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playlist = ambientPlaylist;

        startVolume = audioSource.volume;

        //fightingPlaylistTimeToSwitch = fightingPlaylist.cooldown;

        trackSwitcherCounter = trackSwitcherCooldown;

        uiSettings = FindAnyObjectByType<UISettingsPanelScreenManager>();
    }

    private void Start()
    {
        fightingPlaylist.SwitchToStartTrack();
        ambientPlaylist.SwitchToStartTrack();
        bossPlaylist.SwitchToStartTrack();
        menuPlaylist.SwitchToStartTrack();

        ambientPlaylist.skipBeginning = true;
    }

    public void StartToPlayMusic()
    {
        playlist = ambientPlaylist;
        playlist.SwitchToStartTrack();
        playlist.Play(audioSource);

        playing = true;
    }

    public void StartToPlayMenuMusic()
    {
        playlist = menuPlaylist;

        StopLocalCoroutines();

        playlist.SwitchToStartTrack();
        playlist.Play(audioSource);

        playing = true;
    }

    void Update()
    {
        if (!Application.isFocused || !playing) { return; }

        //Debug.Log("Kek: " + audioSource.en)
        //Debug.Log($"Time: { TrackUtils.CurrentTrackTime(audioSource) }");
        if (!audioSource.isPlaying && playlist)
        {
            playlist.SwitchToNextTrack();
            playlist.Play(audioSource);
        }

        if (audioSource && !volumeOnChanging)
        {
            if (uiSettings)
            {
                soundParameters.volume = uiSettings.volumeEffectsSlider.value;
                soundParameters.musicVolume = uiSettings.volumeMusicSlider.value;
            }


            audioSource.volume = soundParameters.musicVolume;
        }
    }

    public void PlayFightingPlaylist()
    {
        trackSwitcherCounter++;

        if (playlist == fightingPlaylist) { return; }

        if (trackSwitcherCounter < trackSwitcherCooldown) { return; }

        StopLocalCoroutines();

        timingCoroutine = StartCoroutine(TimingCoroutine(fightingPlaylist));

        trackSwitcherCounter = 0;
    }

    public void PlayAmbientPlaylist()
    {
        if (playlist == ambientPlaylist) { return; }
        //trackSwitcherCounter++;
        if (trackSwitcherCounter < trackSwitcherCooldown) { return; }


        if (ambientPlaylist.skipBeginning)
        {
            ambientPlaylist.SkipBeginning();
        }

        StopLocalCoroutines();
        fadingCoroutine = StartCoroutine(FadingCoroutine(ambientPlaylist));

        trackSwitcherCounter = 0;
    }

    public void PlayWinPlaylist()
    {
        if (!playlist) { return; }
        //trackSwitcherCounter++;

        StopLocalCoroutines();
        playlist.Stop(audioSource);
        playlist = null;
        audioSource.clip = null;
        //fadingCoroutine = StartCoroutine(FadingCoroutine(ambientPlaylist));

        trackSwitcherCounter = 0;
    }

    public void PlayLosePlaylist()
    {
        if (!playlist) { return; }
        //trackSwitcherCounter++;

        StopLocalCoroutines();
        playlist.Stop(audioSource);
        playlist = null;
        audioSource.clip = null;
        //fadingCoroutine = StartCoroutine(FadingCoroutine(ambientPlaylist));

        trackSwitcherCounter = 0;
    }

    public void PlayBossPlaylist()
    {
        if (playlist == bossPlaylist) { return; }

        StopLocalCoroutines();

        bossCoroutine = StartCoroutine(BossCoroutine(bossPlaylist));

        trackSwitcherCounter = 0;
    }

    IEnumerator BossCoroutine(MusicPlaylist newPlaylist)
    {
        Debug.Log("BossCoroutine");
        volumeOnChanging = true;

        float tempVolume = audioSource.volume;
        float time = 0;

        while (time < newPlaylist.fadingTime)
        {
            time += Time.deltaTime;

            float a = tempVolume;
            float b = newPlaylist.fadingValue;
            float t = time / newPlaylist.fadingTime;
            audioSource.volume = Mathf.Lerp(a, b, t);

            yield return null;
        }

        Debug.Log("BossCoroutine.Stop()");
        audioSource.Stop();
        audioSource.volume = 0f;
        yield return new WaitForSeconds(2f);

        Debug.Log("BossCoroutine.Next()");

        playlist = newPlaylist;
        playlist.Play(audioSource);
        audioSource.volume = tempVolume;

        volumeOnChanging = false;
    }

    IEnumerator TimingCoroutine(MusicPlaylist newPlaylist)
    {
        int times = 0;
        while (times++ < 1000)
        {
            float sixteenth = playlist.GetSixteenth();
            float time = TrackUtils.CurrentTrackTime(audioSource);

            float currTact = time / sixteenth;
            float desiredTact = Mathf.Round(time / sixteenth);

            float diff = Mathf.Abs(currTact - desiredTact);

            if (diff <= tolerance)
            {
                break;
            }

            yield return null;
        }

        playlist = newPlaylist;
        playlist.Play(audioSource);
        //audioSource.volume = startVolume;
    }

    IEnumerator FadingCoroutine(MusicPlaylist newPlaylist)
    {
        volumeOnChanging = true;

        yield return new WaitForSeconds(playlist.delayBeforeFade);

        float tempVolume = audioSource.volume;
        float time = 0;

        while (time < newPlaylist.fadingTime)
        {
            time += Time.deltaTime;

            float a = tempVolume;
            float b = newPlaylist.fadingValue;
            float t = time / newPlaylist.fadingTime;
            audioSource.volume = Mathf.Lerp(a, b, t);

            yield return null;
        }

        playlist = newPlaylist;
        playlist.Play(audioSource);
        audioSource.volume = tempVolume;

        volumeOnChanging = false;
    }

    private void StopLocalCoroutines()
    {
        if (timingCoroutine != null)
        {
            StopCoroutine(timingCoroutine);
            timingCoroutine = null;
        }

        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
            fadingCoroutine = null;
        }

        if (bossCoroutine != null)
        {
            StopCoroutine(bossCoroutine);
            bossCoroutine = null;
        }
    }
}

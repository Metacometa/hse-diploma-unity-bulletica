using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private MusicPlaylist fightingPlaylist;
    [SerializeField] private MusicPlaylist ambientPlaylist;

    [SerializeField] private float tolerance;

    public MusicPlaylist playlist;
    private AudioSource audioSource;

    private bool playing;

    private Coroutine timingCoroutine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playlist = ambientPlaylist;
    }

    public void StartToPlayMusic()
    {
        playlist = ambientPlaylist;
        playlist.SwitchToStartTrack();
        playlist.Play(audioSource);

        playing = true;
    }

    void Update()
    {
        if (!Application.isFocused || !playing) { return; }

        //Debug.Log($"Time: { TrackUtils.CurrentTrackTime(audioSource) }");
        if (!audioSource.isPlaying)
        {
            playlist.SwitchToNextTrack();
            playlist.Play(audioSource);
        }
    }

    public void PlayFightingPlaylist()
    {
        StopTimingCoroutine();

        timingCoroutine = StartCoroutine(TimingCoroutine(fightingPlaylist));
    }

    public void PlayAmbientPlaylist()
    {
        StopTimingCoroutine();
        timingCoroutine = StartCoroutine(TimingCoroutine(ambientPlaylist));
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
                Debug.Log("Times=1000");
                break;
            }

            yield return null;
        }

        float sixteenth_ = playlist.GetSixteenth();
        float time_ = TrackUtils.CurrentTrackTime(audioSource);

        float currTact_ = time_ / sixteenth_;
        float desiredTact_ = Mathf.Round(time_ / sixteenth_);

        float diff_ = Mathf.Abs(currTact_ - desiredTact_);

        Debug.Log($"Sx: {sixteenth_}, t: {time_}, currT: {currTact_}, desT: {desiredTact_}");

        audioSource.Stop();

        playlist = newPlaylist;
        playlist.Play(audioSource);
    }

    private void StopTimingCoroutine()
    {
        if (timingCoroutine != null)
        {
            StopCoroutine(timingCoroutine);
            timingCoroutine = null;
        }
    }

}

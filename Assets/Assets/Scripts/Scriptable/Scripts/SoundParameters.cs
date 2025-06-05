using UnityEngine;

[CreateAssetMenu(fileName = "SoundParameters", menuName = "Scriptable Objects/SoundParameters")]
public class SoundParameters : ScriptableObject
{
    [Range(0, 1f)]
    public float volume;

    [Range(0, 1f)]
    public float musicVolume;

    [Space]
    [Header("Pitch")]
    [Range(-3f, 3f)]
    public float onStartSoundStartPitch;
    public float onStartSoundPitch;
    public float onStartSoundPitchDiff;

    [Space]
    [SerializeField] public AudioClip chamberStartClip;
    [SerializeField] public AudioClip chamberEndClip;

    [Space]
    [SerializeField] public AudioClip enemyDeath;
    [SerializeField] public AudioClip playerDeath;

}

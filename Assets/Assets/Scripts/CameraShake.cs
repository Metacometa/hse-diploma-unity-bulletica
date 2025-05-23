using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour 
{
    public static CameraShake Instance { get; private set; }
    private CinemachineCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        shakeTimer = time;
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
                cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.AmplitudeGain = 0f;
            }
        }
    }
}

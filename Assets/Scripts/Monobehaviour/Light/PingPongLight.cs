using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PingPongLight : MonoBehaviour
{
    private Light2D lightComponent;

    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensinty;

    void Awake()
    {
        lightComponent = GetComponent<Light2D>();
    }

    void Update()
    {
        lightComponent.intensity = Mathf.PingPong(Time.deltaTime, maxIntensinty) + minIntensity;
    }
}

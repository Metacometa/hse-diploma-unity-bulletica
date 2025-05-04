using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AlarmLight : MonoBehaviour
{
    private IEnumerator alarm;
    private float latency = 1f;

    public Light2D[] lightComponents;
    private Level level;

    public List<Color> colorsA;
    [SerializeField] private Color colorB;

    [SerializeField] private float cycleDuration = 2f;

    private bool onAlarm;

    void Awake()
    {
        colorsA = new List<Color>();

        lightComponents = GetComponentsInChildren<Light2D>();

        alarm = Alarm();

        level = GetComponentInParent<Level>();
        if (level) 
        {
            latency = level.gameParameters.enablingEnemiesDelay;
        }

        foreach(Light2D light in lightComponents)
        {
            colorsA.Add(light.color);
        }

        onAlarm = false;
    }

    void Update()
    {
        if (onAlarm)
        {
            float t = Mathf.PingPong(Time.time / cycleDuration, 1.0f);

            for (int i = 0; i < lightComponents.Length; ++i)
            {
                lightComponents[i].color = Color.Lerp(colorsA[i], colorB, t);
                Color temp = lightComponents[i].color;
                lightComponents[i].color = new Color(temp.r, temp.g, temp.b, 1);
            }

        }
    }


    public void StartAlarm()
    {
        StartCoroutine(alarm);
    }

    IEnumerator Alarm()
    {
        yield return new WaitForSeconds(latency);

        onAlarm = true;
    }
}

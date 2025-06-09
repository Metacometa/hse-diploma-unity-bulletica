using UnityEngine;
using UnityEngine.Rendering.Universal;
public class BulletLight : MonoBehaviour
{
    public Light2D light_;
    public GameObject gameObjectLevel;
    private float tempIntensity;

    void Awake()
    {
        light_ = GetComponent<Light2D>();

        gameObjectLevel = GameObject.Find("Level");
        tempIntensity = light_.intensity;

        Level level = gameObjectLevel.GetComponent<Level>();

        if (gameObjectLevel)
        {

            if (level)
            {
                if (level.onAlarm)
                {
                    light_.intensity = 0f;
                    light_.overlapOperation = Light2D.OverlapOperation.Additive;
                }
                else
                {
                    light_.intensity = tempIntensity;
                    light_.overlapOperation = Light2D.OverlapOperation.AlphaBlend;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

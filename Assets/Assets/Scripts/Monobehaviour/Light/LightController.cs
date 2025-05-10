using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public List<Light2D> otherLights;
    public List<Light2D> characterLights;
    public List<Light2D> doorLights;
    public List<Light2D> globalLights;

    private Level level;
    private Dictionary<Light2D, float> intensities;

    void Awake()
    {
        level = GetComponentInParent<Level>();

        intensities = new Dictionary<Light2D, float>();
        //Debug.Log("Parent: " + transform.p);
        foreach (Light2D light in transform.parent.GetComponentsInChildren<Light2D>())
        {
            //Debug.Log("Tag: " + light.tag + $"Name: { light.name }");
            if (light.tag == "Global Light")
            {
                globalLights.Add(light);
            }
            else if (light.tag == "Character Light")
            {
                characterLights.Add(light);
            }
            else if (light.tag == "Door Light")
            {
                doorLights.Add(light);
            }
            else
            {
                otherLights.Add(light);
            }

            intensities[light] = light.intensity;
        }

        TurnOffLight();

    }

    public void TurnOffLight()
    {
        TurnOffLights(characterLights);
        TurnOffLights(otherLights);

        TurnOnDoorLight();

        TurnOffIntensities(globalLights);
    }

    public void TurnOnLight()
    {
        TurnOnLights(characterLights);
        TurnOnLights(otherLights);

        TurnOffDoorLight();

        TurnOnIntensities(globalLights);
    }

    public void TurnOnDoorLight()
    {
        TurnOnLights(doorLights);
    }

    public void TurnOffDoorLight()
    {
        TurnOffLights(doorLights);
    }

    private void TurnOnLights(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            light.enabled = true;
        }
    }

    private void TurnOffLights(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            light.enabled = false;
        }
    }

    private void TurnOffIntensities(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            light.intensity = level.gameParameters.turnedOffIntensity;
        }
    }

    private void TurnOnIntensities(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            light.intensity = intensities[light];
        }
    }
}

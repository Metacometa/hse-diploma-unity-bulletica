using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public List<Light2D> otherLights;
    public List<Light2D> characterLights;
    public List<Light2D> doorLights;
    public List<Light2D> globalLights;

    [SerializeField] private float turnedOffIntensity = 1;
    private Dictionary<Light2D, float> intensities;

    void Awake()
    {
        intensities = new Dictionary<Light2D, float>();
        //Debug.Log("Parent: " + transform.p);
        foreach (Light2D light in transform.parent.GetComponentsInChildren<Light2D>())
        {
            Debug.Log("Tag: " + light.tag + $"Name: { light.name }");
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

        //TurnOffLight();
    }

    public void TurnOffLight()
    {
        TurnOffLights(characterLights);
        TurnOffLights(otherLights);
        TurnOffLights(doorLights);
        TurnOffIntensities(globalLights);
    }

    public void TurnOnLight()
    {
        TurnOnLights(characterLights);
        TurnOnLights(otherLights);
        TurnOnIntensities(globalLights);
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
            light.intensity = turnedOffIntensity;
        }
    }

    private void TurnOnIntensities(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            light.intensity = intensities[light];
        }
    }

    void Update()
    {
        
    }
}

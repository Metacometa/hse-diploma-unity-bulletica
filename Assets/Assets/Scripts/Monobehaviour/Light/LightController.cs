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
        CopyBoxCollider2D();

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

    private void CopyBoxCollider2D()
    {
        BaseChamberStarter baseChamberStarter = transform.parent.GetComponentInChildren<BaseChamberStarter>();
        BoxCollider2D sourceCollider = baseChamberStarter.GetComponent<BoxCollider2D>();

        BoxCollider2D targetCollider = gameObject.AddComponent<BoxCollider2D>();
        
        if (sourceCollider && targetCollider)
        {
            targetCollider.size = sourceCollider.size + new Vector2(8,8);
            targetCollider.offset = sourceCollider.offset;
            targetCollider.isTrigger = sourceCollider.isTrigger;
            targetCollider.sharedMaterial = sourceCollider.sharedMaterial;
        }
    }

    private void TurnOffLight()
    {
        TurnOffLights(characterLights);
        TurnOffLights(otherLights);

        TurnOnDoorLight();

        TurnOffIntensities(globalLights);
    }

    private void TurnOnLight()
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
        foreach (Light2D light in doorLights)
        {
            light.color = level.gameParameters.clearedColor;
        }
        TurnOffLights(doorLights);

    }

    private void TurnOnLights(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            if (light)
            {
                light.enabled = true;
            }
        }
    }

    private void TurnOffLights(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            if (light)
            {
                light.enabled = false;
            }
        }
    }

    private void TurnOffIntensities(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            if (light)
            {
                light.intensity = level.gameParameters.turnedOffIntensity;
            }
        }
    }

    private void TurnOnIntensities(List<Light2D> lights)
    {
        foreach (Light2D light in lights)
        {
            if (light)
            {
                light.intensity = intensities[light];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("TurnOnLight");
            TurnOnLight();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("TurnOffLight");
            TurnOffLight();
        }
    }
}

using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Scriptable Objects/GameParameters")]
public class GameParameters : ScriptableObject
{
    [Tooltip("Time before the mob starts to attack after spawning.")]

    public float awakeningDelay;

    [Tooltip("Delay after which the mob appears.")]

    public float enablingEnemiesDelay;

    public float turnedOffIntensity;
}

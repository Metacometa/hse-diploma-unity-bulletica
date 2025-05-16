using UnityEngine;

[CreateAssetMenu(fileName = "SpawnChambersData", menuName = "Scriptable Objects/SpawnChambersData")]
public class SpawnChambersData : ScriptableObject
{
    [Header("Chamber Directories")]
    [SerializeField] public string startRoomsPath = "";
    [SerializeField] public string bossRoomsPath = "";
    [SerializeField] public string easyRoomsPath = "";
    [SerializeField] public string mediumRoomsPath = "";
    [SerializeField] public string hardRoomsPath = "";
    [SerializeField] public string hubRoomsPath = "";

    [Header("Generation Settings")]
    [SerializeField] public int minRooms = 10;
    [SerializeField] public int maxRooms = 15;
    [SerializeField][Range(0, 100)] public int easyRoomPercentage = 30;
    [SerializeField][Range(0, 100)] public int mediumRoomPercentage = 50;
    [SerializeField][Range(0, 100)] public int hardRoomPercentage = 20;
    [SerializeField] public int minHubs = 1;
    [SerializeField] public int maxHubs = 3;
    [SerializeField] public string seed = "";
    [SerializeField] public int maxGenerationAttempts = 1000;
}

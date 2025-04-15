using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

public class SpawnChambers : MonoBehaviour
{
    public SpawnChambersData data;

    private List<GameObject> startRoomPrefabs = new List<GameObject>();
    private List<GameObject> bossRoomPrefabs = new List<GameObject>();
    private List<GameObject> easyRoomPrefabs = new List<GameObject>();
    private List<GameObject> mediumRoomPrefabs = new List<GameObject>();
    private List<GameObject> hardRoomPrefabs = new List<GameObject>();
    private List<GameObject> hubRoomPrefabs = new List<GameObject>();

    private List<Chamber> spawnedChambers = new List<Chamber>();
    private Chamber startChamber;
    private Chamber bossChamber;
    private List<Chamber> hubChambers = new List<Chamber>();

    private int totalRooms;
    private int hubRoomsCount;
    private int easyRoomsCount;
    private int mediumRoomsCount;
    private int hardRoomsCount;
    private int currentSeed;

    void Awake()
    {
        LoadRoomPrefabs();
        InitializeSeed();
        CalculateGenerationParameters();
        GenerateLevel();
    }

    private void InitializeSeed()
    {
        if (string.IsNullOrWhiteSpace(data.seed))
        {
            currentSeed = System.DateTime.Now.GetHashCode();
        }
        else if (int.TryParse(data.seed, out int numericSeed))
        {
            currentSeed = numericSeed;
        }
        else
        {
            currentSeed = data.seed.GetHashCode();
        }
        Debug.Log($"Current seed: {currentSeed}");
        Random.InitState(currentSeed);
    }

    private void CalculateGenerationParameters()
    {
        ValidateRoomPercentages();

        totalRooms = Random.Range(data.minRooms, data.maxRooms + 1);

        hubRoomsCount = Random.Range(data.minHubs, data.maxHubs + 1);

        int normalRoomsCount = totalRooms - 2 - hubRoomsCount; 
        if (normalRoomsCount < 0) normalRoomsCount = 0;

        easyRoomsCount = Mathf.RoundToInt(normalRoomsCount * data.easyRoomPercentage / 100f);
        mediumRoomsCount = Mathf.RoundToInt(normalRoomsCount * data.mediumRoomPercentage / 100f);
        hardRoomsCount = normalRoomsCount - easyRoomsCount - mediumRoomsCount;
    }

    private void ValidateRoomPercentages()
    {
        int sum = data.easyRoomPercentage + data.mediumRoomPercentage + data.hardRoomPercentage;
        if (sum != 100)
        {
            float total = data.easyRoomPercentage + data.mediumRoomPercentage + data.hardRoomPercentage;
            data.easyRoomPercentage = Mathf.RoundToInt(data.easyRoomPercentage / total * 100);
            data.mediumRoomPercentage = Mathf.RoundToInt(data.mediumRoomPercentage / total * 100);
            data.hardRoomPercentage = 100 - data.easyRoomPercentage - data.mediumRoomPercentage;
            Debug.Log($"Normalized room percentages to: Easy {data.easyRoomPercentage}%, Medium {data.mediumRoomPercentage}%, Hard {data.hardRoomPercentage}%");
        }
    }

    private void LoadRoomPrefabs()
    {
        startRoomPrefabs = Resources.LoadAll<GameObject>(data.startRoomsPath).ToList();
        bossRoomPrefabs = Resources.LoadAll<GameObject>(data.bossRoomsPath).ToList();
        easyRoomPrefabs = Resources.LoadAll<GameObject>(data.easyRoomsPath).ToList();
        mediumRoomPrefabs = Resources.LoadAll<GameObject>(data.mediumRoomsPath).ToList();
        hardRoomPrefabs = Resources.LoadAll<GameObject>(data.hardRoomsPath).ToList();
        hubRoomPrefabs = Resources.LoadAll<GameObject>(data.hubRoomsPath).ToList();
    }

    private void GenerateLevel()
    {
        startChamber = SpawnStartChamber(GetRandomPrefab(startRoomPrefabs));
        spawnedChambers.Add(startChamber);

        GenerateAllRooms();

        GenerateBossRoom();

        ValidateHubRooms();
    }

    private void GenerateAllRooms()
    {
        int roomsToSpawn = easyRoomsCount + mediumRoomsCount + hardRoomsCount + hubRoomsCount;
        int attempts = 0;

        while ((spawnedChambers.Count < roomsToSpawn + 1) && (attempts < data.maxGenerationAttempts))
        {
            attempts++;

            Chamber baseChamber = GetRandomExistingChamber();

            Vector2Int direction = GetRandomDirection();

            if (TrySpawnRoomInDirection(baseChamber, direction, out Chamber newChamber))
            {
                if (newChamber.gameObject.CompareTag("HubChamber"))
                {
                    hubChambers.Add(newChamber);
                }
            }
        }
    }

    private bool TrySpawnRoomInDirection(Chamber baseChamber, Vector2Int direction, out Chamber newChamber)
    {
        RoomType roomType = GetNextRoomType();
        GameObject roomPrefab = GetRoomPrefabByType(roomType);

        newChamber = SpawnRoomInDirection(baseChamber, direction, roomPrefab);
        spawnedChambers.Add(newChamber);

        if (CheckRoomOverlap(newChamber))
        {
            spawnedChambers.RemoveAt(spawnedChambers.Count - 1);
            Destroy(newChamber.gameObject);
            return false;
        }

        return true;
    }

    private bool TrySpawnBossRoomInDirection(Chamber baseChamber, Vector2Int direction, out Chamber newChamber)
    {
        newChamber = SpawnRoomInDirection(baseChamber, direction, GetRandomPrefab(bossRoomPrefabs));
        spawnedChambers.Add(newChamber);

        if (CheckRoomOverlap(newChamber))
        {
            spawnedChambers.RemoveAt(spawnedChambers.Count - 1);
            Destroy(newChamber.gameObject);
            return false;
        }

        return true;
    }

    private RoomType GetNextRoomType()
    {
        if (hubChambers.Count < hubRoomsCount && spawnedChambers.Count > 1 && Random.value < Mathf.Min(0.3f + (0.05f * (spawnedChambers.Count - 1)), 1.0f))
        {
            return RoomType.Hub;
        }

        List<RoomType> availableTypes = new List<RoomType>();
        if (easyRoomsCount > 0) availableTypes.Add(RoomType.Easy);
        if (mediumRoomsCount > 0) availableTypes.Add(RoomType.Medium);
        if (hardRoomsCount > 0) availableTypes.Add(RoomType.Hard);

        if (availableTypes.Count == 0) return RoomType.Easy;

        RoomType selectedType = availableTypes[Random.Range(0, availableTypes.Count)];

        switch (selectedType)
        {
            case RoomType.Easy: easyRoomsCount--; break;
            case RoomType.Medium: mediumRoomsCount--; break;
            case RoomType.Hard: hardRoomsCount--; break;
        }

        return selectedType;
    }

    private void GenerateBossRoom()
    {
        List<Vector2Int> directions = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (var chamber in spawnedChambers.OrderByDescending(c => Vector3.Distance(startChamber.transform.position, c.transform.position)))
        {
            foreach (var dir in directions)
            {
                if (TrySpawnBossRoomInDirection(chamber, dir, out Chamber newChamber))
                {
                    bossChamber = newChamber;
                    return;
                }
            }
        }
    }

    private void ValidateHubRooms(int maxAttempts=10)
    {
        List<Vector2Int> directions = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (var hub in hubChambers.ToList())
        {
            int attempts = 0;
            int neighbors = CountNeighbors(hub);
            while (neighbors < 3 && attempts<maxAttempts)
            {
                attempts++;
                foreach (var dir in directions)
                {
                    if (TrySpawnRoomInDirection(hub, dir, out Chamber newChamber))
                    {
                        neighbors++;
                    }
                }
            }

            if (CountNeighbors(hub) < 3)
            {
                Debug.LogWarning($"Hub room at {hub.transform.position} has only {CountNeighbors(hub)} neighbors.");
            }
        }
    }

    private Chamber GetRandomExistingChamber()
    {
        return spawnedChambers[Random.Range(0, spawnedChambers.Count)];
    }

    private Vector2Int GetRandomDirection()
    {
        int dir = Random.Range(0, 4);
        return dir switch
        {
            0 => Vector2Int.up,
            1 => Vector2Int.down,
            2 => Vector2Int.left,
            _ => Vector2Int.right
        };
    }

    private int CountNeighbors(Chamber chamber)
    {
        int count = 0;
        if (HasNeighborInDirection(chamber, Vector2Int.up)) count++;
        if (HasNeighborInDirection(chamber, Vector2Int.down)) count++;
        if (HasNeighborInDirection(chamber, Vector2Int.left)) count++;
        if (HasNeighborInDirection(chamber, Vector2Int.right)) count++;
        return count;
    }
    private bool HasNeighborInDirection(Chamber chamber, Vector2Int direction)
    {
        Chamber tempRoom = SpawnRoomInDirection(chamber, direction, startRoomPrefabs[0]);
        bool hasCollision = CheckRoomOverlap(tempRoom);
        Destroy(tempRoom.gameObject);

        return hasCollision;
    }

    private bool CheckRoomOverlap(Chamber newChamber)
    {
        PolygonCollider2D newCollider = newChamber.GetComponentInChildren<Room>().GetComponent<PolygonCollider2D>();
        UpdateCollider(newCollider);
        if (newCollider == null) return false;

        foreach (var chamber in spawnedChambers)
        {
            if (chamber == newChamber) continue;

            PolygonCollider2D existingCollider = chamber.GetComponentInChildren<Room>().GetComponent<PolygonCollider2D>();
            UpdateCollider(existingCollider);
            if (existingCollider == null) continue;

            if (ColliderOverlap(newCollider, existingCollider))
            {
                return true;
            }
        }

        return false;
    }

    private bool ColliderOverlap(PolygonCollider2D colliderA, PolygonCollider2D colliderB)
    {
        return colliderA.bounds.Intersects(colliderB.bounds);
        //return Physics2D.Distance(colliderA, colliderB).distance <= 0;
    }

    private void UpdateCollider(PolygonCollider2D collider)
    {
        collider.enabled = false;
        collider.enabled = true;
    }

    private GameObject GetRoomPrefabByType(RoomType type)
    {
        return type switch
        {
            RoomType.Easy => GetRandomPrefab(easyRoomPrefabs),
            RoomType.Medium => GetRandomPrefab(mediumRoomPrefabs),
            RoomType.Hard => GetRandomPrefab(hardRoomPrefabs),
            RoomType.Hub => GetRandomPrefab(hubRoomPrefabs),
            _ => GetRandomPrefab(easyRoomPrefabs)
        };
    }

    private GameObject GetRandomPrefab(List<GameObject> prefabs)
    {
        if (prefabs == null || prefabs.Count == 0) return null;
        return prefabs[Random.Range(0, prefabs.Count)];
    }

    private enum RoomType
    {
        Easy,
        Medium,
        Hard,
        Hub
    }

    /// <summary>
    /// Спавнит prefab комнаты слева
    /// </summary>
    /// <param name="chamber"> Комната, левая сторона которой будет соприкасаться с новой комнатой </param>
    /// <param name="prefab_"> Префаб новой комнаты</param>
    /// <returns></returns>
    public Chamber SpawnLeftward(in Chamber chamber, in GameObject prefab_)
    {
        Chamber newRoom = Instantiate(prefab_, Vector3.zero, Quaternion.identity, transform).GetComponent<Chamber>();
        newRoom.SetLeftRotation();

        newRoom.transform.position = chamber.GetLeftContactPoint() - newRoom.GetRightContactPoint();

        return newRoom;
    }

    /// <summary>
    /// Спавнит prefab комнаты справа
    /// </summary>
    /// <param name="chamber"> Комната, правая сторона которой будет соприкасаться с новой комнатой </param>
    /// <param name="prefab_"> Префаб новой комнаты</param>
    /// <returns></returns>
    public Chamber SpawnRightward(in Chamber chamber, in GameObject prefab_)
    {
        Chamber newRoom = Instantiate(prefab_, Vector3.zero, Quaternion.identity, transform).GetComponent<Chamber>();
        newRoom.SetRightRotation();

        newRoom.transform.position = chamber.GetRightContactPoint() - newRoom.GetLeftContactPoint();

        return newRoom;
    }

    /// <summary>
    /// Спавнит prefab комнаты сверху
    /// </summary>
    /// <param name="chamber"> Комната, верхняя сторона которой будет соприкасаться с новой комнатой </param>
    /// <param name="prefab_"> Префаб новой комнаты</param>
    /// <returns></returns>
    public Chamber SpawnUpward(in Chamber chamber, in GameObject prefab_)
    {
        Chamber newRoom = Instantiate(prefab_, Vector3.zero, Quaternion.identity, transform).GetComponent<Chamber>();
        newRoom.SetTopRotation();

        newRoom.transform.position = chamber.GetTopContactPoint() - newRoom.GetBottomContactPoint();

        return newRoom;
    }

    /// <summary>
    /// Спавнит prefab комнаты снизу
    /// </summary>
    /// <param name="chamber"> Комната, нижняя сторона которой будет соприкасаться с новой комнатой </param>
    /// <param name="prefab_
    /// <returns></returns>
    public Chamber SpawnDownward(in Chamber chamber, in GameObject prefab_)
    {
        Chamber newRoom = Instantiate(prefab_, Vector3.zero, Quaternion.identity, transform).GetComponent<Chamber>();
        newRoom.SetBottomRotation();

        newRoom.transform.position = chamber.GetBottomContactPoint() - newRoom.GetTopContactPoint();

        return newRoom;
    }

    /// <summary>
    /// Спавнит стартовую комнату в центре уровня
    /// </summary>
    /// <param name="prefab_"> Префаб стартовой комнаты </param>
    /// <returns></returns>
    public Chamber SpawnStartChamber(in GameObject prefab_)
    {
        Chamber newRoom = Instantiate(prefab_, Vector3.zero, Quaternion.identity, transform).GetComponent<Chamber>();
        newRoom.SetLeftRotation();

        return newRoom;
    }

    private Chamber SpawnRoomInDirection(Chamber baseChamber, Vector2Int direction, GameObject prefab)
    {
        if (direction == Vector2Int.up)
            return SpawnUpward(baseChamber, prefab);
        else if (direction == Vector2Int.down)
            return SpawnDownward(baseChamber, prefab);
        else if (direction == Vector2Int.left)
            return SpawnLeftward(baseChamber, prefab);
        else
            return SpawnRightward(baseChamber, prefab);
    }
}

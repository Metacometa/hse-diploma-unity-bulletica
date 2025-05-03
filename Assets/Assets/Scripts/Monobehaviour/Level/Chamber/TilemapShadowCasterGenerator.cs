using UnityEngine;
using UnityEngine.Rendering.Universal; // Для ShadowCaster2D
using System.Collections.Generic;

[RequireComponent(typeof(CompositeCollider2D))]
public class TilemapShadowCasterGenerator : MonoBehaviour
{
    [SerializeField] // Используем SerializeField для доступа из редактора, если нужно
    private CompositeCollider2D tilemapCollider;
    private List<GameObject> shadowCasterObjects = new List<GameObject>(); // Список для хранения созданных объектов

    // Добавляем кнопку в контекстное меню компонента в Инспекторе
    [ContextMenu("Generate Shadow Casters")]
    public void GenerateShadowCasters()
    {
        // Находим коллайдер, если он не назначен вручную
        if (tilemapCollider == null)
        {
            tilemapCollider = GetComponent<CompositeCollider2D>();
            if (tilemapCollider == null)
            {
                Debug.LogError("CompositeCollider2D не найден на этом объекте!", this);
                return;
            }
        }

        // Сначала удаляем старые созданные объекты, если они есть
        ClearExistingShadowCasters();

        // Создаем новый пустой объект-контейнер для чистоты иерархии
        GameObject shadowCasterContainer = new GameObject("ShadowCasterContainer");
        shadowCasterContainer.transform.SetParent(transform); // Делаем дочерним к тайлмапу
        shadowCasterContainer.transform.localPosition = Vector3.zero;
        shadowCasterContainer.transform.localRotation = Quaternion.identity;
        shadowCasterContainer.transform.localScale = Vector3.one;
        shadowCasterObjects.Add(shadowCasterContainer); // Добавляем контейнер в список для удаления

        // Получаем геометрию из CompositeCollider2D
        int pathCount = tilemapCollider.pathCount;
        Vector2[] pathVertices = new Vector2[tilemapCollider.pointCount]; // Буфер для вершин

        for (int i = 0; i < pathCount; i++)
        {
            int pointsInPath = tilemapCollider.GetPath(i, pathVertices); // Получаем вершины i-го контура

            // Создаем под-объект для этого контура теней
            GameObject shadowCasterObject = new GameObject($"ShadowCaster_{i}");
            shadowCasterObject.transform.SetParent(shadowCasterContainer.transform); // Помещаем в контейнер
            shadowCasterObject.transform.localPosition = Vector3.zero;
            shadowCasterObject.transform.localRotation = Quaternion.identity;
            shadowCasterObject.transform.localScale = Vector3.one;

            // --- Важный шаг: Добавляем PolygonCollider2D и копируем путь ---
            PolygonCollider2D polygonCollider = shadowCasterObject.AddComponent<PolygonCollider2D>();
            // Важно: нужно создать новый массив правильного размера для этого контура
            Vector2[] currentPathPoints = new Vector2[pointsInPath];
            System.Array.Copy(pathVertices, currentPathPoints, pointsInPath);
            polygonCollider.points = currentPathPoints;
            polygonCollider.enabled = false; // Отключаем коллайдер, т.к. он нужен только для формы ShadowCaster

            // --- Добавляем ShadowCaster2D ---
            ShadowCaster2D shadowCaster = shadowCasterObject.AddComponent<ShadowCaster2D>();
            shadowCaster.selfShadows = false; // Обычно для тайлмапов самозатенение не нужно

            shadowCasterObjects.Add(shadowCasterObject); // Добавляем в список для возможного удаления
        }

        Debug.Log($"Сгенерировано {pathCount} контуров ShadowCaster для {gameObject.name}");
    }

    // Добавляем кнопку для очистки
    [ContextMenu("Clear Shadow Casters")]
    public void ClearShadowCastersPublic()
    {
        ClearExistingShadowCasters();
        Debug.Log($"Очищены Shadow Casters для {gameObject.name}");
    }


    private void ClearExistingShadowCasters()
    {
        // Удаляем объекты из списка в обратном порядке
        for (int i = shadowCasterObjects.Count - 1; i >= 0; i--)
        {
            if (shadowCasterObjects[i] != null) // Проверяем, не был ли объект удален вручную
            {
                // Используем DestroyImmediate, если вызываем из редактора вне режима Play
                if (Application.isEditor && !Application.isPlaying)
                {
                    DestroyImmediate(shadowCasterObjects[i]);
                }
                else
                {
                    Destroy(shadowCasterObjects[i]);
                }
            }
        }
        shadowCasterObjects.Clear(); // Очищаем список
    }

    // Для примера: если коллайдер не назначен в инспекторе, попробуем найти его при старте
    void Awake()
    {
        if (tilemapCollider == null)
        {
            tilemapCollider = GetComponent<CompositeCollider2D>();
        }
        // Генерировать при старте обычно не нужно, делаем это через меню
    }
}
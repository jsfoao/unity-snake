using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Spawnable entities")]
    [SerializeField] private GameObject aiSnakePrefab;
    [SerializeField] private GameObject playerSnakePrefab;
    [SerializeField] private GameObject[] gridObjectPrefabs;
    
    [Header("Transform parents")]
    [SerializeField] private Transform spawnedObjectsEmpty;
    [SerializeField] private Transform spawnedSnakesEmpty;
    
    [SerializeField] public List<GridObject> spawnedObjects;
    [NonSerialized] public List<Snake> spawnedSnakes;

    private Map map;
    private Transform parent;

    #region Methods
    // Spawn snake on tile
    public void SpawnSnake(Tile tile, Color color, int size = 1, bool isControlled = false)
    {
        if (tile == null)
        {
            Debug.Log("Couldn't spawn snake. Tile outside of map range");
            return;
        }
        parent = spawnedSnakesEmpty;
        GameObject prefabToSpawn = isControlled ? playerSnakePrefab : aiSnakePrefab;
        GameObject instance = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity, parent);
        Snake newSnake = instance.GetComponent<Snake>();
        newSnake.Create(tile, size, color);
        
        spawnedSnakes.Add(newSnake);
    }

    // Spawn snake on random tile
    public void SpawnSnakeInRandomTile(Color color, int size = 1, bool isControlled = false)
    {
        Tile randomValidTile = RandomValidTile();
        if (randomValidTile != null)
        {
            SpawnSnake(randomValidTile, color, size, isControlled);
        }
    }
    
    // Spawn object on random tile
    public void SpawnObjectOfTypeInRandomTile(PickupType pickupType)
    {
        Tile randomValidTile = RandomValidTile();
        if (randomValidTile != null)
        {
            SpawnObjectOfType(pickupType, randomValidTile);
        }
        else { Debug.Log("Couldn't spawn object: No possible valid tiles"); }
    }
    
    // Spawn object on tile
    public GameObject SpawnObjectOfType(PickupType pickupType, Tile tile)
    {
        GameObject objectToSpawn = FindPrefabOfType(pickupType);
        if (objectToSpawn == null)
        {
            Debug.Log($"Couldn't find object of type {pickupType}");
            return null;
        }
        GameObject instance = Instantiate(objectToSpawn, tile.worldPosition, Quaternion.identity, spawnedObjectsEmpty);
        GridObject gridObject = instance.GetComponent<GridObject>();
        gridObject.currentTile = tile;
        tile.currentObjects.Add(gridObject);
        spawnedObjects.Add(gridObject);
        return instance;
    }

    // Destroy object
    public void DestroyObject(GridObject gridObject)
    {
        gridObject.currentTile.currentObjects.Remove(gridObject);
        spawnedObjects.Remove(gridObject);
        Destroy(gridObject.gameObject);
    }
    #endregion

    #region Other Methods
    private Tile RandomValidTile()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector2Int randomPosition = new Vector2Int(Random.Range(0, map.size.x), Random.Range(0, map.size.y));
            Tile tileToSpawnOn = map.tileGrid[randomPosition.x, randomPosition.y];
            if (tileToSpawnOn.walkable && tileToSpawnOn.currentObjects.Count == 0) { return tileToSpawnOn; }
        }
        return null;
    }

    private GameObject FindPrefabOfType(PickupType pickupType)
    {
        foreach (var prefab in gridObjectPrefabs)
        {
            GridObject currentObject = prefab.GetComponent<GridObject>();
            if (currentObject.pickupType == pickupType)
            {
                return prefab;
            }
        }
        return null;
    }

    public Color RandomColor()
    {
        Color color = new Color
        {
            r = Random.Range(0f, .7f),
            g = Random.Range(0f, .7f),
            b = Random.Range(0f, .7f),
            a = 1f
        };
        return color;
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        map = GetComponent<Map>();
        parent = transform.GetChild(1);
        spawnedObjects = new List<GridObject>();
        spawnedSnakes = new List<Snake>();
    }
    #endregion
}

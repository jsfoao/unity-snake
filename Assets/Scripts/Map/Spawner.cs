using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Spawnable entities")]
    [SerializeField] private GameObject entitySnakePrefab;
    [SerializeField] private GameObject playerSnakePrefab;
    [SerializeField] private GameObject gridObjectPrefabs;
    
    [Header("Transform parents")]
    [SerializeField] private Transform spawnedObjectsEmpty;
    [SerializeField] private Transform spawnedSnakesEmpty;
    
    [SerializeField] public List<GridObject> spawnedObjects;
    [SerializeField] public List<Snake> spawnedSnakes;

    private Map map;
    private Transform parent;

    private void SpawnSnake(Tile tile, int size = 3, bool isControlled = false)
    {
        if (tile == null)
        {
            Debug.Log("Couldn't spawn snake. Tile outside of map range");
            return;
        }
        
        parent = spawnedSnakesEmpty;
        GameObject prefabToSpawn = isControlled ? playerSnakePrefab : entitySnakePrefab;
        GameObject instance = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity, parent);
        Snake newSnake = instance.GetComponent<Snake>();
        newSnake.Create(tile, size);
        
        spawnedSnakes.Add(newSnake);
    }

    public void SpawnRandomFruit()
    {
        Tile randomValidTile = RandomValidTile();
        if (randomValidTile != null)
        {
            SpawnObject(gridObjectPrefabs, randomValidTile);
        }
        else { Debug.Log("Game over: No more space for fruits"); }
    }

    private Tile RandomValidTile()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector2Int randomPosition = new Vector2Int(Random.Range(0, map.size.x), Random.Range(0, map.size.y));
            Tile tileToSpawnOn = map.tileGrid[randomPosition.x, randomPosition.y];
            if (tileToSpawnOn.walkable) { return tileToSpawnOn; }
        }
        return null;
    }
    
    private GameObject SpawnObject(GameObject gridObjectPrefab, Tile tile)
    {
        GameObject instance = Instantiate(gridObjectPrefab, tile.worldPosition, Quaternion.identity, spawnedObjectsEmpty);
        GridObject gridObject = instance.GetComponent<GridObject>();
        gridObject.currentTile = tile;
        tile.currentObjects.Add(instance);
        spawnedObjects.Add(gridObject);
        return instance;
    }
    
    public void DestroyObject(GameObject gameObject)
    {
        GridObject gridObject = gameObject.GetComponent<GridObject>();
        gridObject.currentTile.currentObjects.Remove(gameObject);
        spawnedObjects.Remove(gridObject);
        Destroy(gameObject);
    }
    
    private void Awake()
    {
        map = GetComponent<Map>();
        parent = transform.GetChild(1);
    }

    private void Start()
    {
        SpawnRandomFruit();
        SpawnSnake(map.tileGrid[0, 0], 1);
        SpawnSnake(map.tileGrid[0, 1], 1);
        SpawnSnake(map.tileGrid[0, 2], 1);
        SpawnSnake(map.tileGrid[0, 3], 1);
        SpawnSnake(map.tileGrid[0, 4], 1);
        SpawnSnake(map.tileGrid[0, 5], 1);
        SpawnSnake(map.tileGrid[0, 6], 1);
    }
}

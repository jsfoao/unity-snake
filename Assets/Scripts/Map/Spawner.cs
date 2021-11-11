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

    [NonSerialized] public List<Color> possibleColors;

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
        tile.currentObjects.Add(gridObject);
        spawnedObjects.Add(gridObject);
        return instance;
    }
    
    public void DestroyObject(GridObject gridObject)
    {
        gridObject.currentTile.currentObjects.Remove(gridObject);
        spawnedObjects.Remove(gridObject);
        Destroy(gridObject.gameObject);
    }
    
    private void Awake()
    {
        map = GetComponent<Map>();
        parent = transform.GetChild(1);
        possibleColors = new List<Color>();
        possibleColors.Add(Color.magenta);
        possibleColors.Add(Color.red);
        possibleColors.Add(Color.blue);
        possibleColors.Add(Color.yellow);
    }

    private void Start()
    {
        SpawnRandomFruit();
        SpawnSnake(map.tileGrid[0, 0], 3);
        SpawnSnake(map.tileGrid[0, 2], 3);
        SpawnSnake(map.tileGrid[0, 4], 3);
        SpawnSnake(map.tileGrid[0, 6], 3);
        SpawnSnake(map.tileGrid[0, 8], 3);
    }
}

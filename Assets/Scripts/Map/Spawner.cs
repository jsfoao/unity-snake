using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject entitySnakePrefab;
    [SerializeField] private GameObject playerSnakePrefab;
    [SerializeField] private GameObject gridObjectPrefabs;
    [SerializeField] private List<GameObject> spawnedObjects;

    private Map map;
    private Transform parent;

    private void SpawnSnake(Tile tile, int size = 3, bool isControlled = false)
    {
        parent = transform.GetChild(2);
        GameObject prefabToSpawn = isControlled ? playerSnakePrefab : entitySnakePrefab;
        GameObject instance = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity, parent);
        Snake newSnake = instance.GetComponent<Snake>();
        newSnake.Create(tile, size);
    }
    
    public void SpawnRandomFruit()
    {
        Tile randomValidTile = RandomValidTile();
        if (randomValidTile != null)
        {
            SpawnObject(gridObjectPrefabs, randomValidTile);
        }
        else
        {
            Debug.Log("Game over: No more space for fruits");
        }
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
        GameObject instance = Instantiate(gridObjectPrefab, tile.worldPosition, Quaternion.identity, parent);
        GridObject gridObject = instance.GetComponent<GridObject>();
        gridObject.currentTile = tile;
        tile.currentObjects.Add(instance);
        spawnedObjects.Add(instance);
        return instance;
    }
    
    public void DestroyObject(GameObject gameObject)
    {
        GridObject gridObject = gameObject.GetComponent<GridObject>();
        gridObject.currentTile.currentObjects.Remove(gameObject);
        spawnedObjects.Remove(gameObject);
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
    }
}

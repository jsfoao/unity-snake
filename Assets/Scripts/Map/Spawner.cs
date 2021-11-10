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
        Vector2Int randomPosition = new Vector2Int(Random.Range(0, map.size.x), Random.Range(0, map.size.y));
        SpawnObject(gridObjectPrefabs, map.tileGrid[randomPosition.x, randomPosition.y]);
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
        SpawnRandomFruit();
        SpawnRandomFruit();
        
        SpawnSnake(map.tileGrid[0, 0], 3, true);
        SpawnSnake(map.tileGrid[10, 0], 8);
        SpawnSnake(map.tileGrid[20, 0], 15);
    }
}

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gridObjectPrefabs;
    [SerializeField] private List<GameObject> spawnedObjects;
    
    private Map map;
    private Transform parent;

    public void SpawnRandomFruit()
    {
        Vector2Int randomPosition = new Vector2Int(Random.Range(0, map.size.x), Random.Range(0, map.size.y));
        Debug.Log(randomPosition);
        SpawnObject(gridObjectPrefabs, map.tileGrid[randomPosition.x, randomPosition.y]);
        Debug.Log($"Spawn new fruit at {randomPosition}");
    }
    public GameObject SpawnObject(GameObject gridObjectPrefab, Tile tile)
    {
        GameObject instance = Instantiate(gridObjectPrefab, tile.worldPosition, Quaternion.identity, parent);
        GridObject gridObject = instance.GetComponent<GridObject>();
        gridObject.currentTile = tile;
        tile.currentObject = instance;
        spawnedObjects.Add(instance);
        return instance;
    }
    
    public void DestroyObject(GameObject gameObject)
    {
        GridObject gridObject = gameObject.GetComponent<GridObject>();
        gridObject.currentTile.currentObject = null;
        spawnedObjects.Remove(gameObject);
        Destroy(gameObject);
    }
    
    public void Start()
    {
        map = GetComponent<Map>();
        parent = transform.GetChild(1);

        SpawnRandomFruit();
    }
}

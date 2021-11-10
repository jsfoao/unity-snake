using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // List of linked bodies
    public LList<Body> bodyParts;
    
    // Size of snake when spawned
    [SerializeField] [Tooltip("Size of snake when spawned")] 
    private int initialSize;
    
    // Current size of snake
    private int size;

    // Reference to prefab
    [Header("Temp")] 
    [SerializeField] private GameObject bodyPrefab;
    
    public Body AddBody()
    {
        Body body;
        GameObject spawnedGameObject;
        if (bodyParts.Count == 0)
        {
            spawnedGameObject = Instantiate(bodyPrefab, transform);
            body = spawnedGameObject.GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = null;
            bodyParts.AddLast(body);
        }
        else
        {
            spawnedGameObject = Instantiate(bodyPrefab, bodyParts.Tail.Item.transform.position, Quaternion.identity, transform);
            body = spawnedGameObject.GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = bodyParts.Tail.Item.currentTile;
            bodyParts.AddLast(body);
        }
        size++;
        return body;
    }
    
    public void Create(Tile tile, int createSize)
    {
        // Spawn head on tile
        Body headBody = AddBody();
        headBody.currentTile = tile;
        // Spawn rest of body on adjacent neighbour tiles
        for (int i = 1; i < createSize; i++)
        {
            AddBody();
        }
    }

    private void Awake()
    {
        bodyParts = new LList<Body>();
    }
}

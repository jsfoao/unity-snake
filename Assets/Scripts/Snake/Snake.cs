using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public LList<Body> bodyParts;
    [SerializeField] private int initialSize;
    private int size;

    [Header("Temp")] 
    [SerializeField] private GameObject bodyPrefab;

    public Body AddBody()
    {
        Body body;

        if (bodyParts.Count == 0)
        {
            body = Instantiate(bodyPrefab, transform).GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = null;
            bodyParts.AddLast(body);
        }
        else
        {
            body = Instantiate(bodyPrefab, bodyParts.Tail.Item.transform.position, Quaternion.identity, transform).GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = bodyParts.Tail.Item.currentTile;
            bodyParts.AddLast(body);
        }
        size++;
        return body;
    }
    
    public void Create(Tile tile)
    {
        // Spawn head on tile
        Body headBody = AddBody();
        headBody.currentTile = tile;
        // Spawn rest of body on adjacent neighbour tiles
        for (int i = 1; i < initialSize; i++)
        {
            AddBody();
        }
    }

    private void Start()
    {
        bodyParts = new LList<Body>();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public LList<Body> bodyParts;
    [SerializeField] public int size;

    [Header("Temp")] 
    [SerializeField] private GameObject bodyPrefab;

    public void AddBody(Tile tile)
    {
        Body body;

        if (bodyParts.Count == 0)
        {
            body = Instantiate(bodyPrefab, transform).GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = tile;
            bodyParts.AddLast(body);
        }
        else
        {
            body = Instantiate(bodyPrefab, transform).GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = tile;
            bodyParts.AddLast(body);
        }
    }
    
    public void Create(Tile tile)
    {
        // Spawn head on tile
        AddBody(tile);

        // Spawn rest of body on adjacent neighbour tiles
        for (int i = 1; i < size; i++)
        {
            AddBody(bodyParts.Tail.Item.currentTile.neighbourTiles[(int)Direction.Down]);
        }
        
        Debug.Log($"Snake of size {size} spawned on {tile.name}");
    }

    private void Start()
    {
        bodyParts = new LList<Body>();
    }
}

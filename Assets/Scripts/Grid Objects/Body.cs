using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GridObject gridObject;
    [NonSerialized] public Tile previousTile;
    [NonSerialized] public Snake snake;
    public bool linked;

    public void MoveToTile(Tile tile)
    {
        if (!linked) { return; }
        if (tile == null) { return; }
        transform.position = tile.worldPosition;
        Tile temp = gridObject.currentTile;
        previousTile = temp;
        gridObject.currentTile = tile;
        
        UpdateTileProperties();
    }
    
    private void UpdateTileProperties()
    {
        previousTile.walkable = true;
        gridObject.currentTile.walkable = false;
        previousTile.currentObjects.Remove(gridObject);
        gridObject.currentTile.currentObjects.Add(gridObject);
    }

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
    }
}
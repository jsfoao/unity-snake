using System;
using UnityEngine;

public class Body : GridObject
{
    [NonSerialized] public Tile previousTile;

    public void MoveToTile(Tile tile)
    {
        if (tile == null) { return; }
        transform.position = tile.worldPosition;
        Tile temp = currentTile;
        previousTile = temp;
        currentTile = tile;
        
        UpdateTileProperties();
    }
    
    private void UpdateTileProperties()
    {
        previousTile.walkable = true;
        currentTile.walkable = false;
        previousTile.currentObjects.Remove(this);
        currentTile.currentObjects.Add(this);
    }

    private void Awake()
    {
        objectType = ObjectType.Body;
    }
}

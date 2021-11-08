using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [NonSerialized] public Tile currentTile;
    [NonSerialized] public Tile previousTile;

    public Tile CurrentTile
    {
        get => currentTile;
        set
        {
            currentTile = value;
            // MoveToTile(currentTile);
        }
    }
    
    public void MoveToTile(Tile tile)
    {
        if (tile == null) { return; }
        transform.position = tile.worldPosition;
        Tile temp = currentTile;
        previousTile = temp;
        currentTile = tile;
    }
}

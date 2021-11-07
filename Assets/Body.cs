using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Body : MonoBehaviour
{
    [NonSerialized] public Body previous;
    [NonSerialized] public Tile currentTile;
    
    public void MoveToTile(Tile tile)
    {
        if (tile == null) { return; }
        transform.position = tile.worldPosition;
        currentTile = tile;
    }
}

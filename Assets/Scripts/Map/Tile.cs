using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Grid
    [NonSerialized] public Vector2Int gridPosition;
    [NonSerialized] public Vector2 worldPosition;
    [NonSerialized] public Tile[] neighbourTiles = new Tile[4];
    
    // Objects
    [NonSerialized] public List<GameObject> currentObjects;
    
    // Pathfinding
    [NonSerialized] public Tile parentTile;
    [NonSerialized] public int gCost;
    [NonSerialized] public int hCost;
    public int fCost => gCost + hCost;

    private void Awake()
    {
        currentObjects = new List<GameObject>();
    }
}

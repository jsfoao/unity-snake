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
    [SerializeField] public List<GridObject> currentObjects;
    
    // Pathfinding
    [NonSerialized] public Tile parentTile;
    [NonSerialized] public int gCost;
    [SerializeField] public int hCost;
    
    [NonSerialized] public bool walkable;
    
    public int fCost => gCost + hCost;

    private void Awake()
    {
        currentObjects = new List<GridObject>();
        walkable = true;
    }
}

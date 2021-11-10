using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Vector2Int startTilePos;
    [SerializeField] private Vector2Int targetTilePos;
    
    private const int STRAIGHT_COST = 10;

    private Map _map;
    [SerializeField] private List<Tile> pathList;

    public List<Tile> FindPath(Tile startTile, Tile targetTile)
    {
        if (startTile == targetTile)
            return null;

        List<Tile> openList = new List<Tile>(); 
        HashSet<Tile> closedList = new HashSet<Tile>();
        openList.Add(startTile);

        while (openList.Count > 0)
        {
            // Iterate through Open List to get tile with lowest F Cost
            Tile currentTile = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentTile.fCost ||
                    openList[i].fCost == currentTile.fCost && openList[i].hCost < currentTile.hCost)
                {
                    currentTile = openList[i];
                }
            }
            
            // Moving tile from openList to closedList
            openList.Remove(currentTile);
            closedList.Add(currentTile);
            
            // If reached target tile, path was found
            if (currentTile == targetTile)
            {
                return BacktrackPath(startTile, targetTile);
            }

            // Evaluate all neighbour tiles
            foreach (Tile neighbourTile in currentTile.neighbourTiles)
            {
                if (closedList.Contains(neighbourTile))
                    continue;

                int movementCost = currentTile.gCost + TileDistanceCost(currentTile, neighbourTile);
               
                // Neighbour tile is possible path?
                if (movementCost < neighbourTile.gCost || !openList.Contains(neighbourTile))
                {
                    // Update cost values
                    neighbourTile.gCost = movementCost;
                    startTile.hCost = TileDistanceCost(currentTile, targetTile);
                    neighbourTile.parentTile = currentTile;
                    
                    // Add neighbour tile to open list
                    if (!openList.Contains(neighbourTile))
                    {
                        openList.Add(neighbourTile);
                    }
                }
            }
        }
        return null;
    }

    private List<Tile> BacktrackPath(Tile startTile, Tile targetTile)
    {
        List<Tile> tilePath = new List<Tile>();
        Tile currentTile = targetTile;
        
        // Add saved parents of each tile to tilePath
        while (currentTile != startTile)
        {
            tilePath.Add(currentTile);
            currentTile = currentTile.parentTile;
        }
        tilePath.Reverse();
        return tilePath;
    }
    
    private int TileDistanceCost(Tile tileA, Tile tileB)
    {
        int cost = 0;
        if (tileA == null || tileB == null)
            return cost;

        int dx = Math.Abs(tileA.gridPosition.x - tileB.gridPosition.x);
        int dy = Math.Abs(tileA.gridPosition.y - tileB.gridPosition.y);
        int distToTriangle = Math.Abs(dx - dy);

        cost = distToTriangle * STRAIGHT_COST;
        return cost;
    }
    
    private void Awake()
    {
        _map = GetComponent<Map>();
    }

    private void Update()
    {
        startTilePos.x = Mathf.Clamp(startTilePos.x, 0, _map.size.x - 1);
        startTilePos.y = Mathf.Clamp(startTilePos.y, 0, _map.size.y - 1);
        targetTilePos.x = Mathf.Clamp(targetTilePos.x, 0, _map.size.x - 1);
        targetTilePos.y = Mathf.Clamp(targetTilePos.y, 0, _map.size.y - 1);
        Tile tile1 = _map.tileGrid[startTilePos.x, startTilePos.y];
        Tile tile2 = _map.tileGrid[targetTilePos.x, targetTilePos.y];
        pathList = FindPath(tile1, tile2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var tile in pathList)
        {
            Gizmos.DrawCube(tile.worldPosition, new Vector3(.5f, .5f, .5f));
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AIFinder))]
public class AIController : EntityController
{
    private Map map;
    private Spawner _spawner;
    private Pathfinding pathfinding;
    private AIFinder aiFinder;

    private List<Tile> currentTilePath;

    // To perform every tick
    public override void MovementTick()
    {
        ResetPathCosts();
        
        // If can find a lowest cost object
        GridObject lowestCostObject = aiFinder.LowestCostObject();
        if (lowestCostObject != null)
        {
            Tile targetTile = lowestCostObject.currentTile;
            
            // Find path to object
            currentTilePath = pathfinding.FindPath(headBody.gridObject.currentTile, targetTile);

            // Move along path if it's valid
            if (currentTilePath != null)
            {
                MoveHeadToTile(currentTilePath[0]);
                return;
            }
        }

        // If can't find an object, it will try to move to a random valid neighbour
        Tile randomNeighbour = RandomValidNeighbour();
        if (randomNeighbour == null)
        {
            // Move up and die if there's no valid neighbours
            MoveHeadToTile(headBody.gridObject.currentTile.neighbourTiles[(int)Direction.Up]);
            return;
        }
        // Move in random direction if path wasn't found
        MoveHeadToTile(RandomValidNeighbour());
    }
    
    // Move head to chosen tile
    private void MoveHeadToTile(Tile tile)
    {
        headBody.MoveToTile(tile);
        EvaluateBodyPositions();
    }

    // Randomize until finding valid neighbour
    private Tile RandomValidNeighbour()
    {
        List<Tile> possibleTiles = new List<Tile>();
        foreach (Tile tile in headBody.gridObject.currentTile.neighbourTiles)
        {
            if (tile.walkable)
            {
                possibleTiles.Add(tile);
            }
        }

        // No possible valid tiles found
        if (possibleTiles.Count == 0) { return null; }
        
        // Return random possible tile
        int randomIndex = Random.Range(0, possibleTiles.Count);
        return possibleTiles[randomIndex];
    }

    private void ResetPathCosts()
    {
        for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                map.tileGrid[x, y].ResetCost();
            }
        }
    }

    public override void Start()
    {
        base.Start();
        // Object components
        aiFinder = GetComponent<AIFinder>();
        
        pathfinding = FindObjectOfType<Pathfinding>();
        map = FindObjectOfType<Map>();
        _spawner = FindObjectOfType<Spawner>();
    }
    
    private void OnDrawGizmos()
    {
        if (currentTilePath == null) { return; }
        Gizmos.color = Color.black;
        for (int i = 0; i < currentTilePath.Count; i++)
        {
            if (i == currentTilePath.Count - 1) { return; }
            Gizmos.DrawLine(currentTilePath[i].worldPosition, currentTilePath[i + 1].worldPosition);
        }
    }
}

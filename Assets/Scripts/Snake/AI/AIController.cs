using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class AIController : EntityController
{
    private Map map;
    private Spawner _spawner;
    private Pathfinding pathfinding;

    [SerializeField] private List<Tile> currentTilePath;

    public void MoveHeadToTile(Tile tile)
    {
        headBody.MoveToTile(tile);
        EvaluateBodyPositions();
    }
    
    // todo create separate item finder
    private Tile FindFruit()
    {
        foreach (GridObject fruit in _spawner.spawnedObjects)
        {
            if (fruit.type == ObjectType.Fruit)
            {
                return fruit.currentTile;
            }
        }
        return null;
    }

    // Randomize until finding valid neighbour
    private Tile RandomValidNeighbour()
    {
        List<Tile> possibleTiles = new List<Tile>();
        foreach (Tile tile in headBody.currentTile.neighbourTiles)
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
    
    
    public override void MovementTick()
    {
        // Check for new headBody every tick
        if (snake.bodyParts != null)
        {
            headBody = snake.bodyParts.Head.Item;
        }

        Tile targetTile = FindFruit();

        currentTilePath = pathfinding.FindPath(headBody.currentTile, targetTile);
        
        // 0 because it currently calculates new path after every move
        if (currentTilePath != null)
        {
            MoveHeadToTile(currentTilePath[0]);
        }
        else
        {
            Tile randomNeighbour = RandomValidNeighbour();
            
            // no possible way out
            if (randomNeighbour == null)
            {
                MoveHeadToTile(headBody.currentTile.neighbourTiles[(int)Direction.Up]);
            }
            MoveHeadToTile(RandomValidNeighbour());
        }
    }

    public override void Start()
    {
        base.Start();
        pathfinding = GetComponent<Pathfinding>();
        map = FindObjectOfType<Map>();
        _spawner = FindObjectOfType<Spawner>();
    }
}

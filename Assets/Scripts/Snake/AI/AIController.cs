using System.Collections.Generic;
using System.IO;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
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
    
    // todo fix issue
    private Tile FindFruit()
    {
        foreach (GridObject go in _spawner.spawnedObjects)
        {
            Fruit fruit = go.GetComponent<Fruit>();
            if (go.objectType == ObjectType.Fruit)
            {
                return fruit.currentTile;
            }
        }
        return null;
    }

    // Randomize until finding valid neighbour
    private Tile RandomValidNeighbour()
    {
        for (int i = 0; i < 20; i++)
        {
            int randomDirection = Random.Range(0, 4);
            Tile newTile = headBody.currentTile.neighbourTiles[randomDirection];
            if (newTile.walkable) { return newTile; }
        }
        return null;
    }
    
    
    public override void TickUpdate()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
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
                // game over
                Debug.Log($"{snake.name} collided with self and died");
                snake.DestroySelf();
                if (_spawner.spawnedSnakes.Count == 1)
                {
                    Time.timeScale = 0f;
                    Debug.Log($"{_spawner.spawnedSnakes[0]} is winner winner chicken dinner!");
                }
            }
            MoveHeadToTile(RandomValidNeighbour());
        }
        HandleCollisions();
        currentTime = tick;
    }

    public override void Start()
    {
        base.Start();
        pathfinding = GetComponent<Pathfinding>();
        map = FindObjectOfType<Map>();
        _spawner = FindObjectOfType<Spawner>();
    }
}

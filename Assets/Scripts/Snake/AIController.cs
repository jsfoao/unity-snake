using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class AIController : EntityController
{
    private enum State { Wander, Focus }
    private State _state = State.Focus;
    private Map map;
    private Pathfinding pathfinding;
    
    [SerializeField] private List<Tile> currentTilePath;

    public void MoveHeadToTile(Tile tile)
    {
        headBody.MoveToTile(tile);
        EvaluateBodyPositions();
    }
    
    private Tile FindFruit()
    {
        for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                foreach (GameObject go in map.tileGrid[x, y].currentObjects)
                {
                    if (go.GetComponent<Fruit>() != null)
                    {
                        return map.tileGrid[x, y];
                    }
                }
            }
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
        if (targetTile != null)
        {
            Debug.Log(targetTile);
        }
        

        currentTilePath = pathfinding.FindPath(headBody.currentTile, targetTile);
        
        // 0 because it currently calculates new path after every move
        MoveHeadToTile(currentTilePath[0]);
        HandleCollisions();

        currentTime = tick;
    }
    
    public override void Start()
    {
        base.Start();
        pathfinding = GetComponent<Pathfinding>();
        map = FindObjectOfType<Map>();
        _state = State.Focus;
    }

}

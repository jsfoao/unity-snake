using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;
using Direction = UnityEngine.Direction;

// snake will depend on map
// snake will move dependent on tile grid
// snake wont have independent movement
// head of snake will control movement
[RequireComponent(typeof(Snake))]
public class SnakeController : MonoBehaviour, IEntityController
{
    private Direction _currentDirection;
    private Direction _desiredDirection;
    private Direction _oppositeDirection;
    private Transform _transform;
    private Vector2 worldPosition;

    // Tick
    private float currentTime;
    [SerializeField] [Tooltip("Time in seconds to move to next tile")] 
    private float tick;
    
    private Snake snake;
    private Map map;
    private Spawner spawner;

    private Body headBody;
    
    // Set linked body positions
    public void EvaluateBodyPositions()
    {
        // Start traversing from head
        var bodyNode = snake.bodyParts.Head;

        while (bodyNode.Next != null)
        {
            bodyNode.Next.Item.MoveToTile(bodyNode.Item.previousTile);
            bodyNode = bodyNode.Next;
        }
    }

    // Locks snake from moving on opposite direction of its current movement
    private bool IsValidMovement()
    {
        if (_desiredDirection == Direction.Left && _currentDirection == Direction.Right)
        {
            return false;
        }
        if (_desiredDirection == Direction.Right && _currentDirection == Direction.Left)
        {
            return false;
        }
        if (_desiredDirection == Direction.Down && _currentDirection == Direction.Up)
        {
            return false;
        }
        if (_desiredDirection == Direction.Up && _currentDirection == Direction.Down)
        {
            return false;
        }
        return true;
    }
    
    // Handle head of snake movement
    public void HandleMovement()
    {
        // Check if last input is a valid move
        if (IsValidMovement())
        {
            _currentDirection = _desiredDirection;
        }
        
        // Move head of snake to tile set by input
        headBody.MoveToTile(headBody.currentTile.neighbourTiles[(int)_currentDirection]);

        // Evaluate remaining body positions
        EvaluateBodyPositions();
    }

    // Handle head collisions
    private void HandleCollisions()
    {
        // "Collisions"
        // if tile has object
        List<GameObject> objectsToCheck = headBody.currentTile.currentObjects;
        if (objectsToCheck != null)
        {
            int bodyCounter = 0;
            foreach (GameObject go in objectsToCheck)
            {
                if (go.GetComponent<Body>() != null)
                {
                    bodyCounter++;
                }

                if (bodyCounter >= 2)
                {
                    Time.timeScale = 0f;
                    Debug.Log("GameOver");
                }
                
                if (go.GetComponent<Fruit>() != null)
                {
                    spawner.DestroyObject(go);
                    spawner.SpawnRandomFruit();
                    snake.AddBody();
                    return;
                }
            }
        }
    }
    
    // Check for input
    private void HandleInput()
    {
        // handling input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _desiredDirection = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _desiredDirection = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _desiredDirection = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _desiredDirection = Direction.Down;
        }
    }
    
    // Runs every tick seconds
    private void TickUpdate()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
        // Check for new headBody every tick
        if (snake.bodyParts != null)
        {
            headBody = snake.bodyParts.Head.Item;
        }
        
        // Snake movement
        HandleMovement();
        
        // Snake collisions
        HandleCollisions();
        
        currentTime = tick;
    }
    
    private void Update()
    {
        HandleInput();
        TickUpdate();
    }

    // Do after map spawning
    private void Start()
    {
        snake = GetComponent<Snake>();
        map = FindObjectOfType<Map>();
        spawner = FindObjectOfType<Spawner>();
        _desiredDirection = Direction.Up;
        _currentDirection = Direction.Up;
    }
}

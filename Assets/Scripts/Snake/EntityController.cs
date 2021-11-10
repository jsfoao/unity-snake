using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Snake))]
public class EntityController : MonoBehaviour, IEntityController
{
    private Direction _currentDirection;
    private Direction _desiredDirection;
    private Direction _oppositeDirection;
    private Transform _transform;
    private Vector2 worldPosition;

    // Tick
    [NonSerialized] public float currentTime;
    [SerializeField] [Tooltip("Time in seconds to move to next tile")] 
    public float tick;
    
    [NonSerialized] public Snake snake;
    private Spawner spawner;

    [NonSerialized] public Body headBody;
    [NonSerialized] public bool enableInput;
    
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

    // Locks snake from moving to previous tile
    private bool IsValidMovement()
    {
        return headBody.currentTile.neighbourTiles[(int)_desiredDirection] != headBody.previousTile;
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
    public void HandleCollisions()
    {
        // "Collisions"
        // if tile has object
        List<GameObject> objectsToCheck = headBody.currentTile.currentObjects;
        int bodyCounter = 0;
        foreach (GameObject go in objectsToCheck)
        {
            // Collision with self
            Body bodyToCheck = go.GetComponent<Body>();
            if (bodyToCheck != null) 
            { 
                if (snake.bodyParts.Contains(bodyToCheck)) 
                { 
                    bodyCounter++;
                }
                else 
                { 
                    Debug.Log("Collision with different snake");
                }
            }
            // if there's 2 bodies of same snake then collision with self exists
            if (bodyCounter >= 2)
            { 
                Time.timeScale = 0f; 
                Debug.Log("Game Over!");
            }
            
            // Collision with fruit
            if (go.GetComponent<Fruit>() != null)
            { 
                spawner.DestroyObject(go);
                spawner.SpawnRandomFruit();
                snake.AddBody();
                return;
                
            }
        }
    }

    public void ChangeDirection(Direction direction)
    {
        _desiredDirection = direction;
        
        // Check if last input is a valid move
        if (IsValidMovement())
        {
            _currentDirection = _desiredDirection;
        }
    }

    // Runs every tick seconds
    public virtual void TickUpdate()
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
    
    public virtual void Update()
    {
        TickUpdate();
    }

    // Do after map spawning
    public virtual void Start()
    {
        snake = GetComponent<Snake>();
        spawner = FindObjectOfType<Spawner>();
        _desiredDirection = Direction.Up;
        _currentDirection = Direction.Up;
        enableInput = true;
    }
}

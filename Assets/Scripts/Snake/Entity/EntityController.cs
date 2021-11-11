using System;
using UnityEngine;

[RequireComponent(typeof(Snake))]
public class EntityController : MonoBehaviour, IEntityController
{
    private Direction _currentDirection;
    private Direction _desiredDirection;
    private Direction _oppositeDirection;
    private Transform _transform;
    private Vector2 worldPosition;

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
    
    public void ChangeDirection(Direction direction)
    {
        _desiredDirection = direction;
        
        // Check if last input is a valid move
        if (IsValidMovement())
        {
            _currentDirection = _desiredDirection;
        }
    }

    // Handle movement
    public virtual void MovementTick()
    {
        // Check for new headBody every tick
        if (snake.bodyParts != null)
        {
            headBody = snake.bodyParts.Head.Item;
        }
        
        // Check if last input is a valid move
        if (IsValidMovement())
        {
            _currentDirection = _desiredDirection;
        }

        // Move head of snake to tile set by input
        Tile newTile = headBody.currentTile.neighbourTiles[(int) _currentDirection];
        headBody.MoveToTile(newTile);

        // Evaluate remaining body positions
        EvaluateBodyPositions();
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

using System;
using System.Collections;
using UnityEngine;

// snake will depend on map to work
// snake will move dependent on tile grid
// snake wont have independent movement
public class SnakeController : MonoBehaviour, IEntityController
{
    [SerializeField] private Direction currentDirection;
    [SerializeField] private Tile currentTile;
    [SerializeField] private float step;
    private Map map;
    private Transform _transform;
    private Vector2 worldPosition;

    // tick
    private float currentTime;
    [SerializeField] private float tick;
    
    private enum Direction { Left, Right, Up, Down }
    
    public void Move(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            if (currentTile.neighbourTileLeft == null) { return; }
            currentTile = currentTile.neighbourTileLeft;
        }
        if (direction == Vector2.right)
        {
            if (currentTile.neighbourTileRight == null) { return; }
            currentTile = currentTile.neighbourTileRight;
        }
        if (direction == Vector2.down)
        {
            if (currentTile.neighbourTileDown == null) { return; }
            currentTile = currentTile.neighbourTileDown;
        }
        if (direction == Vector2.up)
        {
            if (currentTile.neighbourTileUp == null) { return; }
            currentTile = currentTile.neighbourTileUp;
        }
        _transform.position += (Vector3)direction * step;
    }

    private void TickMovement()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (currentDirection == Direction.Left) { Move(Vector2.left); }
            if (currentDirection == Direction.Right) { Move(Vector2.right); }
            if (currentDirection == Direction.Up) { Move(Vector2.up); }
            if (currentDirection == Direction.Down) { Move(Vector2.down); }
            currentTime = tick;
        }
    }
    private void InputHandler()
    {
        // handling input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDirection = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = Direction.Down;
        }   
    }
    private void Update()
    {
        InputHandler();
        TickMovement();
    }
    private void Awake()
    {
        _transform = transform;
    }

    // do after map spawning
    private void Start()
    {
        map = FindObjectOfType<Map>();
        currentTile = map.tileGrid[0, 0];
    }
}

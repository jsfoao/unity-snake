using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.WSA;

// snake will depend on map to work
// snake will move dependent on tile grid
// snake wont have independent movement
// head of snake will control movement
public class SnakeController : MonoBehaviour, IEntityController
{
    [SerializeField] [Range(1, 100)] private int size;
    private Direction direction;
    private Map map;
    private Transform _transform;
    private Vector2 worldPosition;

    // Tick
    private float currentTime;
    [SerializeField] [Tooltip("Time in seconds to move to next tile")] 
    private float tick;

    [Header("Temp")] [SerializeField] 
    private GameObject bodyPrefab;
    
    private List<Body> bodyParts;

    private void AddBody()
    {
        GameObject bodyGameObject;
        Body body;
        if (bodyParts.Count == 0)
        {
            bodyGameObject = Instantiate(bodyPrefab, map.tileGrid[0, 0].worldPosition, Quaternion.identity, transform);
            bodyGameObject.name = "Body_0";
            body = bodyGameObject.GetComponent<Body>();
            body.previous = null;
            body.currentTile = map.tileGrid[0, 0];
            bodyParts.Add(body);
        }
        else
        {
            Body tail = bodyParts[bodyParts.Count - 1];
            bodyGameObject = Instantiate(bodyPrefab,
                tail.currentTile.neighbourTiles[(int) Direction.Down].worldPosition, 
                Quaternion.identity, transform);
            bodyGameObject.name = $"Body_{bodyParts.Count}";
            body = bodyGameObject.GetComponent<Body>();
            body.previous = tail;
            body.currentTile = tail.currentTile.neighbourTiles[(int) Direction.Down];
            bodyParts.Add(body);
        }
    }
    
    private void CalculateBody()
    {
        // Start traversing from tail
        Body body = bodyParts[bodyParts.Count - 1];

        while (body.previous != null)
        {
            body.MoveToTile(body.previous.currentTile);
            body = body.previous;
        }
    }

    private void SpawnSnake()
    {
        for (int i = 0; i < size; i++)
        {
            AddBody();
        }
    }

    private void TickMovement()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
        // Calculate body parts
        CalculateBody();

        // Move head of snake to tile set by input
        bodyParts[0].MoveToTile(bodyParts[0].currentTile.neighbourTiles[(int)direction]);

        currentTime = tick;
    }
    
    private void InputHandler()
    {
        // handling input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddBody();
        }
    }
    
    private void Update()
    {
        InputHandler();
        TickMovement();
    }

    // Do after map spawning
    private void Start()
    {
        map = FindObjectOfType<Map>();
        bodyParts = new List<Body>();
        direction = Direction.Up;
        SpawnSnake();
    }
}

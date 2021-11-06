using System.Collections.Generic;
using UnityEngine;

// snake will depend on map to work
// snake will move dependent on tile grid
// snake wont have independent movement
// head of snake will control movement
public class SnakeController : MonoBehaviour, IEntityController
{
    [SerializeField] private int size;
    private Direction direction;
    private Tile currentTile;
    private Map map;
    private Transform _transform;
    private Vector2 worldPosition;

    // Tick
    private float currentTime;
    [SerializeField] [Tooltip("Time in seconds to move to next tile")] 
    private float tick;

    [Header("Temp")] [SerializeField] 
    private GameObject bodyPrefab;
    private GameObject head;

    private LinkedList<GameObject> snakeBody;

    private void SpawnSnake()
    {
        int bodyCounter = 0;
        Tile currentTileToSpawn = currentTile.neighbourTiles[(int) Direction.Down];
        while (bodyCounter < size - 1)
        {
            GameObject bodyPart = Instantiate(bodyPrefab, currentTileToSpawn.worldPosition, Quaternion.identity);
            currentTileToSpawn = currentTileToSpawn.neighbourTiles[(int) Direction.Down];
            bodyCounter++;
        }
    }
    public void MoveToTile(Tile tile)
    {
        if (tile == null) { return; }
        _transform.position = tile.worldPosition;
        currentTile = tile;
    }

    private void TickMovement()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
        MoveToTile(currentTile.neighbourTiles[(int)direction]);
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
    }
    private void Update()
    {
        InputHandler();
        TickMovement();
    }
    private void Awake()
    {
        _transform = transform;
        direction = Direction.Up;
    }

    // Do after map spawning
    private void Start()
    {
        map = FindObjectOfType<Map>();
        currentTile = map.tileGrid[0, 0];
        SpawnSnake();
        head = transform.GetChild(0).gameObject;
    }
}

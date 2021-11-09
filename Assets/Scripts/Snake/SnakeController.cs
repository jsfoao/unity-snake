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
    private Direction direction;
    private Transform _transform;
    private Vector2 worldPosition;

    // Tick
    private float currentTime;
    [SerializeField] [Tooltip("Time in seconds to move to next tile")] 
    private float tick;
    
    private Snake snake;
    private Map map;
    private Spawner spawner;
    
    // todo lock movement against snake
    
    // Set linked body positions
    private void EvaluateBodyPositions()
    {
        // Start traversing from head
        var bodyNode = snake.bodyParts.Head;

        while (bodyNode.Next != null)
        {
            bodyNode.Next.Item.MoveToTile(bodyNode.Item.previousTile);
            bodyNode = bodyNode.Next;
        }
    }

    // Runs every tick seconds
    private void TickUpdate()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
        HandleMovement();
        
        currentTime = tick;
    }

    private void HandleMovement()
    {
        // Move head of snake to tile set by input
        Body headBody = snake.bodyParts.Head.Item;
        headBody.MoveToTile(headBody.currentTile.neighbourTiles[(int)direction]);

        // if tile has object
        if (headBody.currentTile.currentObject != null)
        {
            // Check if object is fruit
            if (headBody.currentTile.currentObject.GetComponent<Fruit>() != null)
            {
                Debug.Log("Destroy fruit");
                // destroy fruit and spawn new random one
                spawner.DestroyObject(headBody.currentTile.currentObject);
                spawner.SpawnRandomFruit();
                
                // add to snake size
                snake.AddBody();
            }
        }

        // Evaluate remaining body positions
        EvaluateBodyPositions();
    }
    
    private void InputHandler()
    {
        // handling input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction == Direction.Right) { return; }
            direction = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction == Direction.Left) { return; }
            direction = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction == Direction.Down) { return; }
            direction = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction == Direction.Up) { return; }
            direction = Direction.Down;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            snake.AddBody();
        }
    }
    
    private void Update()
    {
        InputHandler();
        TickUpdate();
    }

    // Do after map spawning
    private void Start()
    {
        snake = GetComponent<Snake>();
        map = FindObjectOfType<Map>();
        spawner = FindObjectOfType<Spawner>();
        direction = Direction.Up;
        
        // Spawn snake on (0, 0)
        snake.Create(map.tileGrid[5, 5]);
    }
}

using UnityEngine;
using Direction = UnityEngine.Direction;

// snake will depend on map to work
// snake will move dependent on tile grid
// snake wont have independent movement
// head of snake will control movement
[RequireComponent(typeof(Snake))]
public class SnakeController : MonoBehaviour, IEntityController
{
    private Direction direction;
    private Map map;
    private Transform _transform;
    private Vector2 worldPosition;

    // Tick
    private float currentTime;
    [SerializeField] [Tooltip("Time in seconds to move to next tile")] 
    private float tick;
    
    private Snake snake;

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

    private void TickMovement()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
        // Move head of snake to tile set by input
        snake.bodyParts.Head.Item.MoveToTile(snake.bodyParts.Head.Item.currentTile.neighbourTiles[(int)direction]);
        
        EvaluateBodyPositions();

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
            snake.AddBody(snake.bodyParts.Tail.Item.currentTile.neighbourTiles[(int)Direction.Down]);
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
        snake = GetComponent<Snake>();
        map = FindObjectOfType<Map>();
        direction = Direction.Up;
        
        // Spawn snake on (0, 0)
        snake.Create(map.tileGrid[5, 5]);
    }
}

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

    private List<BodyNode> bodyNodes;

    private void CalculateBody()
    {
        BodyNode node = bodyNodes[bodyNodes.Count - 1];

        while (node.previous != null)
        {
            MoveToTile(node, node.previous.currentTile);
            node = node.previous;
        }
    }
    
    private void SpawnSnake()
    {
        int bodyCounter = 1;
        Tile currentTileToSpawn = currentTile.neighbourTiles[(int) Direction.Down];

        BodyNode lastBodyNode = head.GetComponent<BodyNode>();
        lastBodyNode.currentTile = currentTile;
        
        while (bodyCounter < size)
        {
            GameObject bodyPart = Instantiate(bodyPrefab, currentTileToSpawn.worldPosition, Quaternion.identity, transform);
            bodyPart.name = $"Body_{bodyCounter}";
            BodyNode bodyNode = bodyPart.GetComponent<BodyNode>();
            
            bodyNodes.Add(bodyNode);
            
            bodyNode.previous = lastBodyNode;
            lastBodyNode = bodyNode;
            
            lastBodyNode.currentTile = currentTileToSpawn;
            
            currentTileToSpawn = currentTileToSpawn.neighbourTiles[(int) Direction.Down];
            bodyCounter++;
        }
    }
    
    public void MoveToTile(BodyNode node, Tile tile)
    {
        if (tile == null) { return; }
        node.transform.position = tile.worldPosition;
        node.currentTile = tile;
        currentTile = tile;
    }
    
    private void TickMovement()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        
        MoveToTile(head.GetComponent<BodyNode>(), currentTile.neighbourTiles[(int)direction]);
        CalculateBody();
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

    // Do after map spawning
    private void Start()
    {
        bodyNodes = new List<BodyNode>();
        map = FindObjectOfType<Map>();
        currentTile = map.tileGrid[0, 0];
        head = transform.GetChild(0).gameObject;
        _transform = head.transform;
        direction = Direction.Up;
        
        SpawnSnake();
    }
}

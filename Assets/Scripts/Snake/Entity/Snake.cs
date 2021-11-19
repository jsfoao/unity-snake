using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    // List of linked bodies
    public LList<Body> bodyParts;

    [Header("Properties")]
    [SerializeField] [Tooltip("Size of snake when spawned")] 
    private int initialSize;
    [NonSerialized] public Color color;

    [NonSerialized] public UnityEvent tickEvent;
    
    // Current size of snake
    [SerializeField] public int size;

    // Reference to prefab
    [Header("Temp")] 
    [SerializeField] private GameObject bodyPrefab;
    private Spawner _spawner;
    
    // Tick
    private float currentTime;
    [SerializeField] private float tick;
    
    public void Create(Tile tile, int createSize, Color snakeColor)
    {
        color = snakeColor;
        // Spawn head on tile
        Body headBody = AddBody();
        headBody.gridObject.currentTile = tile;
        
        // Spawn rest of body on adjacent neighbour tiles
        for (int i = 1; i < createSize; i++)
        {
            AddBody();
        }
    }

    // Add body part to snake
    public Body AddBody()
    {
        Body body;
        GameObject spawnedGameObject;
        if (bodyParts.Count == 0)
        {
            spawnedGameObject = Instantiate(bodyPrefab, transform);
            body = spawnedGameObject.GetComponent<Body>();
            body.previousTile = null;
            body.gridObject.currentTile = null;
            bodyParts.AddLast(body);
        }
        else
        {
            spawnedGameObject = Instantiate(bodyPrefab, bodyParts.Tail.Item.gridObject.currentTile.worldPosition, Quaternion.identity, transform);
            body = spawnedGameObject.GetComponent<Body>();
            body.previousTile = null;
            body.gridObject.currentTile = bodyParts.Tail.Item.gridObject.currentTile;
            
            bodyParts.AddLast(body);
        }

        spawnedGameObject.GetComponent<SpriteRenderer>().color = color;
        body.snake = this;
        body.linked = true;
        size++;
        return body;
    }

    // Remove bodies from tail until body of index (inclusive)
    public void RemoveBodiesUntil(int index)
    {
        for (int i = index; i < bodyParts.Count; i++)
        {
            bodyParts[i].GetComponent<SliceableBody>().Unlink();
            bodyParts[i].gridObject.currentTile.walkable = true;
            _spawner.spawnedObjects.Add(bodyParts[i].gridObject);
            size--;
        }
        bodyParts.RemoveTailUntil(index);
    }
    
    public void DestroySelf()
    {
        var bodyNode = bodyParts.Head;
        while (bodyNode.Next != null)
        {
            bodyNode.Item.gridObject.currentTile.currentObjects.Clear();
            bodyNode = bodyNode.Next;
        }
        Destroy(gameObject);
        _spawner.spawnedSnakes.Remove(this);
    }

    public void DeathBehaviour()
    {
        Debug.Log($"{this} snake is ded!");
        _spawner.spawnedSnakes.Remove(this);
        GetComponent<EntityController>().enableMovement = false;
        GetComponent<Collisions>().enableCollisions = false;

        AIFinder aiFinder = GetComponent<AIFinder>();
        if (aiFinder == true)
        {
            aiFinder.enableFinder = false;
        }
        RemoveBodiesUntil(0);
        // CheckWinner();
    }

    private void CheckWinner()
    {
        // Check if winner
        if (_spawner.spawnedSnakes.Count == 1)
        {
            Time.timeScale = 0f;
            Debug.Log($"{_spawner.spawnedSnakes[0]} is winner winner chicken dinner!");
        }
    }
    
    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        tickEvent.Invoke();
        currentTime = tick;
    }

    private void Awake()
    {
        bodyParts = new LList<Body>();
        _spawner = FindObjectOfType<Spawner>();
        
        // Tick event listeners
        tickEvent = new UnityEvent();
        tickEvent.AddListener(GetComponent<EntityController>().MovementTick);
        tickEvent.AddListener(GetComponent<Collisions>().CheckCollision);

    }
}

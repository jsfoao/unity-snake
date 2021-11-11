using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Snake : MonoBehaviour
{
    // List of linked bodies
    public LList<Body> bodyParts;
    
    // Size of snake when spawned
    [Header("Visuals")]
    private Color _headColor;

    [Header("Properties")]
    [SerializeField] [Tooltip("Size of snake when spawned")] 
    private int initialSize;

    [NonSerialized] public UnityEvent tickEvent;
    
    // Current size of snake
    private int size;

    // Reference to prefab
    [Header("Temp")] 
    [SerializeField] private GameObject bodyPrefab;
    private Spawner _spawner;
    
    // Tick
    private float currentTime;
    [SerializeField] private float tick;
    
    public Body AddBody()
    {
        Body body;
        GameObject spawnedGameObject;
        if (bodyParts.Count == 0)
        {
            spawnedGameObject = Instantiate(bodyPrefab, transform);
            body = spawnedGameObject.GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = null;
            bodyParts.AddLast(body);
        }
        else
        {
            spawnedGameObject = Instantiate(bodyPrefab, bodyParts.Tail.Item.transform.position, Quaternion.identity, transform);
            body = spawnedGameObject.GetComponent<Body>();
            body.previousTile = null;
            body.currentTile = bodyParts.Tail.Item.currentTile;
            
            bodyParts.AddLast(body);
        }
        
        // Setting body color
        body.GetComponent<SpriteRenderer>().color = _headColor;
        
        size++;
        return body;
    }
    
    public void Create(Tile tile, int createSize)
    {
        // Setting colors
        if (_spawner.possibleColors.Count == 0)
        {
            _headColor = Color.black;
        }
        else
        {
            int randomIndex = Random.Range(0, _spawner.possibleColors.Count);
            _headColor = _spawner.possibleColors[randomIndex];
            _spawner.possibleColors.Remove(_headColor);   
        }

        // Spawn head on tile
        Body headBody = AddBody();
        headBody.currentTile = tile; ;
        
        // Spawn rest of body on adjacent neighbour tiles
        for (int i = 1; i < createSize; i++)
        {
            AddBody();
        }
    }

    public void DestroySelf()
    {
        var bodyNode = bodyParts.Head;
        while (bodyNode.Next != null)
        {
            bodyNode.Item.currentTile.currentObjects.Clear();
            bodyNode = bodyNode.Next;
        }
        Destroy(gameObject);
        _spawner.spawnedSnakes.Remove(this);
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
        tickEvent.AddListener(GetComponent<SnakeCollisions>().CollisionTick);
        tickEvent.AddListener(GetComponent<EntityController>().MovementTick);
    }
}

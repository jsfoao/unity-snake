using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float tick = 5f;
    [SerializeField][Range(0, 1)][Tooltip("Chance of snake to spawn every tick")]
    private float chanceSnake;
    [SerializeField][Range(0, 1)][Tooltip("Chance of super fruit to spawn every tick")]
    private float chanceSuperFruit;
    
    [Header("Spawning")]
    [SerializeField][Tooltip("Spawn controlled snake")] 
    private bool spawnControlled;
    [SerializeField][Tooltip("Amount of snakes to spawn on start")]
    private int initialSnakes;
    [SerializeField][Tooltip("Amount of fruits to spawn on start")]
    private int initialFruits;
    
    private float currentTime;
    private Spawner _spawner;
    
    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        // Spawn snake
        if (RandomFloat() < chanceSnake)
        {
            _spawner.SpawnSnakeInRandomTile(_spawner.RandomColor());
        }
        // Spawn super fruit
        if (RandomFloat() < chanceSuperFruit)
        {
            _spawner.SpawnObjectOfTypeInRandomTile(PickupType.SuperFruit);
        }
        currentTime = tick;
    }

    private float RandomFloat()
    {
        float randomFloat = Random.Range(0, 1);
        return randomFloat;
    }

    private void Start()
    {
        // Spawn player snake
        if (spawnControlled)
        {
            _spawner.SpawnSnakeInRandomTile(Color.black, 1, true);
        }

        // Spawn initial AI snakes
        for (int i = 0; i < initialSnakes; i++)
        {
            _spawner.SpawnSnakeInRandomTile(_spawner.RandomColor());
        }
        
        // Spawn initial fruits
        for (int i = 0; i < initialFruits; i++)
        {
            _spawner.SpawnObjectOfTypeInRandomTile(PickupType.Fruit);
        }
    }

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
    }
}

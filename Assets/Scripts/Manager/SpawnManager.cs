using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float tick = 5f;
    [SerializeField][Range(0, 1)] private float chanceSnake;
    [SerializeField][Range(0, 1)] private float chanceSuperFruit;
    [SerializeField] [Tooltip("Spawn controlled snake")] 
    private bool spawnControlled;
    
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
        // Spawn fruit
        _spawner.SpawnObjectOfTypeInRandomTile(PickupType.Fruit);
    }

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
    }
}

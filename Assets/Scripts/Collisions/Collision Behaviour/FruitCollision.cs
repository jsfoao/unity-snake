using UnityEngine;

public class FruitCollision : CollisionBehaviour
{
    private Spawner _spawner;
    private GridObject _gridObject;

    public override void OnCollision(GridCollider gridCollider)
    {
        base.OnCollision(gridCollider);
        
        // Add to snake body
        Snake snake = gridCollider.GetComponent<Body>().snake;
        if (snake != null) { snake.AddBody(); }
        
        // Spawn new fruit
        _spawner.SpawnObjectOfTypeInRandomPosition(ObjectType.Fruit);
        
        // Destroy self
        _spawner.DestroyObject(_gridObject);
    }

    public override void Awake()
    {
        base.Awake();
        _gridObject = GetComponent<GridObject>();
        _spawner = FindObjectOfType<Spawner>();
    }
}

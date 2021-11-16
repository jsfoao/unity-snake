using UnityEngine;

public class FruitCollision : CollisionBehaviour
{
    public override void OnCollision(GridCollider otherCollider)
    {
        if (gridCollider.CollisionType == CollisionType.Passive) { return; }
        
        // Destroy object
        Spawner.DestroyObject(GetComponent<GridObject>());
        
        // Add to snake body
        Snake snake = otherCollider.GetComponent<Body>().snake;
        if (snake != null) { snake.AddBody(); }
        
        // Spawn new fruit
        Spawner.SpawnObjectOfTypeInRandomPosition(ObjectType.Fruit);
    }

    public override void Awake()
    {
        base.Awake();
    }
}

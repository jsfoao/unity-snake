using UnityEngine;

public class BodyCollision : CollisionBehaviour
{
    private Spawner _spawner;
    public override void OnCollision(GridCollider gridCollider)
    {
        Body body = GetComponent<Body>();
        if (body.objectType != ObjectType.Body)
        {
            DisconnectedCollision();
            return;
        }
        
        Snake snake = body.snake;
        // is head
        if (snake.bodyParts[0] == body)
        {
            Body otherBody = gridCollider.GetComponent<Body>();
            if (otherBody == null) { return; }
            
            // other collider is body type
            if (otherBody.objectType == ObjectType.Body)
            {
                snake.DeathBehaviour();
            }
        }
    }

    private void DisconnectedCollision()
    {
        _spawner.DestroyObject(GetComponent<GridObject>());
    }

    public override void Awake()
    {
        base.Awake();
        _spawner = FindObjectOfType<Spawner>();
    }
}

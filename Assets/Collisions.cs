using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] public bool enableCollisions;
    // Handling collisions
    private Body headBody;
    private Snake _snake;
    
    // Handling collision behaviour
    private Spawner _spawner;
    
    private void OnCollision(GridObject otherGridObject)
    {
        Debug.Log($"{headBody.snake} collided with {otherGridObject}");
        
        // Fruit collision
        if (otherGridObject.objectType == ObjectType.Fruit)
        {
            _spawner.DestroyObject(otherGridObject);
            _snake.AddBody();
            _spawner.SpawnObjectOfTypeInRandomPosition(otherGridObject.objectType);
            return;
        }

        // Body collision
        if (otherGridObject.objectType == ObjectType.Body)
        {
            Body otherBody = otherGridObject.GetComponent<Body>();
            
            // Collision with self
            // Linked and not head
            if (otherBody.linked && otherBody.snake == headBody.snake)
            {
                Debug.Log("Snake collision ded");
                _snake.DeathBehaviour();
                _spawner.DestroyObject(headBody.gridObject);
            }
            // Collision with unlinked body
            else if (!otherBody.linked)
            {
                _spawner.DestroyObject(otherGridObject);
                _snake.AddBody();
            }
            // Collision with another snake
            // Collision with body: 
            else if (otherBody.linked && otherBody != otherBody.snake.bodyParts[0])
            {
                int index = otherBody.snake.bodyParts.IndexOf(otherBody);
                otherBody.snake.CutTailUntil(index);
                _spawner.DestroyObject(otherGridObject);
                _snake.AddBody();
            }
            // Collision with other snake
            // Collision with head: kill if bigger, die if smaller
            else if (otherBody.linked && otherBody == otherBody.snake.bodyParts[0])
            {
                // If bigger snake: kill other snake
                if (_snake.size > otherBody.snake.size)
                {
                    if (otherBody.snake.bodyParts.Count == 1)
                    {
                        otherBody.snake.DestroySelf();
                        _snake.AddBody();
                        return;
                    }
                    _spawner.DestroyObject(otherBody.snake.bodyParts[0].gridObject);
                    otherBody.snake.DeathBehaviour();
                }
                // If smaller snake: dies
                else
                {
                    _spawner.DestroyObject(headBody.gridObject);
                    _snake.DeathBehaviour();
                }
            }
        }
    }
    
    public void CheckCollision()
    {
        if (!enableCollisions) { return; }
        
        headBody = _snake.bodyParts[0];
        if (headBody.gridObject.currentTile.currentObjects.Count > 1)
        {
            GridObject otherGridObject = FindOtherGridObject();

            // No collision
            if (otherGridObject == null) { return; }
            
            // Collision happened
            OnCollision(otherGridObject);
        }
    }

    private GridObject FindOtherGridObject()
    {
        foreach (GridObject gridObject in headBody.gridObject.currentTile.currentObjects)
        {
            if (gridObject != headBody.gridObject && gridObject.enableCollision)
            {
                return gridObject;
            }
        }
        return null;
    }

    private void Awake()
    {
        _snake = GetComponent<Snake>();
        _spawner = FindObjectOfType<Spawner>();
    }
}
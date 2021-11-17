using UnityEditor;
using UnityEngine;

public class Collisions : MonoBehaviour
{
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
        }

        // Body collision
        if (otherGridObject.objectType == ObjectType.Body)
        {
            Body otherBody = otherGridObject.GetComponent<Body>();
            
            // Other body is linked and not head
            if (otherBody.linked && otherBody != otherBody.snake.bodyParts[0])
            {
                
                // Cut tail if linked and is bigger than other snake
                if (_snake.size > otherBody.snake.size)
                {
                }
                // else
                // {
                //     _snake.DeathBehaviour();
                // }
            }
            
            // Other body is linked and is head
            else if (otherBody.linked && otherBody == otherBody.snake.bodyParts[0])
            {
                _snake.DeathBehaviour();
            }
            
            // Eat if not linked
            else if (!otherBody.linked)
            {
                _spawner.DestroyObject(otherGridObject);
                _snake.AddBody();   
            }
        }
    }
    
    public void CheckCollision()
    {
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
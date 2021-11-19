using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] public bool enableCollisions;
    
    // Handling collisions
    private Body headBody;
    private Snake _snake;
    private SnakeSlicing _snakeSlicing;
    
    // Handling collision behaviour
    private Spawner _spawner;
    
    private void OnCollision(GridObject otherGridObject)
    {
        // Loot collision
        if (otherGridObject.objectType == ObjectType.Loot)
        {
            otherGridObject.OnPickup(_snake);
            _spawner.DestroyObject(otherGridObject);
            return;
        }

        // Body collision
        if (otherGridObject.objectType == ObjectType.Body)
        {
            // Simple collision
            if (!_snakeSlicing.enabled || _snakeSlicing == null)
            {
                _snake.DestroySelf();
                return;
            }
            // Sliced collision
            Body otherBody = otherGridObject.GetComponent<Body>();
            _snakeSlicing.Slice(otherBody);
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
        _snakeSlicing = GetComponent<SnakeSlicing>();
        
        _spawner = FindObjectOfType<Spawner>();
    }
}
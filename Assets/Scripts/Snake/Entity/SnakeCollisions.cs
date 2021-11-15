using UnityEngine;

public class SnakeCollisions : MonoBehaviour
{
    private Spawner _spawner;
    private Body headBody;
    private Snake _snake;

    public void CollisionTick()
    {
        // Don't check collisions if no other objects to compare
        if (headBody.currentTile.currentObjects.Count == 1) { return; }

        foreach (GridObject gridObject in headBody.currentTile.currentObjects)
        {
            // Collision with body
            if (gridObject.objectType == ObjectType.Body && gridObject != headBody)
            {
                _snake.DestroySelf();
                
                // Check if winner
                if (_spawner.spawnedSnakes.Count == 1)
                {
                    Time.timeScale = 0f;
                    Debug.Log($"{_spawner.spawnedSnakes[0]} is winner winner chicken dinner!");
                }
                return;
            }
            
            // Collision with grid object
            if (gridObject.GetComponent<GridCollider>() != null)
            {
                gridObject.GetComponent<GridCollider>().OnCollision(this);
                return;
            }
        }
    }
    
    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _snake = GetComponent<Snake>();
        headBody = _snake.bodyParts.Head.Item;
    }
}

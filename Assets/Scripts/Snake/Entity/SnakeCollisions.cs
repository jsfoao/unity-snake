using UnityEngine;

public class SnakeCollisions : MonoBehaviour
{
    private Spawner _spawner;
    private Body headBody;
    private Snake _snake;

    public void CollisionTick()
    {
        // If only present object in tile is self
        if (headBody.currentTile.currentObjects.Count == 1) { return; }

        foreach (GridObject gridObject in headBody.currentTile.currentObjects)
        {
            // Collision with body
            if (gridObject.type == ObjectType.Body && gridObject != headBody)
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
            
            // Collision with fruit
            if (gridObject.type == ObjectType.Fruit)
            {
                _spawner.DestroyObject(gridObject);
                _spawner.SpawnRandomFruit();
                _snake.AddBody();
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

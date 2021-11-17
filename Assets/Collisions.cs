using UnityEditor;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // Handling collisions
    private Body headBody;
    private Snake _snake;
    
    // Handling collision behaviour
    private Spawner _spawner;

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

    private void OnCollision(GridObject otherGridObject)
    {
        Debug.Log($"{headBody.snake} collided with {otherGridObject}");
        if (otherGridObject.objectType == ObjectType.Fruit)
        {
            _spawner.DestroyObject(otherGridObject);
            _snake.AddBody();
            _spawner.SpawnObjectOfTypeInRandomPosition(otherGridObject.objectType);
        }

        if (otherGridObject.objectType == ObjectType.Body)
        {
            // Cut tail if linked
            if (otherGridObject.GetComponent<Body>().linked)
            {
                int index = otherGridObject.GetComponent<Body>().snake.bodyParts.IndexOf(otherGridObject.GetComponent<Body>());
                otherGridObject.GetComponent<Body>().snake.CutTailUntil(index);
                _spawner.DestroyObject(otherGridObject);
                _snake.AddBody();
            }
            // Eat if not linked
            else
            {
                _spawner.DestroyObject(otherGridObject);
                _snake.AddBody();
            }
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
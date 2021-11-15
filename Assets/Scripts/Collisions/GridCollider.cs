using UnityEngine;

public class GridCollider : MonoBehaviour
{
    private Spawner _spawner;
    private GridObject _gridObject;
    
    public void OnCollision(SnakeCollisions collisions)
    {
        _spawner.SpawnRandomObjectOfType(_gridObject.objectType);
        if (_gridObject.objectType == ObjectType.Fruit)
        {
            collisions.GetComponent<Snake>().AddBody();
        }
        _spawner.DestroyObject(_gridObject);
    }

    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _gridObject = GetComponent<GridObject>();
    }
}

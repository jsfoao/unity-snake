using UnityEngine;

[RequireComponent(typeof(CollisionBehaviour))]
public class GridCollider : MonoBehaviour
{
    private GridObject _gridObject;
    private CollisionBehaviour _collisionBehaviour;
    [Tooltip("Passive: Only for collision checking; Active: Has effects")]
    public CollisionType CollisionType;
    public void CollisionCheck()
    {
        if (_gridObject.currentTile.currentObjects == null) { return; }
        if (_gridObject.currentTile.currentObjects.Count > 1)
        {
            GridCollider otherCollider = FindCollider();
            if (otherCollider == null) { return; }
            
            // Collision happened
            Debug.Log($"{this} collided with {otherCollider}");
            _collisionBehaviour.OnCollision(otherCollider);
            otherCollider._collisionBehaviour.OnCollision(this);
        }
    }

    private GridCollider FindCollider()
    {
        foreach (GridObject go in _gridObject.currentTile.currentObjects)
        {
            GridCollider gridCollider = go.GetComponent<GridCollider>();
            if (go != _gridObject && gridCollider != null)
            {
                return gridCollider;
            }
        }
        return null;
    }
    
    private void Start()
    {
        _gridObject = GetComponent<GridObject>();
        _collisionBehaviour = GetComponent<CollisionBehaviour>();
    }
}

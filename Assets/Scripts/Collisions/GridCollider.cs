using UnityEngine;

[RequireComponent(typeof(CollisionBehaviour))]
public class GridCollider : MonoBehaviour
{
    public GridObject gridObject;
    public CollisionBehaviour collisionBehaviour;

    public void CollisionCheck()
    {
        if (gridObject.currentTile.currentObjects == null) { return; }
        if (gridObject.currentTile.currentObjects.Count > 1)
        {
            GridCollider otherCollider = FindCollider();
            if (otherCollider == null) { return; }
            collisionBehaviour.OnCollision(otherCollider);
            otherCollider.collisionBehaviour.OnCollision(this);
        }
    }

    private GridCollider FindCollider()
    {
        foreach (GridObject go in gridObject.currentTile.currentObjects)
        {
            if (go != gridObject && go.GetComponent<GridCollider>() == true)
            {
                GridCollider gridCollider = go.GetComponent<GridCollider>();
                return gridCollider;
            }
        }
        return null;
    }
    
    private void Start()
    {
        gridObject = GetComponent<GridObject>();
        collisionBehaviour = GetComponent<CollisionBehaviour>();
    }
}

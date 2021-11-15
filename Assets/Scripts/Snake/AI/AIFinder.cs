using UnityEngine;

public class AIFinder : MonoBehaviour
{
    private AIController aiController;
    private Spawner _spawner;
    private Pathfinding _pathfinding;

    // Closest object of type
    public GridObject ClosestObjectOfType(ObjectType objectType)
    {
        if (_spawner.spawnedObjects == null) { return null; }
        
        int index = 0;
        int flag = 0;
        int max = 99999;
        for (int i = 0; i < _spawner.spawnedObjects.Count; i++)
        {
            GridObject gridObject = _spawner.spawnedObjects[i];
            if (gridObject.objectType == objectType)
            {
                flag = 1;
                int distanceCost = _pathfinding.TileDistanceCost(gridObject.currentTile, aiController.headBody.currentTile);
                if (distanceCost < max)
                {
                    max = distanceCost;
                    index = i;
                }
            }
        }
        if (flag == 0) { return null; }
        Debug.DrawLine(aiController.headBody.transform.position, _spawner.spawnedObjects[index].currentTile.worldPosition, Color.green, .1f);
        return _spawner.spawnedObjects[index];
    }
    
    // Closest object out of all objects
    public GridObject ClosestObject()
    {
        if (_spawner.spawnedObjects == null) { return null; }
        
        int index = 0;
        int max = 99999;
        for (int i = 0; i < _spawner.spawnedObjects.Count; i++)
        {
            GridObject gridObject = _spawner.spawnedObjects[i];
            int distanceCost = _pathfinding.TileDistanceCost(gridObject.currentTile, aiController.headBody.currentTile);
            if (distanceCost < max)
            {
                max = distanceCost;
                index = i;
            }
        }
        Debug.DrawLine(aiController.headBody.transform.position, _spawner.spawnedObjects[index].currentTile.worldPosition, Color.green, .1f);
        return _spawner.spawnedObjects[index];
    }

    // Object with the lowest cost of distance + weight
    public GridObject LowestCostObject()
    {
        if (_spawner.spawnedObjects == null) { return null; }

        int index = 0;
        int max = 99999;
        for (int i = 0; i < _spawner.spawnedObjects.Count; i++)
        {
            GridObject gridObject = _spawner.spawnedObjects[i];
            
            int distanceCost = _pathfinding.TileDistanceCost(gridObject.currentTile, aiController.headBody.currentTile);
            int totalCost = distanceCost + gridObject.GetWeight();
            if (totalCost < max)
            {
                max = totalCost;
                index = i;
            }
        }
        return _spawner.spawnedObjects[index];
    }

    private void Start()
    {
        // Object components
        aiController = GetComponent<AIController>();
        _pathfinding = GetComponent<Pathfinding>();
        
        _spawner = FindObjectOfType<Spawner>();
    }
}

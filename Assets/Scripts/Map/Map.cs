using System;
using UnityEngine;
using Direction = UnityEngine.Direction;

public class Map : MonoBehaviour
{
    [Header("Spawning tiles")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] public Vector2Int size;
    [SerializeField] [Tooltip("Offset between each tile")] 
    private float offset;
    [NonSerialized] public Tile[,] tileGrid;
    
    // Spawn size.x * size.y tiles
    private void GenerateMap()
    {
        tileGrid = new Tile[size.x, size.y];
        Transform parent = transform.GetChild(0);
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                // Spawning tile
                Vector2 position = new Vector2(x, y) * offset;
                GameObject tileGameObject = Instantiate(tilePrefab, position, Quaternion.identity, parent);
                tileGameObject.name = $"Tile_{x}_{y}";
                
                // Setting grid values
                tileGrid[x, y] = tileGameObject.GetComponent<Tile>();
                tileGrid[x, y].gridPosition = new Vector2Int(x, y);
                tileGrid[x, y].worldPosition = tileGameObject.transform.position;
            }
        }
    }
    
    // Check if neighbour is valid (exists)
    private bool SetEdgeTile(Tile tile, Vector2Int positionToCheck)
    {
        // Right edge
        if (positionToCheck.x >= size.x)
        {
            tile.neighbourTiles[(int) Direction.Right] = tileGrid[0, positionToCheck.y];
            return true;
        }
        // Left edge
        if (positionToCheck.x < 0)
        {
            tile.neighbourTiles[(int) Direction.Left] = tileGrid[size.x - 1, positionToCheck.y];
            return true;
        }
        // Up edge
        if (positionToCheck.y >= size.y)
        {
            tile.neighbourTiles[(int) Direction.Up] = tileGrid[positionToCheck.x, 0];
            return true;
        }
        // Down edge
        if (positionToCheck.y < 0)
        {
            tile.neighbourTiles[(int) Direction.Down] = tileGrid[positionToCheck.x, size.y - 1];
            return true;
        }
        return false;
    }

    // Check all neighbours around singular tile
    private void NeighboursAroundTile(Tile tile)
    {
        // Defining positions around tile to check
        Vector2Int position = tile.gridPosition;
        
        // Four possible neighbour positions around tile
        Vector2Int[] positions = new[]
        {
            new Vector2Int(position.x - 1, position.y), // left
            new Vector2Int(position.x + 1, position.y), // right
            new Vector2Int(position.x, position.y - 1), // down
            new Vector2Int(position.x, position.y + 1)  // up
        };

        // Check validity of all positions
        for (int i = 0; i < positions.Length; i++)
        {
            if (!SetEdgeTile(tile, positions[i]))
            {
                tile.neighbourTiles[i] = tileGrid[positions[i].x, positions[i].y];
            }
        }
    }

    // Find neighbours of all tiles on the grid
    private void FindAllNeighbours()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                NeighboursAroundTile(tileGrid[x, y]);
            }
        }
    }

    private void Awake()
    {
        GenerateMap();
        FindAllNeighbours();
    }
}

using System;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    [Header("Spawning tiles")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2Int size;
    [SerializeField] private float offset;
    [NonSerialized] public Tile[,] tileGrid;
    
    private void GenerateMap()
    {
        tileGrid = new Tile[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                // Spawning tile
                Vector2 position = new Vector2(x, y) * offset;
                GameObject tileGameObject = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tileGameObject.name = $"Tile_{x}_{y}";
                
                // Setting grid values
                tileGrid[x, y] = tileGameObject.GetComponent<Tile>();
                tileGrid[x, y].gridPosition = new Vector2Int(x, y);
                tileGrid[x, y].worldPosition = tileGameObject.transform.position;
            }
        }
    }
    
    // Check if neighbour is valid (exists)
    private bool ValidNeighbour(Tile tile, Vector2Int positionToCheck)
    {
        if (positionToCheck.x >= size.x || positionToCheck.x < 0) { return false; }
        if (positionToCheck.y >= size.y || positionToCheck.y < 0) { return false; }
        return true;
    }

    // Check all neighbours around singular tile
    private void NeighboursAroundTile(Tile tile)
    {
        // Defining positions around tile to check
        Vector2Int position = tile.gridPosition;
        Vector2Int positionLeft = new Vector2Int(position.x - 1, position.y);
        Vector2Int positionRight = new Vector2Int(position.x + 1, position.y);
        Vector2Int positionDown = new Vector2Int(position.x, position.y - 1);
        Vector2Int positionUp = new Vector2Int(position.x, position.y + 1);
        
        // left neighbour
        if (ValidNeighbour(tile, positionLeft))
        {
            tile.neighbourTileLeft = tileGrid[positionLeft.x, positionLeft.y];
        }
        // right neighbour
        if (ValidNeighbour(tile, positionRight))
        {
            tile.neighbourTileRight = tileGrid[positionRight.x, positionRight.y];
        }
        // down neighbour
        if (ValidNeighbour(tile, positionDown))
        {
            tile.neighbourTileDown = tileGrid[positionDown.x, positionDown.y];
        }
        // up neighbour
        if (ValidNeighbour(tile, positionUp))
        {
            tile.neighbourTileUp = tileGrid[positionUp.x, positionUp.y];
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public bool walkable;
    [SerializeField] public Vector2Int gridPosition;
    [SerializeField] public Vector2 worldPosition;
    [SerializeField] public Tile neighbourTileLeft;
    [SerializeField] public Tile neighbourTileRight;
    [SerializeField] public Tile neighbourTileDown;
    [SerializeField] public Tile neighbourTileUp;
}

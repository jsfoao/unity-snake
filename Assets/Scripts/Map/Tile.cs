using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Grid
    [SerializeField] public Vector2Int gridPosition;
    [SerializeField] public Vector2 worldPosition;
    public Tile[] neighbourTiles = new Tile[4];
    
    // Objects
    [SerializeField] public GameObject currentObject;
}

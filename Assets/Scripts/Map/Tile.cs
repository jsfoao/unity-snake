using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Grid
    [NonSerialized] public Vector2Int gridPosition;
    [NonSerialized] public Vector2 worldPosition;
    [NonSerialized] public Tile[] neighbourTiles = new Tile[4];
    
    // Objects
    [SerializeField] public List<GameObject> currentObjects;
}

using System;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [NonSerialized] public Tile currentTile;
    [SerializeField] public ObjectType objectType;
}
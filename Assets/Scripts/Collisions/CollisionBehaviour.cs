using System;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
    [NonSerialized] public Spawner Spawner;
    [NonSerialized] public GridObject GridObject;
    public GridCollider gridCollider;
    public virtual void OnCollision(GridCollider otherCollider)
    {
    }

    public virtual void Awake()
    {
        Spawner = FindObjectOfType<Spawner>();
        GridObject = GetComponent<GridObject>();
        gridCollider = GetComponent<GridCollider>();
    }
}

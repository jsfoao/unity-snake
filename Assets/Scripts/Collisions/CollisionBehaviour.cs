using System;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
    public virtual void OnCollision(GridCollider gridCollider)
    {
        // Do collision behaviour
    }
    public virtual void Awake()
    {
    }
}

using System;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [NonSerialized] public Tile currentTile;
    [SerializeField] public ObjectType objectType;
    [SerializeField] public PickupType pickupType;
    [Tooltip("Higher tier means bigger weight")]
    [SerializeField] [Range(1, 5)] public int tier;
    [SerializeField] public bool enableCollision;

    public int GetWeight()
    {
        switch (tier)
        {
            case 1:
                return 5 * 2;
            case 2:
                return 4 * 2;
            case 3:
                return 3 * 2;
            case 4:
                return 2 * 2;
            case 5:
                return 1 * 2;
        }
        return 0;
    }

    public virtual void OnPickup(Snake snake)
    {
        // Pickup behaviour
    }
}
using UnityEngine;

public class SuperFruit : GridObject
{
    [Header("SuperFruit Properties")]
    [SerializeField] private float speedPercentage;
    public override void OnPickup(Snake snake)
    {
        snake.tick *= speedPercentage;
    }
}

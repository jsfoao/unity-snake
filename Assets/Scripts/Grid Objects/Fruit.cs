using UnityEngine;

public class Fruit : GridObject
{
    private Spawner _spawner;
    public override void OnPickup(Snake snake)
    {
        snake.AddBody();
        _spawner.SpawnObjectOfTypeInRandomTile(pickupType);
    }

    private void Awake()
    {
        objectType = ObjectType.Loot;
        _spawner = FindObjectOfType<Spawner>();
    }
}

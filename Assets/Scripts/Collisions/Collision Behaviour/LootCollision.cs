public class LootCollision : CollisionBehaviour
{
    private Spawner _spawner;
    private GridObject _gridObject;
    public override void OnCollision(GridCollider otherCollider)
    {
        base.OnCollision(otherCollider);
        _spawner.DestroyObject(_gridObject);
    }

    public override void Awake()
    {
        base.Awake();
    }
}

public class LootCollision : CollisionBehaviour
{
    private Spawner _spawner;
    private GridObject _gridObject;
    public override void OnCollision(GridCollider gridCollider)
    {
        base.OnCollision(gridCollider);
        _spawner.DestroyObject(_gridObject);
    }

    public override void Awake()
    {
        base.Awake();
        _gridObject = GetComponent<GridObject>();
        _spawner = FindObjectOfType<Spawner>();
    }
}

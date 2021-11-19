namespace UnityEngine
{
    public enum Direction { Left, Right, Down, Up, None }
    public enum ObjectType { None, Body, Loot }

    public enum PickupType
    {
        None,
        Body,
        Fruit,
        SuperFruit
    }
    public enum CollisionType { Passive, Active }
    public enum AIState { Wander, Focus }
}

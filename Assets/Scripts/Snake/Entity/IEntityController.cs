using UnityEngine;

public interface IEntityController
{
    void EvaluateBodyPositions();
    void MovementTick();
    void ChangeDirection(Direction direction);
}

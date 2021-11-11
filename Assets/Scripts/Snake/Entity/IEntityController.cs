using UnityEngine;

public interface IEntityController
{
    void HandleMovement();
    void EvaluateBodyPositions();
    void HandleCollisions();
    void TickUpdate();
    void ChangeDirection(Direction direction);
}

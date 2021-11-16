using UnityEngine;
using Direction = UnityEngine.Direction;

public class PlayerController : EntityController
{
    // Check for input
    private void HandleInput()
    {
        if (!enableInput) { return; }
        
        // handling input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeDirection(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeDirection(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeDirection(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Snake>().CutTailUntil(1);
        }
    }

    public void Update()
    {
        HandleInput();
    }
}

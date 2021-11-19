using UnityEngine;

public class SnakeSlicing : MonoBehaviour
{
    [SerializeField] public bool enableSlicing;
    private Snake _snake;
    private Spawner _spawner;

    public void Slice(Body otherBody)
    {
        Body headBody = _snake.bodyParts[0];
        if (otherBody.linked && otherBody.snake == headBody.snake)
        {
            Debug.Log("Self collision");
            _snake.DeathBehaviour();
            _spawner.DestroyObject(headBody.gridObject);
        }
        // Collision with unlinked body
        else if (!otherBody.linked)
        {
            _spawner.DestroyObject(otherBody.gridObject);
            _snake.AddBody();
        }
        // Collision with another snake
        // Collision with body: 
        else if (otherBody.linked && otherBody != otherBody.snake.bodyParts[0])
        {
            int index = otherBody.snake.bodyParts.IndexOf(otherBody);
            otherBody.snake.RemoveBodiesUntil(index);
            _spawner.DestroyObject(otherBody.gridObject);
            _snake.AddBody();
        }
        // Collision with other snake
        // Collision with head: kill if bigger, die if smaller
        else if (otherBody.linked && otherBody == otherBody.snake.bodyParts[0])
        {
            // If bigger snake: kill other snake
            if (_snake.size > otherBody.snake.size)
            {
                if (otherBody.snake.bodyParts.Count == 1)
                {
                    otherBody.snake.DeathBehaviour();
                    _spawner.DestroyObject(otherBody.gridObject);
                    _snake.AddBody();
                    return;
                }

                _spawner.DestroyObject(otherBody.snake.bodyParts[0].gridObject);
                otherBody.snake.DeathBehaviour();
            }
            // If smaller snake: dies
            else
            {
                _spawner.DestroyObject(headBody.gridObject);
                _snake.DeathBehaviour();
            }
        }
    }

    private void Awake()
    {
        _snake = GetComponent<Snake>();
        _spawner = FindObjectOfType<Spawner>();
    }
}

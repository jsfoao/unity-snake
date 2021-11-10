using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject instance;
    public HighScore highScore;
    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        
        highScore = new HighScore();
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject instance;
    
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
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Charge MainMenu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
    }
}

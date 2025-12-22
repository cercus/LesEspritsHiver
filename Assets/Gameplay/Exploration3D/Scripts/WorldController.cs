using UnityEngine;

public class WorldController : MonoBehaviour
{

    public WorldData worldData;
    [SerializeField] private Transform playerSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Bienvenue dans " + worldData.worldName);
        ApplyWorldData();
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("PlayerManager introuvable !");
            return;
        }

        PlayerManager.Instance.SpawnPlayer(playerSpawnPoint);
        Debug.Log("Spawn at: " + playerSpawnPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyWorldData()
    {
        
    }
}

using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        if(playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance);
        }
        Debug.LogError("position : " + spawnPoint.position);
        playerInstance.transform.position = spawnPoint.position;
        playerInstance.transform.rotation = spawnPoint.rotation;
    }

    public GameObject GetPlayer()
    {
        return playerInstance;
    }

}

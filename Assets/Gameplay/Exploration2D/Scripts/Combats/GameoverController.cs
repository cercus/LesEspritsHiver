using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        if(BattleContext.Victory == null)
        {
            return;
        }
            
        if (BattleContext.Victory == false)
        {
            Debug.Log("defaite");
            ShowGameOver();
            BattleContext.Victory = null; // reset
        } else if(BattleContext.Victory == true)
        {
            MapProgression.Instance.MarkNodeCompleted(BattleContext.CurrentNode);
            BattleContext.CurrentNode = null;
            BattleContext.Enemies = null;
            SceneManager.LoadScene("World01");
        }
    }

    void ShowGameOver()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

}
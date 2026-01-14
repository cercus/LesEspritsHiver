using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text titleText;

    void Awake()
    {
        Hide();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Option animation texte
        titleText.text = "Vous avez perdus...";
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnRetryClicked()
    {
        BattleManager.Instance.RetryBattle();
    }

    public void OnReturnToMapClicked()
    {
        BattleManager.Instance.ReturnToMap();
    }
}
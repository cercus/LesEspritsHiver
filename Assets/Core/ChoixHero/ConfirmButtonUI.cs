using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmButtonUI : MonoBehaviour
{
    [SerializeField] private CharacterSelectUI characterSelectUI;
    [SerializeField] private PlayerDatabase playerDatabase;

    public void OnClick()
    {
        PlayerProfile.Instance.SwitchHero(characterSelectUI.selectedHero.Id, playerDatabase);
        SceneManager.LoadScene("World01");
    }
}
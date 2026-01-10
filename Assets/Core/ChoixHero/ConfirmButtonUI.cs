using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmButtonUI : MonoBehaviour
{
    [SerializeField] private CharacterSelectUI characterSelectUI;

    public void OnClick()
    {
        HeroState state = new()
        {
            experience = characterSelectUI.selectedHero.Experience,
            level = characterSelectUI.selectedHero.Niveau,
            id = characterSelectUI.selectedHero.Id,
            name = characterSelectUI.selectedHero.Name,
            currentHealth = characterSelectUI.selectedHero.Health,
            maxHealth = characterSelectUI.selectedHero.Health
        };
        SaveSystem.Instance.Data.hero = state;
        SaveSystem.Instance.Save();
        Debug.Log("Hero sauvegardé");
    }
}
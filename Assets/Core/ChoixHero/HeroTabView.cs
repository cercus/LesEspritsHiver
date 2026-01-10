using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroTabView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image portrait;
    [SerializeField] private TMP_Text name;

    private HeroData heroData;

    public void Setup(HeroData data, System.Action<HeroData> onClick)
    {
        heroData = data;
        portrait.sprite = data.Image;
        name.text = data.Name;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke(heroData));
    }
}
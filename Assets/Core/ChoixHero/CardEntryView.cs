using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardEntryView : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;

    private CardData cardData;

    public void Setup(CardData data)
    {
        cardData = data;

        cardImage.sprite = data.Image;
        nameText.text = data.name;
        costText.text = data.mana.ToString();
    }
}
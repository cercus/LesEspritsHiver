using UnityEngine;
using UnityEngine.UI;

public class CardIconView : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Setup(CardData card)
    {
        icon.sprite = card.Image;
    }
}
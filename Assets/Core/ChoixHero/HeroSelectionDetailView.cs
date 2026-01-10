using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HeroSelectionDetailView : MonoBehaviour
{
    [SerializeField] private Image heroImage;
    [SerializeField] private TMP_Text heroName;
    [SerializeField] private TMP_Text heroDescription;

    [SerializeField] private Transform deckGrid;
    [SerializeField] private CardEntryView cardPrefab;

    public void Show(HeroData hero)
    {
        heroImage.sprite = hero.Image;
        heroName.text = hero.Name;
        heroDescription.text = "Description du héros...";

        foreach (Transform child in deckGrid)
            Destroy(child.gameObject);

        foreach (var card in hero.Deck)
        {
            CardEntryView entry = Instantiate(cardPrefab, deckGrid);
            entry.Setup(card);
        }
    }
}

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
    [SerializeField] private CardDatabase cardDatabase;

    public void Show(HeroData hero)
    {
        heroImage.sprite = hero.Image;
        heroName.text = hero.Name;
        heroDescription.text = "Description du héros...";

        foreach (Transform child in deckGrid)
            Destroy(child.gameObject);

        foreach (string card in SaveSystem.Instance.Data.heroes[hero.Id].deckCardIds)
        {
            CardEntryView entry = Instantiate(cardPrefab, deckGrid);
            entry.Setup(cardDatabase.Get(card));
        }
    }
}

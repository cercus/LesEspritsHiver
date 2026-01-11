using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterSelectUI : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private PlayerDatabase heroDatabase;
    [SerializeField] private CardDatabase cardDatabase;

    [Header("Left Panel")]
    [SerializeField] private Transform tabsParent;
    [SerializeField] private HeroTabView tabPrefab;

    [Header("Right Panel")]
    [SerializeField] private Image heroPortrait;
    [SerializeField] private TMP_Text heroName;
    [SerializeField] private TMP_Text heroDescription;
    [SerializeField] private Transform deckParent;
    [SerializeField] private CardEntryView cardEntryPrefab;

    public HeroData selectedHero;

    void Start()
    {
        BuildTabs();
        SelectHero(heroDatabase.allPlayers[0]); // auto select
    }

    void BuildTabs()
    {
        foreach (HeroData hero in heroDatabase.allPlayers)
        {
            HeroTabView tab = Instantiate(tabPrefab, tabsParent);
            tab.Setup(hero, SelectHero);
        }
    }

    void SelectHero(HeroData hero)
    {
        selectedHero = hero;

        heroPortrait.sprite = hero.Image;
        heroName.text = hero.Name;
        heroDescription.text = hero.Description;

        RefreshDeck(SaveSystem.Instance.Data.heroes[hero.Id].deckCardIds);
    }

    void RefreshDeck(List<string> deck)
    {
        foreach (Transform child in deckParent)
            Destroy(child.gameObject);

        foreach (string card in deck)
        {
            CardEntryView icon = Instantiate(cardEntryPrefab, deckParent);
            
            icon.Setup(cardDatabase.Get(card));
        }
    }


}
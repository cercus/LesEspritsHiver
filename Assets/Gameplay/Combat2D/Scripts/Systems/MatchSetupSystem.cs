using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    
    [SerializeField] private HeroData heroData;
    void Start()
    {

        SetupHero();
        SetupEnemies();
        SetupCardAndMana();
        
    }

    private void SetupCardAndMana()
    {
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.Perform(refillManaGA, () =>
        {
            DrawCardGA drawCardsGA = new(5);
            ActionSystem.Instance.Perform(drawCardsGA);
        });
    }

    private void SetupEnemies()
    {
        List<EnemyData> enemies = BattleContext.Enemies;

        if (enemies == null || enemies.Count == 0)
        {
            Debug.LogError("MatchSetupSystem : aucun ennemi reçu !");
            return;
        }

        EnemySystem.Instance.Setup(enemies);
    }

    private void SetupHero()
    {
        HeroSystem.Instance.Setup(heroData);
        CardSystem.Instance.Setup(heroData.Deck);
    }

}

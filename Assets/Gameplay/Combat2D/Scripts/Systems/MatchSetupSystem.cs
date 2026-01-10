using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    
    
    [SerializeField] private Camera battleCamera;

    [SerializeField] private ManaUI manaUI;

    [SerializeField] private GameObject damageVFX;

    [SerializeField] private LayerMask targetLayerMask;

    [SerializeField] private EnemyBoardView enemyBoardView;

    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    [SerializeField] private CardView cardViewHover;

    [SerializeField] private PlayerDatabase heroDatabase;
    [SerializeField] private HeroView heroView;

    

    void Start()
    {
        MouseUtil.BindCamera(battleCamera);
        ManaSystem.Instance.BindScene(manaUI);
        DamageSystem.Instance.BindScene(damageVFX);
        ManualTargetSystem.Instance.BindScene(targetLayerMask);
        EnemySystem.Instance.BindScene(enemyBoardView);
        CardSystem.Instance.BindScene(handView, drawPilePoint, discardPilePoint);
        CardViewHoverSystem.Instance.BindScene(cardViewHover);
        HeroSystem.Instance.BindScene(heroView);
        

        SetupHero();
        SetupEnemies();
        SetupCardAndMana();
        
    }

/*
    void OnDestroy()
    {
        ManaSystem.Instance.UnbindScene();
    }
*/
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
        HeroData heroData = heroDatabase.Get(SaveSystem.Instance.Data.hero.id);
        HeroSystem.Instance.Setup(heroData);
        CardSystem.Instance.Setup(heroData.Deck);
    }

}

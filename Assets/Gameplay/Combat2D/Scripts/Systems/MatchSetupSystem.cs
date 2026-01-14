using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchSetupSystem : MonoBehaviour
{
    
    
    [SerializeField] private Camera battleCamera;

    [SerializeField] private ManaUI manaUI;

    [SerializeField] private GameObject damageVFX;
    [SerializeField] private GameObject killVFX;

    [SerializeField] private LayerMask targetLayerMask;

    [SerializeField] private EnemyBoardView enemyBoardView;

    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    [SerializeField] private CardView cardViewHover;

    [SerializeField] private PlayerDatabase heroDatabase;
    [SerializeField] private HeroView heroView;

    [SerializeField] private CardDatabase cardDatabase;

    [SerializeField] private Button button;

    [SerializeField] private GameOverUI gameOverUI;

    

    void Start()
    {
        BattleContext.Victory = null;
        MouseUtil.BindCamera(battleCamera);
        ManaSystem.Instance.BindScene(manaUI);
        DamageSystem.Instance.BindScene(damageVFX, killVFX);
        ManualTargetSystem.Instance.BindScene(targetLayerMask);
        EnemySystem.Instance.BindScene(enemyBoardView);
        CardSystem.Instance.BindScene(handView, drawPilePoint, discardPilePoint, cardDatabase);
        CardViewHoverSystem.Instance.BindScene(cardViewHover);
        HeroSystem.Instance.BindScene(heroView, heroDatabase);
        

        SetupHero();
        SetupEnemies();
        SetupCardAndMana();
        gameOverUI.Hide();
        
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
        HeroState hero = PlayerProfile.Instance.CurrentHero;
        HeroData heroData = PlayerProfile.Instance.CurrentHeroData;
        HeroSystem.Instance.Setup(PlayerProfile.Instance);
        CardSystem.Instance.Setup(PlayerProfile.Instance);
    }

}

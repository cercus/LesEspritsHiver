using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.Perform(refillManaGA, () =>
        {
            DrawCardGA drawCardsGA = new(5);
            ActionSystem.Instance.Perform(drawCardsGA);
        });
    }

}

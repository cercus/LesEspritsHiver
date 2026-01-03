using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{

    [SerializeField] private EnemyBoardView enemyBoardView;
    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;


    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);       
        //ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);

    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        //ActionSystem.DetachPerformer<AttackHeroGA>(); 
        ActionSystem.DetachPerformer<KillEnemyGA>();     
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (EnemyData enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (EnemyView enemyView in enemyBoardView.EnemyViews)
        {
            enemyView.ExecuteCurrentAction();
            enemyView.Brain.DecideNext();
            yield return new WaitForSeconds(0.2f);
            //AttackHeroGA attackHeroGA = new(enemyView);
            //ActionSystem.Instance.AddRection(attackHeroGA);
            //enemyView.DecideNextIntention();
        }
        yield return null;
    }

/*
    private IEnumerator AttackHeroPerformer(AttackHeroGA attackHeroGA)
    {
        EnemyView attacker = attackHeroGA.Attacker;
        Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x-1f, 0.15f);
        yield return tween.WaitForCompletion();
        attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
        DealDamageGA dealDamageGA = new(attacker.AttackPower,new(){HeroSystem.Instance.HeroView});
        ActionSystem.Instance.AddRection(dealDamageGA);
    }
*/
    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        SaveSystem.Instance.Data.bestiary.Discover(killEnemyGA.EnemyView.EnemyData.Id);
        SaveSystem.Instance.Data.bestiary.RegisterKill(killEnemyGA.EnemyView.EnemyData.Id);
        SaveSystem.Instance.Save();

        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
        
        if (enemyBoardView.EnemyViews.Count == 0)
        {
            yield return new WaitForSeconds(1.5f);
            BattleManager.Instance.EndBattle(true);
        }
    }
}

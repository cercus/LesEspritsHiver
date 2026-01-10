using System.Collections;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    private GameObject damageVFX;
    //private bool isBound;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // 🔗 Binding de la scène
    public void BindScene(GameObject damageVFX)
    {
        this.damageVFX = damageVFX;
        //isBound = true;

        
    }

    // 🧹 Optionnel mais recommandé
    public void UnbindScene()
    {
        damageVFX = null;
        //isBound = false;
    }

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }


    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach (CombatantView target in dealDamageGA.Targets)
        {
           
            target.Damage(dealDamageGA.Amount);
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
            
            // Cas où le heros n'a plus de vie
            if(target is HeroView heroView && target.CurrentHealth <= 0)
            {
                BattleManager.Instance.EndBattle(false);
            }
            
            if(target.CurrentHealth <= 0)
            {
                if(target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddRection(killEnemyGA);
                }
            }
            
        }
    }
}

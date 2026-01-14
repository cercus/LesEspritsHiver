using System.Collections;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    private GameObject damageVFX;
    private GameObject killVFX;
    //private bool isBound;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // 🔗 Binding de la scène
    public void BindScene(GameObject damageVFX, GameObject killVFX)
    {
        this.damageVFX = damageVFX;
        this.killVFX = killVFX;
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
            if(target == null)
                yield break;
            Debug.Log("Damage="+ dealDamageGA.Amount);
            target.Damage(dealDamageGA.Amount);
            if(damageVFX != null && target != null)
                Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
            
            // Cas où le heros n'a plus de vie
            if(target is HeroView heroView && heroView.CurrentHealth <= 0)
            {
                if(damageVFX != null && heroView != null)
                    Instantiate(killVFX, heroView.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                BattleManager.Instance.EndBattle(false);
                yield break;
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

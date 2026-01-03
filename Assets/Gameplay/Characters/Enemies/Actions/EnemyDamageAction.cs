using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Action/Attack")]
public class EnemyAttackAction : EnemyActionData
{
    public int damage = 5;

    public override void Execute(EnemyView enemy)
    {
        ActionSystem.Instance.AddRection(
            new DealDamageGA(damage, new() { HeroSystem.Instance.HeroView })
        );
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Conditions/HP Below %")]
public class EnemyHpBelowCondition : EnemyCondition
{
    [Range(0f, 1f)]
    public float threshold = 0.5f;

    public override bool IsMet(EnemyView enemy)
    {
        return enemy.CurrentHealth <= enemy.MaxHealth * threshold;
    }
}
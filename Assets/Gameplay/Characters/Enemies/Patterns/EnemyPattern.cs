using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Pattern")]
public class EnemyPattern : ScriptableObject
{
    public EnemyIntentionData Intention;
    public EnemyActionData Action;
    public List<EnemyCondition> Conditions;

    public bool IsValid(EnemyView enemy)
    {
        foreach (var condition in Conditions)
        {
            if (!condition.IsMet(enemy))
                return false;
        }
        return true;
    }
}
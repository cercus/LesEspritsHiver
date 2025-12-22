using UnityEngine;

public abstract class EnemyCondition : ScriptableObject
{
    public abstract bool IsMet(EnemyView enemy);
}
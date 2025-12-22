using UnityEngine;

public abstract class EnemyActionData : ScriptableObject
{
    public abstract void Execute(EnemyView enemy);
    //public abstract GameAction CreateGameAction(EnemyView enemy);
}

using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Combat Definition")]
public class CombatDefinition : ScriptableObject
{
    public EnemyGroup fixedGroup;     // ex : Boss, Event, Elite
    public EnemyPool randomPool;      // ex : forêt, donjon

    public int minEnemies = 1;
    public int maxEnemies = 5;

    public bool allowDuplicates = true;
}

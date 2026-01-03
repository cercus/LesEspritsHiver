using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyData> allEnemies;
}

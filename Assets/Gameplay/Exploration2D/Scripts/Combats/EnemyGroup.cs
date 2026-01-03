using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Enemy Group")]
public class EnemyGroup : ScriptableObject
{
    public List<EnemyData> enemies;
}
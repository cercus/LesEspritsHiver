using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
     [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Health { get; private set; }

    [field: SerializeField] public int AttackPower { get; private set; }
    
    [field: SerializeField] public int DefensePower { get; private set; }

    public EnemyBehaviorType behaviorType;

    // Ennemis normaux
    public List<EnemyPattern> randomPatterns;

    // Boss
    public EnemyPatternSequence patternSequence;
}

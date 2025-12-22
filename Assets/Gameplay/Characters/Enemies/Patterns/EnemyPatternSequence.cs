using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Pattern Sequence")]
public class EnemyPatternSequence : ScriptableObject
{
    public List<EnemyPattern> Patterns;
    public bool Loop = true;
}
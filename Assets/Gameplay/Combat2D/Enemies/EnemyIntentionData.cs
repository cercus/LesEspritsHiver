using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Enemy Intention")]
public class EnemyIntentionData : ScriptableObject
{
    public EnemyIntentionType type;
    public Sprite icon;
}
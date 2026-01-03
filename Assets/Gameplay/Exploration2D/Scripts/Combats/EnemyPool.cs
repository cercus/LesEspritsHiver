using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Enemy Pool")]
public class EnemyPool : ScriptableObject
{
    public List<EnemyPoolEntry> entries;

    public List<EnemyData> DrawEnemies(int min, int max)
    {
        int targetCount = Random.Range(min, max + 1);
        List<EnemyData> result = new();

        List<EnemyPoolEntry> shuffled = new(entries);
        shuffled.Shuffle();

        foreach (var entry in shuffled)
        {
            if (result.Count >= targetCount)
                break;

            int maxForThis = Mathf.Min(entry.maxCount, targetCount - result.Count);
            int count = Random.Range(0, maxForThis + 1);

            for (int i = 0; i < count; i++)
                result.Add(entry.enemy);
        }

        // sécurité
        if (result.Count < min && entries.Count > 0)
            result.Add(entries[0].enemy);

        return result;
    }
}
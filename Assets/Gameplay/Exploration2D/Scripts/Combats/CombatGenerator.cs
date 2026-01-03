using System.Collections.Generic;
using UnityEngine;
public static class CombatGenerator
{
    public static List<EnemyData> GenerateEnemies(CombatDefinition def)
    {
        List<EnemyData> enemies = new();

        // 1️⃣ Groupe fixe
        if (def.fixedGroup != null)
        {
            enemies.AddRange(def.fixedGroup.enemies);
        }

        // 2️⃣ Pool aléatoire
        if (def.randomPool != null)
        {
            int remainingMin = Mathf.Max(0, def.minEnemies - enemies.Count);
            int remainingMax = Mathf.Max(0, def.maxEnemies - enemies.Count);

            var randoms = def.randomPool.DrawEnemies(remainingMin, remainingMax);
            enemies.AddRange(randoms);
        }

        return enemies;
    }
}
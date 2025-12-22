using UnityEngine;

public class EnemyBrain
{
    private EnemyView enemy;
    private EnemyData data;
    private int index;
    private EnemyPattern current;

    public EnemyBrain(EnemyView enemy, EnemyData data)
    {
        this.enemy = enemy;
        this.data = data;
        index = 0;
    }

    public void DecideNext()
    {
        if (data.behaviorType == EnemyBehaviorType.Random)
            PickRandom();
        else
            PickFromSequence();
            

        enemy.SetPattern(current);
    }

    private void PickRandom()
    {
        var valid = data.randomPatterns.FindAll(p => p.IsValid(enemy));
        current = valid[Random.Range(0, valid.Count)];
    }

    private void PickFromSequence()
    {
        var seq = data.patternSequence.Patterns;

        for (int i = 0; i < seq.Count; i++)
        {
            var p = seq[index];
            index = (index + 1) % seq.Count;

            if (p.IsValid(enemy))
            {
                current = p;
                return;
            }
        }
    }

    public void ExecuteCurrent()
    {
        current.Action.Execute(enemy);
    }
}
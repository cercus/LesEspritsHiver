using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyView : CombatantView
{

    [Header("Intentions disponibles")]
    [SerializeField] private EnemyIntentionData attackIntention;
    [SerializeField] private EnemyIntentionData buffIntention;
    [SerializeField] private EnemyIntentionData defendIntention;

    [Header("View")]
    [SerializeField] private SpriteRenderer intentionRenderer;

    public EnemyBrain Brain { get; private set; }

    public EnemyPattern CurrentPattern { get; private set; }

    public EnemyData EnemyData { get; private set; }


    public void Setup(EnemyData enemyData)
    {
        Debug.Log("EnemyView ok");
        EnemyData = enemyData;
        SetupBase(enemyData.Health, enemyData.Image, enemyData.name);
        Brain = new EnemyBrain(this, enemyData);
        Brain.DecideNext();
        
    }

    public void SetPattern(EnemyPattern pattern)
    {
        CurrentPattern = pattern;
        intentionRenderer.sprite = pattern.Intention.icon;
    }

    public void ExecuteCurrentAction()
    {
        CurrentPattern.Action.Execute(this);
    }

}

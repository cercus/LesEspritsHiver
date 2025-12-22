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

    public EnemyIntentionData CurrentIntention { get; private set; }
    public int AttackPower { get; private set; }

    public void Setup(EnemyData enemyData)
    {
        DecideNextIntention();
        SetupBase(enemyData.Health, enemyData.Image, enemyData.name);
        
    }

    public void DecideNextIntention()
    {
        // Exemple simple (aléatoire)
        int roll = Random.Range(0, 100);

        if(roll < 25)
        {
            SetIntention(attackIntention, 1);
            
        } else if(roll < 50)
        {
            SetIntention(buffIntention, 1);
        } else
        {
            SetIntention(defendIntention, 1);
        }
    }

    private void SetIntention(EnemyIntentionData intention, int attackPower)
    {
        CurrentIntention = intention;
        AttackPower = attackPower;
        intentionRenderer.sprite = intention.icon;
    }

}

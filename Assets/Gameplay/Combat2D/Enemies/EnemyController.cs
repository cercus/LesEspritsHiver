using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Intentions disponibles")]
    [SerializeField] private EnemyIntentionData attackIntention;
    [SerializeField] private EnemyIntentionData buffIntention;
    [SerializeField] private EnemyIntentionData defendIntention;

    [Header("View")]
    [SerializeField] private SpriteRenderer intentionRenderer;

    public EnemyIntentionData CurrentIntention { get; private set; }
    public int AttackPower { get; private set; }

    public void DecideNextIntention()
    {
        // Exemple simple (aléatoire)
        int roll = Random.Range(0, 3);

        switch (roll)
        {
            case 0:
                SetIntention(attackIntention, 10);
                break;
            case 1:
                SetIntention(buffIntention, 10);
                break;
            case 2:
                SetIntention(defendIntention, 10);
                break;
        }
    }

    private void SetIntention(EnemyIntentionData intention, int attackPower)
    {
        CurrentIntention = intention;
        AttackPower = attackPower;
        intentionRenderer.sprite = intention.icon;
    }
}
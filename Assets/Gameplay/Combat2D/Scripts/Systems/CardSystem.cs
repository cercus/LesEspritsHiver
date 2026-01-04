using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    private HandView handView;
    private Transform drawPilePoint;
    private Transform discardPilePoint;
    private List<Card> drawPile;
    private List<Card> discardPile;
    private List<Card> hand;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // 🔗 Binding de la scène
    public void BindScene(HandView handView, Transform drawPilePoint, Transform discardPilePoint)
    {
        this.handView = handView;
        this.drawPilePoint = drawPilePoint;
        this.discardPilePoint = discardPilePoint;
        drawPile = new();
        discardPile = new();
        hand = new();

    }

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Setup(List<CardData> deckData)
    {

        foreach(CardData cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
    }


    private IEnumerator DrawCardsPerformer(DrawCardGA drawCardGA)
    {
        int actualAmount = Mathf.Min(drawCardGA.Amount, drawPile.Count);
        int notDrawnAmount = drawCardGA.Amount - actualAmount;
        for(int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }
        if(notDrawnAmount > 0)
        {
            RefillDeck();
            for(int i = 0; i < notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }

    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }

    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discradAllCardsGA)
    {
        foreach(Card card in hand)
        {
            //discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        CardView cardView = handView.RemoveCard(playCardGA.Card);
        yield return DiscardCard(cardView);
        SpendManaGA spendMana = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddRection(spendMana);

        if(playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectGA performEffectGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget});
            ActionSystem.Instance.AddRection(performEffectGA);
        }


        foreach (AutoTargetEffect effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            ActionSystem.Instance.AddRection(performEffectGA);
        }

    }


    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddRection(discardAllCardsGA);
    }

     private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddRection(drawCardGA);
    }


    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }

}

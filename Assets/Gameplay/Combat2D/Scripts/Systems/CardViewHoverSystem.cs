using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    private CardView cardViewHover;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // 🔗 Binding de la scène
    public void BindScene(CardView cardViewHover)
    {
        this.cardViewHover = cardViewHover;

    }

    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position;
    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }
}

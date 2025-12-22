
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    public string Title => data.Title;
    public string Description => data.Description;

    public Sprite Image => data.Image;

    public Effect ManualTargetEffect => data.ManualTargetEffect;

    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;

    private readonly CardData data;
    public int Mana { get; private set; }

    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.mana;
    }

}

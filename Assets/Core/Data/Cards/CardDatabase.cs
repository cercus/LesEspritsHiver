using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> allCards;

    public CardData Get(string id)
    {
        return allCards.Find(h => h.Id == id);
    }
    
}
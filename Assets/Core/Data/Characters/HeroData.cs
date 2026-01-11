using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Hero")]
public class HeroData : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; } = 20;
    [field: SerializeField] public int BaseHealth { get; private set; }
    [field: SerializeField] public string Description {get; private set; }
    [field: SerializeField] public List<CardData> StartingDeck { get; private set; }
    
}

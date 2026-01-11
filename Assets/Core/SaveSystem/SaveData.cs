using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string selectedHeroId;
    public BestiarySaveData bestiary = new();
    public Dictionary<string, HeroState> heroes = new();
    public HashSet<string> unlockedCards = new();
    public MapSaveData mapProgression = new();
}

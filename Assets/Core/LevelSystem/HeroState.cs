using System.Collections.Generic;

[System.Serializable]
public class HeroState
{
    public string id;
    public int level;
    public int experience;
    public int maxHealth;
    public int currentHealth;
    public string name;
    public List<string>  deckCardIds = new();
}
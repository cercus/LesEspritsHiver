using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    public HeroView HeroView { get; private set; }

    public HeroState State { get; private set; }
    
    public void BindScene(HeroView heroView)
    {
        HeroView = heroView;
    }

    public void UnbindScene()
    {
        HeroView = null;
    }
    
    public void Setup(HeroData heroData)
    {
        if (HeroView == null)
        {
            Debug.LogError("HeroSystem not bound to scene");
            return;
        }
        
        State = SaveSystem.Instance.Data.hero;
        State.currentHealth = State.maxHealth;
        
        HeroView.Setup(State.maxHealth, heroData.Image, heroData.name);
    }

    public void GainXP(int amount)
    {
        State.experience += amount;

        while (State.experience >= XPTable.GetXPForLevel(State.level + 1))
        {
            State.level++;
            RecalculateStats();
        }

        SaveSystem.Instance.Save();
    }

    private void RecalculateStats()
    {
        State.maxHealth = XPTable.CalculateHealth(State.level);
        State.currentHealth = State.maxHealth;
    }
}

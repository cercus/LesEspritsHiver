using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    public HeroView HeroView { get; private set; }

    public HeroState State { get; private set; }

    public HeroData HeroData { get; private set; }
    public PlayerDatabase PlayerDatabase { get; private set;}
    
    public void BindScene(HeroView heroView, PlayerDatabase playerDatabase)
    {
        HeroView = heroView;
        PlayerDatabase = playerDatabase;
    }

    public void UnbindScene()
    {
        HeroView = null;
    }

    public void Setup(PlayerProfile profile)
    {
        HeroData = PlayerDatabase.Get(profile.CurrentHero.id);

        State = new HeroState
        {
            level = profile.CurrentHero.level,
            experience = profile.CurrentHero.experience,
            maxHealth = XPTable.CalculateHealth(profile.CurrentHero.maxHealth),
            currentHealth = XPTable.CalculateHealth(profile.CurrentHero.currentHealth),
            name = profile.CurrentHeroData.Name
        };
        HeroView.Setup(State.maxHealth, profile.CurrentHeroData.Image, profile.CurrentHeroData.name);
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

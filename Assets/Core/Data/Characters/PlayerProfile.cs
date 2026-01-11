using System.Collections.Generic;

public class PlayerProfile : Singleton<PlayerProfile>
{
    public HeroState CurrentHero { get; private set; }
    public HeroData CurrentHeroData { get; private set; }

    public Dictionary<string, HeroState> Heroes { get; private set; }
    public HashSet<string> UnlockedCards { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void Load(PlayerDatabase db)
    {
        SaveData save = SaveSystem.Instance.Data;

        CurrentHero = save.heroes[save.selectedHeroId];
        CurrentHeroData = db.Get(CurrentHero.id);
    }

    public void SwitchHero(string heroId, PlayerDatabase db)
    {
        SaveSystem.Instance.Data.selectedHeroId = heroId;

        CurrentHero = SaveSystem.Instance.Data.heroes[heroId];
        CurrentHeroData = db.Get(heroId);

        SaveSystem.Instance.Save();
    }
}

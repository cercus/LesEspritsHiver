using UnityEngine;

public class HeroView : CombatantView
{
    private void Awake()
    {
        HeroSystem.Instance.BindScene(this);
    }

    private void OnDestroy()
    {
        if (HeroSystem.Instance)
            HeroSystem.Instance.UnbindScene();
    }
    public void Setup(HeroData heroData)
    {
        SetupBase(heroData.Health, heroData.Image, heroData.name);
    }
}

using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    public HeroView HeroView { get; private set; }
    
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
        HeroView.Setup(heroData);
    }
}

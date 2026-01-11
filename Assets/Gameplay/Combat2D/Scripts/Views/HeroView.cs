using UnityEngine;

public class HeroView : CombatantView
{
    [SerializeField] private PlayerDatabase playerDatabase;
    private void Awake()
    {
        HeroSystem.Instance.BindScene(this, playerDatabase);
    }

    private void OnDestroy()
    {
        if (HeroSystem.Instance)
            HeroSystem.Instance.UnbindScene();
    }
    public void Setup(int health, Sprite image, string name)
    {
        SetupBase(health, image, name);
    }
}

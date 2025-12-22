using DG.Tweening;
using Microlight.MicroBar;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] private MicroBar healthBar;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text nameTmp;

    public int MaxHealth{ get; private set; }
    public int CurrentHealth{ get; private set; }

    protected void SetupBase(int health, Sprite image, string name)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        nameTmp.text = name;
        healthBar.Initialize(health);
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthBar.UpdateBar(CurrentHealth);
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealth();
    }

}

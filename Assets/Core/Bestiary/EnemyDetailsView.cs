using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyDetailsView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text healthValue;
    [SerializeField] private TMP_Text ATKValue;
    [SerializeField] private TMP_Text nbKilledValue;
    [SerializeField] private TMP_Text levelValue;
    [SerializeField] private TMP_Text experienceValue;
    [SerializeField] private Sprite unknown;

    public void Show(EnemyData data, BestiaryEntrySave save)
    {
        if (save == null || !save.discovered)
        {
            healthValue.text = "???";
            ATKValue.text = "???";
            nbKilledValue.text = "???";
            levelValue.text = "???";
            experienceValue.text = "???";
            image.color = Color.black;
            image.sprite = unknown;
            return;
        }
        
        image.color = Color.white;
        image.sprite = data.Image;
       

        healthValue.text = data.Health.ToString();
        ATKValue.text = data.AttackPower.ToString();
        nbKilledValue.text = save.killCount.ToString();
        levelValue.text = data.Niveau.ToString();
        experienceValue.text = data.Experience.ToString();
    }
}
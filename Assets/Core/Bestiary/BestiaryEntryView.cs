using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BestiaryEntryView : MonoBehaviour
{
    [SerializeField] private TMP_Text identifiant;
    [SerializeField] private TMP_Text name;
    [SerializeField] private Button button;

    private EnemyData enemy;
    private bool discovered;

    public void Setup(EnemyData data, bool discovered, System.Action<EnemyData> onClick)
    {
        enemy = data;
        this.discovered = discovered;
        identifiant.text = data.Id;
        name.text = discovered ? data.Name : "???";
        //name.text = data.Name;

        button.onClick.AddListener(() => onClick(enemy));
    }
}
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
public class BestiaryUI : MonoBehaviour
{
    [SerializeField] private EnemyDatabase database;
    [SerializeField] private BestiaryEntryView entryPrefab;
    [SerializeField] private Transform listParent;
    [SerializeField] private EnemyDetailsView detailsView;
    [SerializeField] private TMP_Text title;

    void Start()
    {
        Populate();
    }

    void Populate()
    {
        int nbDiscovered = 0;
        foreach (var enemy in database.allEnemies)
        {
            var save = SaveSystem.Instance.Data.bestiary.Get(enemy.Id);
            bool discovered = save != null && save.discovered;
            if (discovered)
            {
                nbDiscovered += 1;
            }
            var entry = Instantiate(entryPrefab, listParent);
            entry.Setup(enemy, discovered, OnEntryClicked);
        }
        double percentage = 100 * nbDiscovered * 1f / database.allEnemies.Count;
        title.text = "Bestiaire complété à "+percentage+"%";
    }

    void OnEntryClicked(EnemyData enemy)
    {
        var save = SaveSystem.Instance.Data.bestiary.Get(enemy.Id);
        detailsView.Show(enemy, save);
    }
}
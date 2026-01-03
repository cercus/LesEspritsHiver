using Unity.Android.Gradle.Manifest;
using UnityEngine;
public class BestiaryUI : MonoBehaviour
{
    [SerializeField] private EnemyDatabase database;
    [SerializeField] private BestiaryEntryView entryPrefab;
    [SerializeField] private Transform listParent;
    [SerializeField] private EnemyDetailsView detailsView;

    void Start()
    {
        Populate();
    }

    void Populate()
    {
        foreach (var enemy in database.allEnemies)
        {
            var save = SaveSystem.Instance.Data.bestiary.Get(enemy.Id);
            bool discovered = save != null && save.discovered;
            var entry = Instantiate(entryPrefab, listParent);
            entry.Setup(enemy, discovered, OnEntryClicked);
        }
    }

    void OnEntryClicked(EnemyData enemy)
    {
        var save = SaveSystem.Instance.Data.bestiary.Get(enemy.Id);
        detailsView.Show(enemy, save);
    }
}
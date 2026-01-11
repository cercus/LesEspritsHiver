using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SaveSystem : Singleton<SaveSystem>
{
    private string path;
    public  SaveData Data { get; private set; }
    [SerializeField] private PlayerDatabase playerDatabase;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/save.dat";
        Load();

        if (Data == null || Data.heroes.Count == 0)
            CreateNewSave();
    }

    public void Save()
    {
        
        string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (!File.Exists(path))
        {
            Data = new SaveData();
            //Save();
            return;
        }
        string json = File.ReadAllText(path);
        Data = JsonConvert.DeserializeObject<SaveData>(json);

    }

    public void CreateNewSave()
    {
        SaveData data = new();

        foreach (HeroData hero in playerDatabase.allPlayers)
        {
            List<string> deckIds = new();
            foreach (CardData card in hero.StartingDeck)
            {
                deckIds.Add(card.Id);
            }
            HeroState state = new()
            {
                id = hero.Id,
                level = 0,
                experience = 0,
                maxHealth = hero.BaseHealth,
                currentHealth = hero.BaseHealth,
                name = hero.Name,
                deckCardIds = deckIds
            };

            data.heroes.Add(state.id, state);
        }

        data.selectedHeroId = playerDatabase.allPlayers[0].Id;

        Data = data;
        Save();
    }
}
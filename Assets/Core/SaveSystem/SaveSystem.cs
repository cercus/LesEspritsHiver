using UnityEngine;
using System.IO;
public class SaveSystem : Singleton<SaveSystem>
{
    private string path;
    public  SaveData Data { get; private set; }

    void OnEnable()
    {
        path = Application.persistentDataPath + "/save.dat";
        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (!File.Exists(path))
        {
            Data = new SaveData();
            Save();
            return;
        }
        string json = File.ReadAllText(path);
        Data = JsonUtility.FromJson<SaveData>(json);
    }
}
using System;
using System.Collections.Generic;

public class MapProgression : Singleton<MapProgression>
{
    private HashSet<string> completedNodes = new();
    private HashSet<string> unlockedNodes = new();

    private MapSaveData Save => SaveSystem.Instance.Data.mapProgression;

    protected void Start()
    {
        LoadFromSave();
    }

    public bool IsEmpty()
{
    return SaveSystem.Instance.Data.mapProgression.completedNodes.Count == 0;
}

    private string Key(MapNode node)=> $"{node.worldName}:{node.nodeId}";

    private void LoadFromSave()
    {
        completedNodes = new HashSet<string>(Save.completedNodes);
        unlockedNodes = new HashSet<string>(Save.unlockedNodes);
    }

    public void MarkNodeCompleted(MapNode node)
    {
        string key = Key(node);

        completedNodes.Add(key);
        Save.completedNodes.Add(key);

        foreach (var next in node.nextNodes)
        {
            string nextKey = Key(next);
            unlockedNodes.Add(nextKey);
            Save.unlockedNodes.Add(nextKey);
        }
        SaveSystem.Instance.Save();
    }

    public bool IsCompleted(MapNode node)
        => completedNodes.Contains(Key(node));

    public bool IsUnlocked(MapNode node)
        => unlockedNodes.Contains(Key(node));
}
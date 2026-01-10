using System.Collections.Generic;

[System.Serializable]
public class MapSaveData
{
    public List<string> completedNodes = new();
    public List<string> unlockedNodes = new();
}
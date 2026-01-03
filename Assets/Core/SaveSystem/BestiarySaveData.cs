using System;
using System.Collections.Generic;

[Serializable]
public class BestiaryEntrySave
{
    public string enemyID;
    public bool discovered;
    public int killCount;
}

[Serializable]
public class BestiarySaveData
{
    public List<BestiaryEntrySave> entries = new();

    public BestiaryEntrySave Get(string id)
    {
        return entries.Find(e => e.enemyID == id);
    }

    public void Discover(string id)
    {
        if (Get(id) == null)
            entries.Add(new BestiaryEntrySave { enemyID = id, discovered = true });
    }

    public void RegisterKill(string id)
    {
        var e = Get(id);
        if (e == null)
        {
            e = new BestiaryEntrySave { enemyID = id, discovered = true };
            entries.Add(e);
        }
        e.killCount++;
    }
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/PlayerDatabase")]
public class PlayerDatabase : ScriptableObject
{
    public List<HeroData> allPlayers;

    public HeroData Get(string id)
    {
        return allPlayers.Find(h => h.Id == id);
    }
}

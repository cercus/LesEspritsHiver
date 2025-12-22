using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WorldData", menuName = "World/WorldData")]
public class WorldData : ScriptableObject
{
    public string worldId;
    public string worldName;
    public List<string> encouters;
}

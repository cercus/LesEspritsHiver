using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public enum NodeType
{
    Home,
    Combat,
    Rest,
    Event,
    Shop,
    Boss
}

[CreateAssetMenu(menuName = "Map/Node")]
public class MapNode : ScriptableObject
{
    public string nameNode;
    public string nodeId;
    public NodeType type;
    public string worldName;

    public CombatDefinition combat;

    public List<MapNode> nextNodes; // nodes accessibles après
}

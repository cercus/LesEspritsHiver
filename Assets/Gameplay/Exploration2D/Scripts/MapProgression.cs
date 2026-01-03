using System.Collections.Generic;

public class MapProgression : Singleton<MapProgression>
{
    private HashSet<MapNode> completedNodes = new();
    private HashSet<MapNode> unlockedNodes = new();

    public void MarkNodeCompleted(MapNode node)
    {
        completedNodes.Add(node);

        foreach (var next in node.nextNodes)
        {
            unlockedNodes.Add(next);
        }
    }

    public bool IsCompleted(MapNode node)
        => completedNodes.Contains(node);

    public bool IsUnlocked(MapNode node)
        => unlockedNodes.Contains(node);
}
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<NodeView> nodeViews;
    [SerializeField] private List<RailView> railViews;
    [SerializeField] private NodeView startNode;

    private MapNode currentNode;
    private Dictionary<MapNode, NodeState> nodeStates = new();

    void Start()
    {
        InitMap();
        //RefreshNodes();
    }

    void InitMap()
    {

        // Tous les nodes commencent LOCKED
        foreach (var view in nodeViews)
        {
            nodeStates[view.Node] = NodeState.Locked;
        }
        MapProgression.Instance.MarkNodeCompleted(startNode.Node);
        currentNode = startNode.Node;
        nodeStates[currentNode] = NodeState.Completed;
        // 2. Débloque automatiquement les nodes suivants
        UnlockNextNodes(currentNode);

        RefreshNodes();
        RefreshRails();
    }

    void RefreshNodes()
    {
        foreach (var view in nodeViews)
        {
            if (MapProgression.Instance.IsCompleted(view.Node))
                view.Setup(NodeState.Completed);
            else if (MapProgression.Instance.IsUnlocked(view.Node))
                view.Setup(NodeState.Available);
            else
                view.Setup(NodeState.Locked);
        }
    }

    public void SelectNode(MapNode node)
    {
        if (nodeStates[node] != NodeState.Available)
            return;

        currentNode = node;

        nodeStates[node] = NodeState.Completed;

        UnlockNextNodes(node);

        LaunchNode(node);

        RefreshNodes();
        RefreshRails();
    }


    void UnlockNextNodes(MapNode node)
    {
        foreach (var next in node.nextNodes)
        {
            if (nodeStates[next] == NodeState.Locked)
            {
                nodeStates[next] = NodeState.Available;
            }
            
        }
    }

    void RefreshRails()
    {
        foreach (var rail in railViews)
        {
            bool visible = MapProgression.Instance.IsCompleted(rail.From);

            rail.SetVisible(visible);
        }
            /*
            bool fromVisible =
                nodeStates[rail.From] != NodeState.Locked;

            bool toVisible =
                nodeStates[rail.To] != NodeState.Locked;

            rail.SetVisible(fromVisible && toVisible);
            */
        
    }
    void LaunchNode(MapNode node)
    {

        foreach (var next in node.nextNodes)
        {
        }
        
    }
}
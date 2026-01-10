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
        InitMapFromSave();
        RefreshNodes();
        RefreshRails();
    }

    void InitMapFromSave()
    {
        nodeStates.Clear();

        // 1️⃣ Initialiser tous les nodes en LOCKED
        foreach (var view in nodeViews)
        {
            nodeStates[view.Node] = NodeState.Locked;
        }

        // 2️⃣ Cas spécial : première partie → node de départ
        if (MapProgression.Instance.IsEmpty())
        {
            MapProgression.Instance.MarkNodeCompleted(startNode.Node);
        }

        // 3️⃣ Appliquer la progression sauvegardée
        foreach (var view in nodeViews)
        {
            if (MapProgression.Instance.IsCompleted(view.Node))
            {
                nodeStates[view.Node] = NodeState.Completed;
            }
            else if (MapProgression.Instance.IsUnlocked(view.Node))
            {
                nodeStates[view.Node] = NodeState.Available;
            }
        }
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
        if (!MapProgression.Instance.IsUnlocked(node))
            return;

        currentNode = node;
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
        
    }
}
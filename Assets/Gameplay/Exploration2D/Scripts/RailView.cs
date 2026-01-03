using UnityEngine;
using UnityEngine.Tilemaps;

public class RailView : MonoBehaviour
{
    [SerializeField] private MapNode from;
    [SerializeField] private MapNode to;
    [SerializeField] private Tilemap tilemap;

    public MapNode From => from;
    public MapNode To => to;

    public void SetVisible(bool visible)
    {
        tilemap.gameObject.SetActive(visible);
    }
}
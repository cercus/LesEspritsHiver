using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MapNode node;
    public MapNode Node => node;

    [Header("Images")]
    [SerializeField] private Sprite lockedImage;
    [SerializeField] private Sprite availableImage;
    [SerializeField] private Sprite completedImage;

    public void Setup(NodeState state)
    {
        button.onClick.RemoveAllListeners();
        switch (state)
        {
            case NodeState.Locked:
                //button.interactable = false;
                button.gameObject.SetActive(false);
                if(button.image != null)
                    button.image.sprite = lockedImage;
                break;

            case NodeState.Available:
                button.gameObject.SetActive(true);
                //button.interactable = true;
                if(button.image != null)
                {
                    button.image.sprite = availableImage;
                }
                    
                
                break;

            case NodeState.Completed:
                button.gameObject.SetActive(true);
                //button.interactable = true;

                if(button.image != null)
                {
                    button.image.sprite = completedImage;
                }
                    //image.color = completedColor;
                break;
        }
        updateStateButton(state);
       
    }

    void updateStateButton(NodeState state)
    {
        if(state != NodeState.Locked)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        BattleManager.Instance.StartBattle(node);
        //MapManager.Instance.SelectNode(Node);
    }
}
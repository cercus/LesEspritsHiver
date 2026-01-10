using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void StartBattle(MapNode node)
    {
        BattleContext.CurrentNode = node;
        BattleContext.Enemies = CombatGenerator.GenerateEnemies(node.combat);

        
        SceneManager.LoadScene("Battle");
    }

    public void EndBattle(bool victory)
    {
        if (victory && BattleContext.CurrentNode != null)
        {
            MapProgression.Instance.MarkNodeCompleted(BattleContext.CurrentNode);
        }
        else if(!victory && BattleContext.CurrentNode != null)
        {
            Debug.Log("Mort du heros");
        }
        BattleContext.CurrentNode = null;
        BattleContext.Enemies = null;
        SceneManager.LoadScene("World01");
    }
}
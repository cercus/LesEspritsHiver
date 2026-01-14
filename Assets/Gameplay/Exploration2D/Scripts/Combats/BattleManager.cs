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
        
        BattleContext.Victory = victory;
        Debug.Log("victory="+BattleContext.Victory);
        StopAllCoroutines();
        CardSystem.Instance.UnbindScene();
        DamageSystem.Instance.UnbindScene();
        /*
        if (victory && BattleContext.CurrentNode != null)
        {
            MapProgression.Instance.MarkNodeCompleted(BattleContext.CurrentNode);
        }
        else if(!victory && BattleContext.CurrentNode != null)
        {
            Debug.Log("Mort du heros");
            gameOverUI.Show();
        }
        BattleContext.CurrentNode = null;
        BattleContext.Enemies = null;
        SceneManager.LoadScene("World01");
        */
    }

    public void RetryBattle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        BattleContext.Victory = null;
    }

    public void ReturnToMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("World01");
        BattleContext.Victory = null;
    } 
}
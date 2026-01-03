using System.Collections;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    private ManaUI manaUI;
    private const int MAX_MANA = 3;
    private int currentMana = MAX_MANA;
    private bool isBound;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // 🔗 Binding de la scène
    public void BindScene(ManaUI manaUI)
    {
        this.manaUI = manaUI;
        isBound = true;

        // synchro immédiate
        manaUI.UpdateManaText(currentMana);
    }

    // 🧹 Optionnel mais recommandé
    public void UnbindScene()
    {
        manaUI = null;
        isBound = false;
    }

    public void ResetMana()
    {
        currentMana = MAX_MANA;
        UpdateUI();
    }

    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
            ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);

    }

    public bool HasEnoughMana(int mana)
    {
        return currentMana >= mana;
    }

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.Amount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        currentMana = MAX_MANA;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.AddRection(refillManaGA);
    }

    private void UpdateUI()
    {
        if (!isBound)
        {
            Debug.LogWarning("ManaSystem utilisé sans ManaUI bindée");
            return;
        }

        manaUI.UpdateManaText(currentMana);
    }
}
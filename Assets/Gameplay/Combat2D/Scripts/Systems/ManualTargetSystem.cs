using UnityEngine;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    private LayerMask targetLayerMask;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // 🔗 Binding de la scène
    public void BindScene(LayerMask targetLayerMask)
    {
        this.targetLayerMask = targetLayerMask;

    }

    public EnemyView EndTargeting(Vector3 endPosition)
    {
        Debug.Log("targetLayerMask="+targetLayerMask);
        Vector3 rayStart = endPosition + Vector3.back * 5f;

        if (Physics.Raycast(rayStart, Vector3.forward, out RaycastHit hit, 20f, targetLayerMask))
        {
            return hit.transform.GetComponentInParent<EnemyView>();
        }

        return null;
    }
}

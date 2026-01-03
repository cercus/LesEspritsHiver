using UnityEngine;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private LayerMask targetLayerMask;

    public EnemyView EndTargeting(Vector3 endPosition)
    {
        Vector3 rayStart = endPosition + Vector3.back * 5f;

        if (Physics.Raycast(rayStart, Vector3.forward, out RaycastHit hit, 20f, targetLayerMask))
        {
            return hit.transform.GetComponentInParent<EnemyView>();
        }

        return null;
    }
}

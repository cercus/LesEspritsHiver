using UnityEngine;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private LayerMask targetLayerMask;

    public EnemyView EndTargeting(Vector3 endPosition)
    {
        if(Physics.Raycast(endPosition, Vector3.forward, out RaycastHit hit, 10f, targetLayerMask) 
        && hit.collider != null && hit.transform.TryGetComponent(out EnemyView enemyView))
        {
            return enemyView;
        }
        return null;
    }
}

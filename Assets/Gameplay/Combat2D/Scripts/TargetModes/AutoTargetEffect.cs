using UnityEngine;
using SerializeReferenceEditor;
[System.Serializable]
public class AutoTargetEffect
{
    [field: SerializeReference, SR, SerializeField] public TargetMode TargetMode { get; private set; }
    [field: SerializeReference, SR, SerializeField] public Effect Effect { get; private set; }
}

public enum NodeState
{
    Locked,     // Non débloqué
    Available,  // Débloqué mais pas encore visité (en cours)
    Completed   // Fini
}
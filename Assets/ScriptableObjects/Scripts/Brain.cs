using UnityEngine;

public abstract class Brain : ScriptableObject
{
    public abstract void InitializeAI();
    public abstract void Think(EnemyThinker _thinker);
}

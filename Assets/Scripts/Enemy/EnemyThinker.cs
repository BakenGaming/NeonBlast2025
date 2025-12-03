using UnityEngine;

public class EnemyThinker : MonoBehaviour
{
    //testing and debugging enemies only
    [SerializeField] private bool _testing;
    //*******************************************
    private Brain[] brain;

    public void ActivateBrain(EnemyStatSystem _stats)
    {
        if(_testing) return; //remove after testing
        brain = _stats.GetEnemyBRAINS();
        foreach (Brain _brain in brain)
            _brain.InitializeAI();
    }
    private void LateUpdate() 
    {
        if(_testing) return; //remove after testing
        foreach (Brain _brain in brain)
            _brain.Think(this);    
    }
}

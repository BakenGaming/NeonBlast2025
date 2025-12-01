using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThinker : MonoBehaviour
{
    //testing and debugging enemies only
    [SerializeField] private bool _testing;
    //*******************************************
    private Brain[] brain;

    public void ActivateBrain(IHandler _handler)
    {
        if(_testing) return; //remove after testing
        //brain = _handler.GetStats().GetBrains();
        foreach (Brain _brain in brain)
            _brain.InitializeAI(GetComponent<IHandler>());
    }
    private void LateUpdate() 
    {
        if(_testing) return; //remove after testing
        foreach (Brain _brain in brain)
            _brain.Think(this);    
    }
}

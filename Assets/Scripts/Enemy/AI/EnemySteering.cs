using UnityEngine;

public abstract class EnemySteering : MonoBehaviour
{
    public abstract (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData);
}

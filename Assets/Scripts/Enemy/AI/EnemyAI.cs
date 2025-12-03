using UnityEngine;
using System.Collections.Generic;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<EnemySteering> steeringBehaviors;
    [SerializeField] private List<Detector> detectors;
    private AIData aiData;
    private const float DETECTION_DELAY = .05f;

    public void Initialize()
    {
        aiData = GetComponent<AIData>();
        InvokeRepeating("DetectObjects", 0, DETECTION_DELAY);
    }

    private void DetectObjects()
    {
        foreach(Detector detector in detectors)
            detector.Detect(aiData);
        
        float[] danger = new float[8];
        float[] interest = new float[8];

        foreach (EnemySteering steering in steeringBehaviors)
            (danger, interest) = steering.GetSteering(danger, interest, aiData);
    }
}

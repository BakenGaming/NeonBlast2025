using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField] private bool showGizmos;
    Collider2D[] colliders;

    public override void Detect(AIData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, StaticVariables.i.GetEnemyDetectionRadius(), StaticVariables.i.GetObstacleLayer());
        aiData.enemies = colliders;
    }
    void OnDrawGizmos()
    {
        if(!showGizmos) return;

        if(Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach(Collider2D collider in colliders)
            {
                Gizmos.DrawSphere(collider.transform.position, 0.2f);
            }
        }
    }
}

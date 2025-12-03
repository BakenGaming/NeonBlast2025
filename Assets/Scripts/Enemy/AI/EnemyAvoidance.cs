using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoidance : EnemySteering
{
    [SerializeField] private float agentColliderSize = .6f;
    [SerializeField] private bool showGizmos = true;

    float[] dangersResultTemp = null;
    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        foreach (Collider2D enemyCollider in aiData.enemies)
        {
            Vector2 directionToEnemy = enemyCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToEnemy = directionToEnemy.magnitude;
            float weight
             = distanceToEnemy <= agentColliderSize
             ? 1
             : (StaticVariables.i.GetEnemyDetectionRadius() - distanceToEnemy) / StaticVariables.i.GetEnemyDetectionRadius();

             Vector2 directionToEnemyNormalized = directionToEnemy.normalized;
             for (int i = 0; i < Directions.eightDirections.Count; i++)
             {
                float result = Vector2.Dot(directionToEnemyNormalized, Directions.eightDirections[i]);
                float valueToPutIn = result * weight;

                if(valueToPutIn > danger[i]) danger[i] = valueToPutIn;  
             }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }
    #region Draw Gizmos
    void OnDrawGizmos()
    {
        if(!showGizmos) return;
        if(Application.isPlaying && dangersResultTemp != null)
        {
            if(dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * dangersResultTemp[i]);
                }
            }
        }
    }
    #endregion
}
#region Directions Class
public static class Directions
{
    public static List<Vector2> eightDirections = new List<Vector2>
    {
        new Vector2(0,1).normalized,
        new Vector2(1,1).normalized,
        new Vector2(1,0).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(0,-1).normalized,
        new Vector2(-1,-1).normalized,
        new Vector2(-1,0).normalized,
        new Vector2(-1,1).normalized
    };
}
#endregion

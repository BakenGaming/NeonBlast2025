using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField] private bool showGizmos;
    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {   
        Collider2D playerCollider = FindAnyObjectByType<PlayerHandler>().GetComponent<Collider2D>();
        if(playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(playerCollider.transform.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToTarget, StaticVariables.i.GetPlayerLayer());
            if(hit.collider != null && (StaticVariables.i.GetPlayerLayer() & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.Log("Detected Player");
                Debug.DrawRay(transform.position, direction * distanceToTarget, Color.magenta);
                colliders = new List<Transform>() {playerCollider.transform};
            }
            else colliders = null;
        }
        else colliders = null;
        aiData.targets = colliders;
    }
    void OnDrawGizmos()
    {
        if(!showGizmos) return;
        if(colliders == null) return;

        if(Application.isPlaying) 
            Gizmos.DrawWireSphere(transform.position, StaticVariables.i.GetPlayerDetectionRadius());

        Gizmos.color = Color.magenta;
        foreach(var item in colliders)
            Gizmos.DrawSphere(item.position, .3f);
    }
}

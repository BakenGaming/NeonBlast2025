using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStatSystem stats;
    private Rigidbody2D thisRigidbody;
    private Vector3 aimDirection;
    private bool initialized = false;
    private bool canMove = true;
    public void Initialize(EnemyStatSystem _stats)
    {
        stats = _stats;
        thisRigidbody = GetComponent<Rigidbody2D>();
        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized) return;
        UpdateAimDirection(GameManager.i.GetPlayerGO().transform.position);
        if (GameManager.i.GetIsPaused())
        { 
            thisRigidbody.linearVelocity = Vector2.zero;
            return;
        }
    }
    private void UpdateAimDirection(Vector3 _target)
    {
        aimDirection = (_target - transform.position).normalized;
        //transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(aimDirection) - 90f);
    }
    public void ChaseMovement()
    {
        if(GameManager.i.GetIsPaused())
        {
            thisRigidbody.linearVelocity = Vector2.zero;
            return;
        }
        
        thisRigidbody.linearVelocity = aimDirection * stats.GetEnemySPEED();
    }
    public void SetCanMove(bool _canMove){canMove = _canMove;}
    public bool GetCanMove(){return canMove;}
}

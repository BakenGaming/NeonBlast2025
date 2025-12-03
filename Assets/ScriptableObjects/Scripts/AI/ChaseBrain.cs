using UnityEngine;

[CreateAssetMenu(menuName ="Brains/Chase Brain")]
public class ChaseBrain : Brain
{
    private GameObject playerTransform;
    private Vector3 playerLastPosition;
    public override void InitializeAI()
    {
        playerTransform = GameManager.i.GetPlayerGO();
        playerLastPosition = playerTransform.transform.position;
    }
    public override void Think(EnemyThinker _thinker)
    {
        var chaseMovement = _thinker.gameObject.GetComponent<EnemyMovement>();
        if(chaseMovement)
        {
            chaseMovement.ChaseMovement();
        }
    }
}

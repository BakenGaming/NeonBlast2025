using UnityEngine;

public class FollowPlayerHandler : MonoBehaviour
{
    public Transform playerTransform;
    public void Initialize()
    {
        playerTransform = GameManager.i.GetPlayerGO().transform;
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
}

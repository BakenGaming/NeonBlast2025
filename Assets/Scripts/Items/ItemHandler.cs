using System;
using UnityEngine;

public class ItemHandler : MonoBehaviour, ICollectable
{
    public static event Action<int> OnCurrencyCollected;
    public static event Action<int> onEXPCollected;
    public static event Action<float> OnHealthCollected;
    public static event Action OnMagnetCollected;
    public static event Action OnBombCollected;
    [SerializeField] private ItemSO itemSO;
    private Rigidbody2D itemRB;
    private float actualAttractSpeed = 50f;
    private Vector3 targetPosition;
    private bool playerFound, collected;

    public void Initialize()
    {
        itemRB = GetComponent<Rigidbody2D>();
        playerFound = false;
        collected = false;
    }
    public void Collect()
    {
        if(collected) return;
        switch(itemSO.itemType)
        {
            case ItemType.EXP:
            onEXPCollected?.Invoke(itemSO.value);
            break;
            case ItemType.Currency:
            OnCurrencyCollected?.Invoke(itemSO.value);
            break;
            case ItemType.Health:
            OnHealthCollected?.Invoke(.5f);
            break;
            case ItemType.Bomb:
            OnBombCollected?.Invoke();
            break;
            case ItemType.Magnet:
            OnMagnetCollected?.Invoke();
            break;
        }
        ObjectPooler.EnqueueObject(this, "Collectable", PoolType.Collectables);
    }

    public void SetTarget(Vector3 _pos)
    {
        targetPosition = _pos;
        playerFound = true;
    }

    private void FixedUpdate()
    {
        if (playerFound)
        {
            Vector3 targetDirection = (targetPosition - transform.position).normalized;
            itemRB.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * actualAttractSpeed;
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, IDamageable
{
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {

    }

    void Update()
    {
    }

    public void TakeDamage(int _d, bool _isCrit)
    {
        DamagePopup.Create(transform.position, _d, _isCrit);
    }
}

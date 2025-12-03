using UnityEngine;

public class EnemyHandler : MonoBehaviour, IDamageable, IEnemyHandler
{
    [SerializeField] private EnemyStatsSO stats;
    private DamageFlash _dmgFlash;
    private EnemyStatSystem statsSystem;
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _dmgFlash = GetComponent<DamageFlash>();
        statsSystem = new EnemyStatSystem(stats);
        GetComponent<EnemyThinker>().ActivateBrain(statsSystem);
        GetComponent<EnemyMovement>().Initialize(statsSystem);
        GetComponent<EnemyAI>().Initialize();
    }

    void Update()
    {
    }

    public void TakeDamage(int _d, bool _isCrit)
    {
        _dmgFlash.CallDamageFlash();
        DamagePopup.Create(transform.position, _d, _isCrit);
    }
    public EnemyStatSystem GetStats(){return null;}
}

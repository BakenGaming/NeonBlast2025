using UnityEngine;

public class TomatoHandler : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private StaticWeaponStats stats;
    private int actualDamage;
    private float lifeTimer;

    public void Initialize(Transform _t, StaticWeaponStats _stats)
    {
        target = _t;
        rb = GetComponent<Rigidbody2D>();
        lifeTimer = _stats.activelifeTime;
        stats = _stats;
    }

    private void Update() 
    {
        if(target == null || target.gameObject.activeInHierarchy == false) { ObjectPooler.EnqueueObject(this, "Tomato", PoolType.Projectiles); return;}

        Vector3 moveDir = (target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
        
        transform.position += moveDir * stats.speed * Time.deltaTime;
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) DeactivateProjectile();
    }
    private void DeactivateProjectile()
    {
        ObjectPooler.EnqueueObject(this, "Tomato", PoolType.Projectiles);
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = Random.Range(0f,100f) < stats.critChance;
        float dmg = stats.damage * GameManager.i.GetPlayerStats().GetPlayerATK();
        float critDmg = GameManager.i.GetPlayerStats().GetPlayerCRIT() * dmg;

        if(isCritical) actualDamage = (int)(dmg + critDmg);
        else actualDamage = (int)dmg;
        
        if(damageable != null)
        {
            TomatoAcidField newAcidField = ObjectPooler.DequeueObject<TomatoAcidField>("Acid Field", PoolType.Projectiles);
            newAcidField.transform.position = trigger.gameObject.transform.position;
            newAcidField.gameObject.SetActive(true);
            newAcidField.Initialize(stats);

            damageable.TakeDamage(actualDamage, isCritical);
        }

        ObjectPooler.EnqueueObject(this, "Tomato", PoolType.Projectiles);
    }
}

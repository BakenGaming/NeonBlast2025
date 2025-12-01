using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private int damage, actualDamage;
    private float lifeTimer, speed, critChance;

    public void Initialize(Transform _t, int _d, float _lt, float _s, float _c)
    {
        Debug.Log("Initialize Bullet");
        target = _t;
        damage = _d;
        lifeTimer = _lt;
        rb = GetComponent<Rigidbody2D>();
        speed = _s;
        critChance = _c;
    }

    private void Update() 
    {
        if(target == null || target.gameObject.activeInHierarchy == false) { ObjectPooler.EnqueueObject(this, "Bullet", PoolType.Projectiles); return;}

        Vector3 moveDir = (target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        
        transform.position += moveDir * speed * Time.deltaTime;
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) DeactivateProjectile();
    }
    private void DeactivateProjectile()
    {
        ObjectPooler.EnqueueObject(this, "Bullet", PoolType.Projectiles);
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = Random.Range(0f,100f) < critChance;
        if(isCritical) actualDamage = (int)(damage * 1.5f);
        else actualDamage = damage;

        if(damageable != null) 
            damageable.TakeDamage(actualDamage, isCritical);

        ObjectPooler.EnqueueObject(this, "Bullet", PoolType.Projectiles);
    }
}

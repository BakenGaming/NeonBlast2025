using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Tomato Bomb", fileName = "Tomato Bomb")]
public class TomatoBomb : Weapon
{
    private StaticWeaponStats stats;
    private float coolDownTimer;
    private bool readyToFire;
    private Transform target;
    private Transform firePoint;
    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        stats = _s;
        readyToFire = true;
        firePoint = _w.GetFirePoint();
        GameManager.i.SetupObjectPools(stats.spawnedObject.GetComponent<TomatoHandler>(), 50, "Tomato", PoolType.Projectiles);
    }
    public override void TryActivateWeapon()
    {
        if(readyToFire)
        {
            target = FindTarget();
            if(target != null)
            {
                //Debug.Log("Shoot");
                TomatoHandler newProjectile = ObjectPooler.DequeueObject<TomatoHandler>("Tomato", PoolType.Projectiles);
                newProjectile.transform.position = firePoint.position;
                newProjectile.transform.rotation = firePoint.rotation;
                newProjectile.gameObject.SetActive(true);
                newProjectile.Initialize(target, stats);
            }
            ActivateWeaponCooldown();
        }
    }
    public override void ActivateWeaponCooldown()
    {
        readyToFire = false;
        coolDownTimer = stats.coolDown;
    }

    public override void UpdateWeaponTimers()
    {
        if(!readyToFire) coolDownTimer -= Time.deltaTime;
        if(coolDownTimer <= 0) readyToFire = true;
    }

    private Transform FindTarget()
    {
        float distancetoClosestEnemy = Mathf.Infinity;
        EnemyHandler closestEnemy = null;
        EnemyHandler[] allEnemies = FindObjectsByType<EnemyHandler>(FindObjectsSortMode.None);
        
        foreach(EnemyHandler currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - GameManager.i.GetPlayerGO().transform.position).sqrMagnitude;
            if(distanceToEnemy < distancetoClosestEnemy)
            {
                distancetoClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        //Debug.Log(closestEnemy);
        return closestEnemy.transform;        
    }
}

using UnityEngine;

public class OrbitHandler : MonoBehaviour
{
    private Transform pivot;
    private StaticWeaponStats stats;
    private int actualDamage;
    
    public void InitializeOrbiter(Transform _p, StaticWeaponStats _s)
    {
        pivot = _p;
        stats = _s;
    }
    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, stats.rotationSpeed * Time.deltaTime);
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
            damageable.TakeDamage(actualDamage, isCritical);
    }
}

using UnityEngine;

public class StaticFieldHandler : MonoBehaviour
{
    private StaticWeaponStats stats;
    private float damageCooldownTimer, lifeTimer;
    private int actualDamage;
    private bool hasDamaged, readyToDamage;
    public void Initialize(StaticWeaponStats _stats)
    {
        readyToDamage = false;
        stats = _stats;
        lifeTimer = _stats.residualLifetime;
        hasDamaged = false;
    }
    void Update()
    {
        if(hasDamaged) UpdateTimers();
        else TryToCauseDamage();
    }
    private void UpdateTimers()
    {
        damageCooldownTimer -= Time.deltaTime;
        if(damageCooldownTimer <= 0) hasDamaged = false;
    }
    private void TryToCauseDamage()
    {
        EnemyHandler _h;
        int dmgeCnt = 0;
        Collider2D[] damageables = Physics2D.OverlapCircleAll(transform.position, stats.fieldRadius, StaticVariables.i.GetEnemyLayer());
        bool isCritical = Random.Range(0f,100f) < stats.critChance;
        for(int i = 0; i < damageables.Length; i++)
        {
            _h = damageables[i].gameObject.GetComponent<EnemyHandler>();
            if(_h != null)
            {
                float dmg = stats.fieldDamage * GameManager.i.GetPlayerStats().GetPlayerATK();
                float critDmg = GameManager.i.GetPlayerStats().GetPlayerCRIT() * dmg;
                if(isCritical) actualDamage = (int)(dmg + critDmg);
                else actualDamage = (int)dmg;

                _h.TakeDamage(actualDamage, isCritical);
                dmgeCnt++;
            }
        }
        if(dmgeCnt > 0)
        {
            damageCooldownTimer = stats.tickInterval;
            hasDamaged = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.azure;
        Gizmos.DrawWireSphere(transform.position, stats.fieldRadius);
    }
}

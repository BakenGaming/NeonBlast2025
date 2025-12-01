
using Unity.VisualScripting;
using UnityEngine;

public class FusionFieldHandler : MonoBehaviour
{
    private int damage, actualDamage;
    private float critChance, damageCooldownTimer, damageRadius, tickInterval;
    private bool hasDamaged;
    public void Initialize(int _d, float _c, float _r, float _t)
    {
        damage = _d;
        critChance = _c;
        damageRadius = _r;
        tickInterval = _t;
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
        Collider2D[] damageables = Physics2D.OverlapCircleAll(transform.position, damageRadius, StaticVariables.i.GetEnemyLayer());
        bool isCritical = Random.Range(0f,100f) < critChance;
        for(int i = 0; i < damageables.Length; i++)
        {
            _h = damageables[i].gameObject.GetComponent<EnemyHandler>();
            if(_h != null)
            {
                if(isCritical) actualDamage = (int)(damage * 1.5f);
                else actualDamage = damage;
                
                _h.TakeDamage(actualDamage, isCritical);
                dmgeCnt++;
            }
        }
        if(dmgeCnt > 0)
        {
            damageCooldownTimer = tickInterval;
            hasDamaged = true;
        }
    }
}

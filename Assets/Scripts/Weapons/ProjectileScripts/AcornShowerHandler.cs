using UnityEngine;

public class AcornShowerHandler : MonoBehaviour
{
    private StaticWeaponStats stats;
    private GameObject hitParticles, acorn;
    private int actualDamage;
    private bool readyToDamage;

    public void InitializeShower(StaticWeaponStats _s)
    {
        stats = _s;
        acorn = transform.Find("Acorn").gameObject;
        acorn.SetActive(true);
        hitParticles.SetActive(false);
    }
    void Update()
    {
        if(!readyToDamage) return;
        else TryToCauseDamage();
    }
    public void SetReadyToDamage()
    {
        readyToDamage = true;
        acorn.SetActive(false);
        hitParticles.SetActive(true);
    }
    public void DisableAcorn()
    {
        hitParticles.SetActive(false);
        readyToDamage = false;
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
    }
}

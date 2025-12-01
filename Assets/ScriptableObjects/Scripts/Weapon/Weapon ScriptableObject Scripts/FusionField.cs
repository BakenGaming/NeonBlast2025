using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Fusion Field Weapon")]
public class FusionField : Weapon
{
    private StaticWeaponStats stats;
    private PlayerHandler _handler;
    private GameObject fieldParent;
    private FusionFieldHandler _fusionHandler;
    private float coolDownTimer, activeDurationTimer;
    private bool readyToActivate, isActive;
    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        stats = _s;
        _handler = _w.GetComponent<PlayerHandler>();
        readyToActivate = true;
        isActive = false;
        GameManager.i.SetupObjectPools(stats.spawnedObject.GetComponent<FusionFieldHandler>(), 1, "Fusion", PoolType.Projectiles);
    }
    public override void TryActivateWeapon()
    {
        if(readyToActivate)
        {
            fieldParent = new GameObject("Fusion Field");
            fieldParent.AddComponent<FollowPlayerHandler>();
            fieldParent.GetComponent<FollowPlayerHandler>().Initialize();
            FusionFieldHandler newFusionField = ObjectPooler.DequeueObject<FusionFieldHandler>("Fusion", PoolType.Projectiles);
            newFusionField.transform.SetParent(fieldParent.transform);
            newFusionField.transform.position = fieldParent.transform.position;
            newFusionField.gameObject.SetActive(true);
            newFusionField.Initialize(stats.damage, _handler.GetStats().GetCritChance(), stats.fieldRaduis, stats.tickInterval);
            _fusionHandler = newFusionField;
            SetWeaponActive();
        }
    }
    private void SetWeaponActive()
    {
        //activeDurationTimer = stats.lifeTime;
        readyToActivate = false;
        isActive = true;
    }
    public override void UpdateWeaponTimers()
    {
        //THIS WEAPON DOES NOT DEACTIVATE
        //if(!isActive) coolDownTimer -= Time.deltaTime;
        //else activeDurationTimer -= Time.deltaTime;

        //if(coolDownTimer <= 0 && !isActive) readyToActivate = true;
        //if(activeDurationTimer <= 0 && isActive) DeactivateWeapon();
    }
    private void DeactivateWeapon()
    {
        ObjectPooler.EnqueueObject(_fusionHandler, "Fusion", PoolType.Projectiles);
        Destroy(fieldParent.gameObject);
        ActivateWeaponCooldown();
    }
    public override void ActivateWeaponCooldown()
    {
        readyToActivate = false;
        isActive = false;
        coolDownTimer = stats.coolDown;
    }

    public override StaticWeaponStats GetWeaponStats(){return stats;}
}

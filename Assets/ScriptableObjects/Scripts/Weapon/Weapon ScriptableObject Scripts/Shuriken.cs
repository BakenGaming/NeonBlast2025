using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Shuriken", fileName = "Shuriken")]
public class Shuriken : Weapon
{
    private StaticWeaponStats stats;
    private OrbitPositionHandler _orbitPositionHandler;
    private List<OrbitHandler> activeOrbiters;
    private GameObject orbiterPivot;

    private float coolDownTimer, activeDurationTimer;
    private bool readyToActivate, isActive;

    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        stats = _s;
        readyToActivate = true;
        isActive = false;
        activeOrbiters = new List<OrbitHandler>();
        GameManager.i.SetupObjectPools(stats.spawnedObject.GetComponent<OrbitHandler>(), 5, "Shuriken", PoolType.Projectiles);
        _orbitPositionHandler = new OrbitPositionHandler(stats.numberOfOrbiters);
    }
    public override void ActivateWeaponCooldown()
    {
        readyToActivate = false;
        isActive = false;
        coolDownTimer = stats.coolDown;
    }
    public override void TryActivateWeapon()
    {
        if(readyToActivate)
        {
            orbiterPivot = new GameObject("Orbitor Pivot");
            orbiterPivot.AddComponent<FollowPlayerHandler>();
            orbiterPivot.GetComponent<FollowPlayerHandler>().Initialize();
            for(int i = 0; i < stats.numberOfOrbiters; i++)
            {
                OrbitHandler newOrbiter = ObjectPooler.DequeueObject<OrbitHandler>("Shuriken", PoolType.Projectiles);
                newOrbiter.transform.SetParent(orbiterPivot.transform);
                newOrbiter.transform.position = _orbitPositionHandler.GetOrbitPositions()[i];
                newOrbiter.gameObject.SetActive(true);
                newOrbiter.InitializeOrbiter(GameManager.i.GetPlayerGO().transform, stats);
               activeOrbiters.Add(newOrbiter);
            }
            SetWeaponActive();
        }
    }
    private void SetWeaponActive()
    {
        activeDurationTimer = stats.activelifeTime;
        readyToActivate = false;
        isActive = true;
    }
    public override void UpdateWeaponTimers()
    {
        if(!isActive) coolDownTimer -= Time.deltaTime;
        else activeDurationTimer -= Time.deltaTime;

        if(coolDownTimer <= 0 && !isActive) readyToActivate = true;
        if(activeDurationTimer <= 0 && isActive) DeactivateWeapon();
    }

    private void DeactivateWeapon()
    {
        foreach(OrbitHandler orbiter in activeOrbiters)
        {
            ObjectPooler.EnqueueObject(orbiter, "Shuriken", PoolType.Projectiles);
        }
        activeOrbiters.Clear();
        Destroy(orbiterPivot.gameObject);
        ActivateWeaponCooldown();
    }
}

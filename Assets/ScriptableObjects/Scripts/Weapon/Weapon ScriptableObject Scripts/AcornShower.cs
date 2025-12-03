using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Acorn Shower", fileName = "Acorn Shower")]
public class AcornShower : Weapon
{
    private StaticWeaponStats stats;
    private List<AcornShowerHandler> activeShowers;
    private Vector3 lastSpawnedPosition;
    private float spawnMinRadius = 5f;
    private float coolDownTimer, activeDurationTimer;
    private bool readyToActivate, isActive;
    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        stats = _s;
        readyToActivate = true;
        isActive = false;
        activeShowers = new List<AcornShowerHandler>();
        GameManager.i.SetupObjectPools(stats.fieldToSpawn.GetComponent<AcornShowerHandler>(), 5, "Shower", PoolType.Projectiles);
    }
    public override void TryActivateWeapon()
    {
        if(readyToActivate)
        {
            for(int i = 0; i < stats.numberOfFields; i++)
            {
                AcornShowerHandler newShower = ObjectPooler.DequeueObject<AcornShowerHandler>("Shower", PoolType.Projectiles);
                newShower.transform.position = ChooseSpawnLocation();
                newShower.gameObject.SetActive(true);
                newShower.InitializeShower(stats);
                activeShowers.Add(newShower);
            }
            SetWeaponActive();
        }
    }
    private Vector3 ChooseSpawnLocation()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), 
            Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
		
        return new Vector3(screenPosition.x, screenPosition.y, 0f);        
    }
    public override void UpdateWeaponTimers()
    {
        if(!isActive) coolDownTimer -= Time.deltaTime;
        else activeDurationTimer -= Time.deltaTime;

        if(coolDownTimer <= 0 && !isActive) readyToActivate = true;
        if(activeDurationTimer <= 0 && isActive) DeactivateWeapon();
    }
    private void SetWeaponActive()
    {
        //activeDurationTimer = stats.activelifeTime;
        readyToActivate = false;
        isActive = true;
    }
    private void DeactivateWeapon()
    {
        foreach(AcornShowerHandler shower in activeShowers)
        {
            ObjectPooler.EnqueueObject(shower, "Shower", PoolType.Projectiles);
        }
        activeShowers.Clear();

        ActivateWeaponCooldown();
    }

    public override void ActivateWeaponCooldown()
    {
        readyToActivate = false;
        isActive = false;
        coolDownTimer = stats.coolDown;
    }
}

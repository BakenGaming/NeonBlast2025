using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Photon Beam Weapon")]
public class PhotonBeam : Weapon
{
    private StaticWeaponStats stats;
    private PlayerHandler _handler;
    private float coolDownTimer, activeDurationTimer;
    private bool readyToFire, isActive;
    private Transform target;
    private Transform firePoint;
    private GameObject photonBeamObject;
    private LineRenderer photonBeamRenderer;
    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        _handler = _w.GetComponent<PlayerHandler>();
        stats = _s;
        readyToFire = true;
        isActive = false;
        firePoint = _w.GetFirePoint();
        //DisablePhotonBeam();
    }
    public override void TryActivateWeapon()
    {
        if(isActive)
        {
            UpdatePhotonBeam();
            return;
        }
        if(readyToFire)
        {
            target = FindTarget();
            if(target != null)
            {
                if(!isActive)
                {
                    EnablePhotonBeam();
                    isActive = true;
                    readyToFire = false;
                }
            }
        }
    }
    private void EnablePhotonBeam()
    {
        photonBeamObject = new GameObject("Photon Beam");
        photonBeamObject.AddComponent<FollowPlayerHandler>();
        photonBeamObject.GetComponent<FollowPlayerHandler>().Initialize();
        GameObject newBeam = Instantiate(stats.spawnedObject, photonBeamObject.transform.position, Quaternion.identity);
        newBeam.transform.SetParent(photonBeamObject.transform);
        photonBeamRenderer = newBeam.GetComponent<LineRenderer>();
        photonBeamRenderer.enabled = true;
        photonBeamRenderer.SetPosition(0, firePoint.position);
        photonBeamRenderer.SetPosition(1, target.position);
        activeDurationTimer = stats.lifeTime;
        readyToFire = false;
        isActive = true;
    }
    private void UpdatePhotonBeam()
    {
        photonBeamRenderer.SetPosition(0, photonBeamObject.transform.position);
        photonBeamRenderer.SetPosition(1, target.position);
    }

    private void DisablePhotonBeam()
    {
        photonBeamRenderer.enabled = false;
        isActive = false;
        Destroy(photonBeamObject.gameObject);
        ActivateWeaponCooldown();
    }
    public override void ActivateWeaponCooldown()
    {
        readyToFire = false;
        coolDownTimer = stats.coolDown;
    }

    public override void UpdateWeaponTimers()
    {
        if(!isActive) coolDownTimer -= Time.deltaTime;
        else activeDurationTimer -= Time.deltaTime;

        if(coolDownTimer <= 0 && !isActive) readyToFire = true;
        if(activeDurationTimer <= 0 && isActive) DisablePhotonBeam();
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
        return closestEnemy.transform;        
    }
    public override StaticWeaponStats GetWeaponStats(){return stats;}
}

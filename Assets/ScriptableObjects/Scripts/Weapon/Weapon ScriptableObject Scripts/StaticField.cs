using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Static Field", fileName ="Static Field")]
public class StaticField : Weapon
{
    private StaticWeaponStats stats;
    private GameObject fieldParent;
    private bool readyToActivate;
    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        stats = _s;
        readyToActivate = true;
    }
    public override void TryActivateWeapon()
    {
        if(readyToActivate)
        {
            fieldParent = new GameObject("Static Field");
            fieldParent.AddComponent<FollowPlayerHandler>();
            fieldParent.GetComponent<FollowPlayerHandler>().Initialize();
            GameObject newStaticField = Instantiate(stats.fieldToSpawn, fieldParent.transform.position, Quaternion.identity);
            newStaticField.transform.SetParent(fieldParent.transform);
            newStaticField.GetComponent<StaticFieldHandler>().Initialize(stats);
            SetWeaponActive();
        }
    }
    private void SetWeaponActive()
    {
        readyToActivate = false;
    }
    public override void UpdateWeaponTimers(){/*This weapon does not deactivate*/}
    
    public override void ActivateWeaponCooldown(){/*This weapon does not deactivate*/}
}

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private Dictionary<Weapon, int> equippedWeapons;
    private Weapon mainWeapon;
    public void InitializeFreshGame(WeaponSO startingWeapon)
    {
        Debug.Log("Initialize Weapon System");
        equippedWeapons = new Dictionary<Weapon, int>();
        mainWeapon = startingWeapon.weapon;
        EquipNewWeapon(startingWeapon);
    }

    public void InitializeContinuedGame()
    {
        
    }
    void Update()
    {
        ActivateWeapons();
        UpdateWeaponTimers();
    }
    private void ActivateWeapons()
    {
        foreach(Weapon weapon in equippedWeapons.Keys)
            weapon.TryActivateWeapon();
    }

    private void UpdateWeaponTimers()
    {
        foreach(Weapon weapon in equippedWeapons.Keys)
            weapon.UpdateWeaponTimers();
    }
    private void EquipNewWeapon(WeaponSO _w)
    {
        equippedWeapons.Add(_w.weapon, _w.ID);
        _w.weapon.InitializeWeapon(this, _w.weaponStats);
    }
    private void SaveWeapons()
    {
        
    }
    public Transform GetFirePoint(){return firePoint;}
}

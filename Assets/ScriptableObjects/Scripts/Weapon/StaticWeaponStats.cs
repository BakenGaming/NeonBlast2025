using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Stats / Static Weapon Stats")]
public class StaticWeaponStats: ScriptableObject
{
    [Header("All Weapons")]
    public string statsName;
    public int statLevel;
    public float damage;
    public float speed;
    public float critChance;
    public float coolDown;
    public float activelifeTime;
    public GameObject spawnedObject;
    [Header("Orbiter Only")]
    public int numberOfOrbiters;
    public float rotationSpeed;
    [Header("Explosive Only")]
    public float blastRadius;
    [Header("Field Only")]
    public GameObject fieldToSpawn;
    public float fieldDamage;
    public float fieldRadius;
    public float tickInterval;
    public float residualLifetime;
    public int numberOfFields;
}

using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Stats / Static Weapon Stats")]
public class StaticWeaponStats: ScriptableObject
{
    public string statsName;
    public int statLevel;
    public int damage;
    public float coolDown;
    public float lifeTime;
    public GameObject spawnedObject;
    [Header("Orbiter Only")]
    public int numberOfOrbiters;
    public float rotationSpeed;
    [Header("Explosive Only")]
    public float blastRadius;
    [Header("Field Only")]
    public float fieldRaduis;
    public float tickInterval;
}

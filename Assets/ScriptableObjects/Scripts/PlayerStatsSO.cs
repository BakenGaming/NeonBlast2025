using UnityEngine;

[CreateAssetMenu(menuName ="Base Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    public int HP;
    public int ATK;
    public float CRIT;
    public float SPEED;
    public WeaponSO WEAPON;
}

using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public int HP;
    public int ATK;
    public float CRIT;
    public float SPEED;
    public Brain[] BRAINS;
}

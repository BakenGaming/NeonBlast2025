using UnityEngine;

public class EnemyStatSystem 
{
    private int HP;
    private int ATK;
    private float CRIT;
    private float SPEED;
    private Brain[] BRAINS;

    public EnemyStatSystem(EnemyStatsSO _stats)
    {
        HP = _stats.HP;
        ATK = _stats.ATK;
        CRIT = _stats.CRIT;
        SPEED = _stats.SPEED;
        BRAINS = _stats.BRAINS;
    }
    public float GetEnemySPEED(){return SPEED;}
    public Brain[] GetEnemyBRAINS(){return BRAINS;}
}

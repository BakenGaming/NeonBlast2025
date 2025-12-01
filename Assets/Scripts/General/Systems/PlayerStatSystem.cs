public enum PlayerStatType
{HP, ATK, CRIT, SPEED}
public class PlayerStatSystem
{
    private int HP;
    private int ATK;
    private float CRIT;
    private float SPEED;
    private WeaponSO WEAPON;
    public PlayerStatSystem(PlayerStatsSO _stats)
    {
        HP = _stats.HP;
        ATK = _stats.ATK;
        CRIT = _stats.CRIT;
        SPEED = _stats.SPEED;
        WEAPON = _stats.WEAPON;
    }

    public int GetPlayerHP(){return HP;}
    public int GetPlayerATK(){return ATK;}
    public float GetPlayerCRIT(){return CRIT;}
    public float GetPlayerSPEED(){return SPEED;}
    public WeaponSO GetPlayerWEAPON(){return WEAPON;}
}

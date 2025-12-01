using UnityEngine;

public enum ItemType
{EXP, Currency, Health, Bomb, Magnet}
[CreateAssetMenu(menuName ="ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int value;
    public ItemType itemType;
}

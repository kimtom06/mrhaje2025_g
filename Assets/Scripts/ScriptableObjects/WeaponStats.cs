using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Scriptable Objects/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    string weaponName;
    float attackSpeed;
    float damage;
    float attackRange;
}

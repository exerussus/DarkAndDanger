
using UnityEngine;

public class Weapon : ScriptableObject
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject prefab;
    [SerializeField] private WeaponType typeOfWeapon;
    public WeaponType TypeOfWeapon => typeOfWeapon;
    public Item Item => item;
    public GameObject Prefab => prefab;

    public enum WeaponType
    {
        Magical,
        Physical
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon/PhysicalWeapon", fileName = "PhysicalWeapon",order = 51)]
public class PhysicalWeapon : Weapon
{
    [SerializeField] private WeaponHandType handType;
    [SerializeField] private AttackType firstAttack;
    [SerializeField] private AttackType secondAttack;
    [SerializeField] private AttackType thirdAttack;
    public WeaponHandType HandType => handType;
    public AttackType FirstAttack => firstAttack;
    public AttackType SecondAttack => secondAttack;
    public AttackType ThirdAttack => thirdAttack;
}

public enum AttackType
{
    Pierce,
    BluntPierce,
    BluntSlash,
}

public struct WeaponPattern
{
    public AttackType FirstAttack { get; private set; }
    public AttackType SecondAttack { get; private set; }
    public AttackType ThirdAttack { get; private set; }

    public WeaponPattern(AttackType first, AttackType second, AttackType third)
    {
        FirstAttack = first;
        SecondAttack = second;
        ThirdAttack = third;
    }
}

public enum WeaponHandType
{
    OneHand = 1,
    TwoHand = 2
}
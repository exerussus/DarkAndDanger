using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Weapon",order = 51)]
public class Weapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private float weight;
    [SerializeField] private int level;
    [SerializeField] private WeaponHandType handType;
    [SerializeField] private AttackType firstAttack;
    [SerializeField] private AttackType secondAttack;
    [SerializeField] private AttackType thirdAttack;
    [SerializeField] private Item item;


    public string WeaponName => weaponName;
    public float Weight => weight;
    public int Level => level;
    public WeaponHandType HandType => handType;
    public AttackType FirstAttack => firstAttack;
    public AttackType SecondAttack => secondAttack;
    public AttackType ThirdAttack => thirdAttack;
    public Item Item => item;
}

public enum AttackType
{
    Pierce,
    BluntPierce,
    BluntSlash
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
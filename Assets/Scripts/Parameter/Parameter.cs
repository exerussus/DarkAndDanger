using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Parameter
{
    
    [Header("Ресурсы")]
    
    [Tooltip("Здоровье")] 
    public float health;
    [Tooltip("Стамина")] 
    public float stamina;
    [Tooltip("Мана")] 
    public float mana;
    [Tooltip("Регенерация стамины за тик")] 
    public float staminaRegeneration;
    
    
    [Header("Расход стамины")]
    
    [Tooltip("Расход стамины на атаку")] public float staminaAttackCost;
    [Tooltip("Мультипликатор расхода стамины от веса оружия")] public float staminaWeaponWeightMultiply;
    [Tooltip("Расход стамины при попадании в физические объекты")] public float staminaMissAttackCost;
    [Tooltip("Расход стамины на подкрадывание")] public float staminaCrouchCost;
    [Tooltip("Расход стамины при спринте")] public float staminaSprintCost;
    [Tooltip("Расходы стамины на блок")] public float staminaParryCost;
    [Tooltip("Время после расхода стамины, необходимое для начала регенирации")] public float timeAfterDrainStaminaCount;
   
    
    [Header("Модификаторы ударов")]
    [Tooltip("Мультипликатор критического удара, где 1 => 100%")] public float criticalDamageMultiply;
    [Tooltip("Шанс критического удара, где 1 => 100%")] public float criticalChance;
    [Tooltip("Шанс пробивания брони, где 1 => 100%")] public float weakSpotChance;
    [Tooltip("Мультипликатору урона при ударе в спину, где 1 => 100%")] public float backStabMultiply;
    
    
    [Header("Скорость ударов")]
    [Tooltip("")] public float attackSpeed;
    [Tooltip("")] public float rechargeAttackSpeed;
    [Tooltip("")] public float rechargeSpeedAfterHitCollision;
    [Tooltip("")] public float rechargeSpeedAfterHitPhysicalObject;
    [Tooltip("")] public float rechargeSpeedAfterMissAttack;
    [Tooltip("Модификатор уменьшения веса оружия, где 1 => 100%")] public float weaponWeightMultiply;
    
    [Header("Скорость движений")]
    [Tooltip("")] public float moveSpeed;
    [Tooltip("")] public float sprintSpeedMultiply;
    [Tooltip("")] public float crouchSpeedMultiply;
    
    [Tooltip("")] public float actionSpeed;
    [Tooltip("")] public float rotationSpeed;
    [Tooltip("")] public float minRotationSpeed;
    
    [Header("Физический урон")]
    [Tooltip("")] public float pierceDamage; 
    [Tooltip("")] public float bluntDamage; 
    [Tooltip("")] public float slashDamage;
    
    [Header("Физическая защита")]
    [Tooltip("")] public float pierceResist; 
    [Tooltip("")] public float bluntResist; 
    [Tooltip("")] public float slashResist;    
    
    [Header("Магическая атака")]
    [Tooltip("")] public float fireDamage;
    [Tooltip("")] public float iceDamage;
    [Tooltip("")] public float arcaneDamage;
    [Tooltip("")] public float holyDamage;
    
    [Header("Магическая защита")]
    [Tooltip("")] public float fireResist;
    [Tooltip("")] public float iceResist;
    [Tooltip("")] public float arcaneResist;
    [Tooltip("")] public float holyResist;

    [Header("Обзор")]
    [Tooltip("")] public int visionRadius;
    
    [Header("Стелс")]
    [Tooltip("")] public int stepNoise;
    [Tooltip("")] public float stepNoiseCrouchMultiply;
    
    [Header("Магия и способности")]
    [Tooltip("")] public float magicCastSpeed;
    [Tooltip("")] [SerializeField] private int memory;

    public int Memory
    {
        get
        {
            return memory;
        }
        set
        {
            if (value < 0)
            {
                memory = 0;
            }
            else if(value > 10)
            {
                memory = 10;
            }
            else
            {
                memory = (int)Mathf.Floor(value);
            }
        }
    }
}

using System;
using System.Reflection;
using UnityEngine;

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
    [Tooltip("")] public float visionRadius;
    
    [Header("Стелс")]
    [Tooltip("")] public float stepNoise;
    [Tooltip("")] public float stepNoiseCrouchMultiply;
    
    [Header("Магия и способности")]
    [Tooltip("")] public float magicCastSpeed;
    [Tooltip("")] [SerializeField] private float memory;

    public float Memory
    {
        get => memory;
        set
        {
            if (value < 0) memory = 0;
            else if(value > 10) memory = 10;
            else memory = Mathf.Floor(value); 
        }
    }
    public static Parameter operator +(Parameter a, Parameter b)
    {
        Parameter result = new Parameter();
        
        result.health = a.health + b.health;
        result.stamina = a.stamina + b.stamina;
        result.mana = a.mana + b.mana;
        result.staminaRegeneration = a.staminaRegeneration + b.staminaRegeneration;
        result.staminaAttackCost = a.staminaAttackCost + b.staminaAttackCost;
        result.staminaWeaponWeightMultiply = a.staminaWeaponWeightMultiply + b.staminaWeaponWeightMultiply;
        result.staminaMissAttackCost = a.staminaMissAttackCost + b.staminaMissAttackCost;
        result.staminaCrouchCost = a.staminaCrouchCost + b.staminaCrouchCost;
        result.staminaSprintCost = a.staminaSprintCost + b.staminaSprintCost;
        result.staminaParryCost = a.staminaParryCost + b.staminaParryCost;
        result.timeAfterDrainStaminaCount = a.timeAfterDrainStaminaCount + b.timeAfterDrainStaminaCount;
        result.criticalDamageMultiply = a.criticalDamageMultiply + b.criticalDamageMultiply;
        result.criticalChance = a.criticalChance + b.criticalChance;
        result.weakSpotChance = a.weakSpotChance + b.weakSpotChance;
        result.backStabMultiply = a.backStabMultiply + b.backStabMultiply;
        result.attackSpeed = a.attackSpeed + b.attackSpeed;
        result.rechargeAttackSpeed = a.rechargeAttackSpeed + b.rechargeAttackSpeed;
        result.rechargeSpeedAfterHitCollision = a.rechargeSpeedAfterHitCollision + b.rechargeSpeedAfterHitCollision;
        result.rechargeSpeedAfterHitPhysicalObject = a.rechargeSpeedAfterHitPhysicalObject + b.rechargeSpeedAfterHitPhysicalObject;
        result.rechargeSpeedAfterMissAttack = a.rechargeSpeedAfterMissAttack + b.rechargeSpeedAfterMissAttack;
        result.weaponWeightMultiply = a.weaponWeightMultiply + b.weaponWeightMultiply;
        result.moveSpeed = a.moveSpeed + b.moveSpeed;
        result.sprintSpeedMultiply = a.sprintSpeedMultiply + b.sprintSpeedMultiply;
        result.crouchSpeedMultiply = a.crouchSpeedMultiply + b.crouchSpeedMultiply;
        result.actionSpeed = a.actionSpeed + b.actionSpeed;
        result.rotationSpeed = a.rotationSpeed + b.rotationSpeed;
        result.minRotationSpeed = a.minRotationSpeed + b.minRotationSpeed;
        result.pierceDamage = a.pierceDamage + b.pierceDamage;
        result.bluntDamage = a.bluntDamage + b.bluntDamage;
        result.slashDamage = a.slashDamage + b.slashDamage;
        result.pierceResist = a.pierceResist + b.pierceResist;
        result.bluntResist = a.bluntResist + b.bluntResist;
        result.slashResist = a.slashResist + b.slashResist;
        result.fireDamage = a.fireDamage + b.fireDamage;
        result.iceDamage = a.iceDamage + b.iceDamage;
        result.arcaneDamage = a.arcaneDamage + b.arcaneDamage;
        result.holyDamage = a.holyDamage + b.holyDamage;
        result.fireResist = a.fireResist + b.fireResist;
        result.iceResist = a.iceResist + b.iceResist;
        result.arcaneResist = a.arcaneResist + b.arcaneResist;
        result.holyResist = a.holyResist + b.holyResist;
        result.visionRadius = a.visionRadius + b.visionRadius;
        result.stepNoise = a.stepNoise + b.stepNoise;
        result.stepNoiseCrouchMultiply = a.stepNoiseCrouchMultiply + b.stepNoiseCrouchMultiply;
        result.magicCastSpeed = a.magicCastSpeed + b.magicCastSpeed;
        result.memory = a.memory + b.memory;
        
        return result;
    }

    public static Parameter operator *(Parameter a, float multiply)
    {
        Parameter result = new Parameter();

        result.health = a.health * multiply;
        result.stamina = a.stamina * multiply;
        result.mana = a.mana * multiply;
        result.staminaRegeneration = a.staminaRegeneration * multiply;
        result.staminaAttackCost = a.staminaAttackCost * multiply;
        result.staminaWeaponWeightMultiply = a.staminaWeaponWeightMultiply * multiply;
        result.staminaMissAttackCost = a.staminaMissAttackCost * multiply;
        result.staminaCrouchCost = a.staminaCrouchCost * multiply;
        result.staminaSprintCost = a.staminaSprintCost * multiply;
        result.staminaParryCost = a.staminaParryCost * multiply;
        result.timeAfterDrainStaminaCount = a.timeAfterDrainStaminaCount * multiply;
        result.criticalDamageMultiply = a.criticalDamageMultiply * multiply;
        result.criticalChance = a.criticalChance * multiply;
        result.weakSpotChance = a.weakSpotChance * multiply;
        result.backStabMultiply = a.backStabMultiply * multiply;
        result.attackSpeed = a.attackSpeed * multiply;
        result.rechargeAttackSpeed = a.rechargeAttackSpeed * multiply;
        result.rechargeSpeedAfterHitCollision = a.rechargeSpeedAfterHitCollision * multiply;
        result.rechargeSpeedAfterHitPhysicalObject = a.rechargeSpeedAfterHitPhysicalObject * multiply;
        result.rechargeSpeedAfterMissAttack = a.rechargeSpeedAfterMissAttack * multiply;
        result.weaponWeightMultiply = a.weaponWeightMultiply * multiply;
        result.moveSpeed = a.moveSpeed * multiply;
        result.sprintSpeedMultiply = a.sprintSpeedMultiply * multiply;
        result.crouchSpeedMultiply = a.crouchSpeedMultiply * multiply;
        result.actionSpeed = a.actionSpeed * multiply;
        result.rotationSpeed = a.rotationSpeed * multiply;
        result.minRotationSpeed = a.minRotationSpeed * multiply;
        result.pierceDamage = a.pierceDamage * multiply;
        result.bluntDamage = a.bluntDamage * multiply;
        result.slashDamage = a.slashDamage * multiply;
        result.pierceResist = a.pierceResist * multiply;
        result.bluntResist = a.bluntResist * multiply;
        result.slashResist = a.slashResist * multiply;
        result.fireDamage = a.fireDamage * multiply;
        result.iceDamage = a.iceDamage * multiply;
        result.arcaneDamage = a.arcaneDamage * multiply;
        result.holyDamage = a.holyDamage * multiply;
        result.fireResist = a.fireResist * multiply;
        result.iceResist = a.iceResist * multiply;
        result.arcaneResist = a.arcaneResist * multiply;
        result.holyResist = a.holyResist * multiply;
        result.visionRadius = a.visionRadius * multiply;
        result.stepNoise = a.stepNoise * multiply;
        result.stepNoiseCrouchMultiply = a.stepNoiseCrouchMultiply * multiply;
        result.magicCastSpeed = a.magicCastSpeed * multiply;
        result.memory = a.memory * multiply;

        return result;
    }
}

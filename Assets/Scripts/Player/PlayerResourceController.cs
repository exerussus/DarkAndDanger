
using System;
using UnityEngine;

public class PlayerResourceController : MonoBehaviour
{
    [Header("Компоненты")] 
    [SerializeField] private Character character;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private StepObserver stepObserver;
    [SerializeField] private HitsObserver hitObserver;
    private PhysicalAttack weaponAttack;

    private void Start()
    {
        if (playerMovement == null && stepObserver != null) Debug.LogError("Missed playerMovement component or there is an extra stepObserver component");
        if (stepObserver == null && playerMovement != null) Debug.LogError("Missed stepObserver component");
    }

    private void OnEnable()
    {

        if (playerMovement != null)
        {
            playerMovement.OnTrySprinting += IsStaminaEnoughForSprint;
            playerMovement.OnTryCrouching += IsStaminaEnoughForCrouch;
            playerMovement.OnStartSprinting += Sprinting;
            playerMovement.OnStartCrouching += Crouching;
            playerMovement.OnEndSprinting += StopToSprint;
            playerMovement.OnEndCrouching += StopToCrouch;
        }
        if (hitObserver != null) hitObserver.OnTakingPhysicalDamage += TakePhysicalDamage;
    }

    private void OnDisable()
    {
        if (weaponAttack != null)
        {
            weaponAttack.OnStartAttack -= DrainStaminaToAttack;
            weaponAttack.OnTouchPhysicalObject -= DrainStaminaToTouchWall;
            weaponAttack.OnTryAttack -= AttackStaminaIsEnough;
            weaponAttack.BeforeParryStart -= CheckAndDrainStaminaBeforeParry;
            weaponAttack.OnStartParry -= DrainStaminaToStartParry;
        }
        if (playerMovement != null)
        {
            playerMovement.OnTrySprinting -= IsStaminaEnoughForSprint;
            playerMovement.OnTryCrouching -= IsStaminaEnoughForCrouch;
            playerMovement.OnStartSprinting -= Sprinting;
            playerMovement.OnStartCrouching -= Crouching;
            playerMovement.OnEndSprinting -= StopToSprint;
            playerMovement.OnEndCrouching -= StopToCrouch;
        }
        if (hitObserver != null) hitObserver.OnTakingPhysicalDamage -= TakePhysicalDamage;
    }

    public void SetPhysicalAttack(PhysicalAttack physicalAttack)
    {
        weaponAttack = physicalAttack;
        if (weaponAttack != null)
        {
            weaponAttack.OnStartAttack += DrainStaminaToAttack;
            weaponAttack.OnTouchPhysicalObject += DrainStaminaToTouchWall;
            weaponAttack.OnTryAttack += AttackStaminaIsEnough;
            weaponAttack.BeforeParryStart += CheckAndDrainStaminaBeforeParry;
            weaponAttack.OnStartParry += DrainStaminaToStartParry;
        }
    }
    
    private void CheckAndDrainStaminaBeforeParry()
    {
        weaponAttack.isStaminaEnough = character.isEnoughStamina(character.Parameter.staminaParryCost);
        if(weaponAttack.isStaminaEnough) character.DrainStamina(character.Parameter.staminaParryCost);
    }

    private void TakePhysicalDamage(PhysicalDamage damage, float weaponWeight)
    {
        PhysicalDamage damageResist = new PhysicalDamage(
            character.Parameter.bluntResist,
            character.Parameter.pierceResist,
            character.Parameter.slashResist);

        character.TakePhysicalDamage(damage.GetDamageWithArmor(damageResist));
    }

    private void DrainStaminaToStartParry()
    {
        character.DrainStamina(weaponAttack.Weapon.Item.Weight);
    }

    private void AttackStaminaIsEnough()
    {
        weaponAttack.isStaminaEnough = character.isEnoughStamina(weaponAttack.Weapon.Item.Weight * character.Parameter.staminaAttackCost);
    }


    private void StopToCrouch()
    {
        if (playerMovement.IsCrouching) stepObserver.OnStep -= DrainStaminaToCrouch;
    }
    
    private void StopToSprint()
    {
        if (playerMovement.IsSprinting) stepObserver.OnStep -= DrainStaminaToSprint;
    }
    
    private void IsStaminaEnoughForCrouch()
    {
        playerMovement.isStaminaEnough = character.isEnoughStamina(character.Parameter.staminaSprintCost);
    }

    private void IsStaminaEnoughForSprint()
    {
        playerMovement.isStaminaEnough = character.isEnoughStamina(character.Parameter.staminaSprintCost);
    }
    
    private void Crouching()
    {
        if(!playerMovement.IsCrouching) stepObserver.OnStep += DrainStaminaToCrouch;
    }
    
    private void Sprinting()
    {
        if(!playerMovement.IsSprinting) stepObserver.OnStep += DrainStaminaToSprint;
    }

    private void DrainStaminaToCrouch()
    {
        character.DrainStamina(character.Parameter.staminaCrouchCost);
    }

    private void DrainStaminaToSprint()
    {
        character.DrainStamina(character.Parameter.staminaSprintCost);
    }
    
    private void DrainStaminaToTouchWall()
    {
        character.DrainStamina(weaponAttack.Weapon.Item.Weight * character.Parameter.staminaMissAttackCost);
    }
    
    private void DrainStaminaToAttack()
    {
        character.DrainStamina(weaponAttack.Weapon.Item.Weight * character.Parameter.staminaAttackCost);
    }
}

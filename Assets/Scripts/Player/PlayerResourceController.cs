
using System;
using UnityEngine;

public class PlayerResourceController : MonoBehaviour
{
    [Header("Компоненты")] 
    [SerializeField] private Character character;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private StepObserver stepObserver;
    [SerializeField] private HitsObserver hitObserver;
    private PhysicalAttack _physicalAttack;
    private MagicalAttack _magicalAttack;

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
        UnsubscribeAttacks();
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

    private void UnsubscribeAttacks()
    {
        if (_physicalAttack != null)
        {
            _physicalAttack.OnStartAttack -= DrainStaminaToAttack;
            _physicalAttack.OnTouchPhysicalObject -= DrainStaminaToTouchWall;
            _physicalAttack.OnTryAttack -= AttackStaminaIsEnough;
            _physicalAttack.BeforeParryStart -= CheckAndDrainStaminaBeforeParry;
            _physicalAttack.OnStartParry -= DrainStaminaToStartParry;
            _physicalAttack = null;
        }
        if (_magicalAttack != null)
        {
            _magicalAttack.OnTryToCast -= CastManaIsEnough;
            _magicalAttack.OnEndCast -= DrainManaToCast;
            _magicalAttack = null;
        }
    }
    
    public void SetMagicalAttack(MagicalAttack magicalAttack)
    {
        UnsubscribeAttacks();
        _magicalAttack = magicalAttack;
        _magicalAttack.OnTryToCast += CastManaIsEnough;
        _magicalAttack.OnEndCast += DrainManaToCast;
    }
    
    public void SetPhysicalAttack(PhysicalAttack physicalAttack)
    {
        UnsubscribeAttacks();
        _physicalAttack = physicalAttack;
        _physicalAttack.OnStartAttack += DrainStaminaToAttack;
        _physicalAttack.OnTouchPhysicalObject += DrainStaminaToTouchWall;
        _physicalAttack.OnTryAttack += AttackStaminaIsEnough;
        _physicalAttack.BeforeParryStart += CheckAndDrainStaminaBeforeParry;
        _physicalAttack.OnStartParry += DrainStaminaToStartParry;
        
    }

    private void DrainManaToCast()
    {
        character.DrainMana(_magicalAttack.ActuallySpell.ManaCost);
    }
    
    private void CastManaIsEnough()
    {
        _magicalAttack.SetManaEnough(character.isEnoughMana(_magicalAttack.ActuallySpell.ManaCost));
    }

    
    private void CheckAndDrainStaminaBeforeParry()
    {
        _physicalAttack.isStaminaEnough = character.isEnoughStamina(character.Parameter.staminaParryCost);
        if(_physicalAttack.isStaminaEnough) character.DrainStamina(character.Parameter.staminaParryCost);
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
        character.DrainStamina(_physicalAttack.Weapon.Item.Weight);
    }

    private void AttackStaminaIsEnough()
    {
        _physicalAttack.isStaminaEnough = character.isEnoughStamina(_physicalAttack.Weapon.Item.Weight * character.Parameter.staminaAttackCost);
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
        character.DrainStamina(_physicalAttack.Weapon.Item.Weight * character.Parameter.staminaMissAttackCost);
    }
    
    private void DrainStaminaToAttack()
    {
        character.DrainStamina(_physicalAttack.Weapon.Item.Weight * character.Parameter.staminaAttackCost);
    }
}

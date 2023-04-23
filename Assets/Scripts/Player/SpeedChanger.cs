
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PhysicalAttack weaponAttack;
    [SerializeField] private Character character;
    
    private void OnEnable()
    {
        playerMovement.OnStartSprinting += SetSprintSpeed;
        playerMovement.OnStartCrouching += SetCrouchSpeed;
        playerMovement.OnStandardMoving += SetStandardSpeed;
        playerMovement.OnResetRotation += ResetRotation;
        playerMovement.OnSetAttackRotation += SetAttackRotationSpeed;
        if (weaponAttack != null) weaponAttack.OnAttackEnd += AttackRotationReset;
        
    }

    private void OnDisable()
    {
        if (playerMovement != null)
        {
            playerMovement.OnStartSprinting -= SetSprintSpeed;
            playerMovement.OnStartCrouching -= SetCrouchSpeed;
            playerMovement.OnStandardMoving -= SetStandardSpeed;
            playerMovement.OnResetRotation -= ResetRotation;
            playerMovement.OnSetAttackRotation -= SetAttackRotationSpeed;
        }
        if (weaponAttack != null)
        {
            weaponAttack.OnAttackEnd -= AttackRotationReset;
            weaponAttack.OnStartAttack -= AttackRotationSlowdown;
            weaponAttack.OnStartAttack += AttackRotationSlowdown;
        }
    }
    
    private void AttackRotationSlowdown()
    {
        playerMovement.SetAttackRotationSpeed(character.Parameter.minRotationSpeed);
    }

    private void SetSprintSpeed()
    {
        playerMovement.CurrentSpeed = character.Parameter.moveSpeed * character.Parameter.sprintSpeedMultiply;
    }

    private void SetCrouchSpeed()
    {
        playerMovement.CurrentSpeed = character.Parameter.moveSpeed * character.Parameter.crouchSpeedMultiply;
    }

    private void SetStandardSpeed()
    {
        playerMovement.CurrentSpeed = character.Parameter.moveSpeed;
    }
    
    private void AttackRotationReset()
    {
        playerMovement.ResetRotationSpeed();
    }
        
    private void SetAttackRotationSpeed()
    {
        playerMovement.RotationSpeed = character.Parameter.minRotationSpeed;
    }
    
    private void ResetRotation()
    {
        playerMovement.RotationSpeed = character.Parameter.rotationSpeed;
    }

}

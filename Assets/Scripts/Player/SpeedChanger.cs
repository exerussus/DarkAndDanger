
using System;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Character character;

    private void OnEnable()
    {
        playerMovement.OnStartSprinting += SetSprintSpeed;
        playerMovement.OnStartCrouching += SetCrouchSpeed;
        playerMovement.OnStandardMoving += SetStandardSpeed;
    }

    private void OnDisable()
    {
        playerMovement.OnStartSprinting -= SetSprintSpeed;
        playerMovement.OnStartCrouching -= SetCrouchSpeed;
        playerMovement.OnStandardMoving -= SetStandardSpeed;
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
    
}

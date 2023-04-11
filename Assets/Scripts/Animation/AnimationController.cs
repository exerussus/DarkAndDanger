
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Vector3 resultDirection;
    [SerializeField] private float animationSpeedNormalizer = 100f;
    private const string MoveForward = "Forward";
    private const string MoveRight = "Right";
    private const string MoveLeft = "Left";
    private const string Stay = "Stay";
    
    private float normalAnimationSpeed;
    private float stayTimer;
    private bool isStaying;

    private void OnEnable()
    {
        playerMovement.OnMoveForward += Moving;
        playerMovement.OnMoveBack += Moving;
        playerMovement.OnMoveLeft += Moving;
        playerMovement.OnMoveRight += Moving;
        playerMovement.OnTrySprinting += Sprinting;
        playerMovement.OnTryCrouching += Crouching;
    }

    private void OnDisable()
    {
        playerMovement.OnMoveForward -= Moving;
        playerMovement.OnMoveBack -= Moving;
        playerMovement.OnMoveLeft -= Moving;
        playerMovement.OnMoveRight -= Moving;
        playerMovement.OnTrySprinting -= Sprinting;
        playerMovement.OnTryCrouching -= Crouching;
        
    }

    private void Start()
    {
        normalAnimationSpeed = playerAnimator.speed / animationSpeedNormalizer;
    }

    private string GetMovingAnimation()
    {
        ClearMovesBool();
        resultDirection = playerMovement.transform.TransformDirection(playerMovement.MoveDirection);
        if(resultDirection is { x: > 1.1f, y: < 0.5f and > -0.5f } or{ x: < -1.1f, y: < 0.5f and > -0.5f } )
            return MoveForward;
        
        if (resultDirection is { x: < -0.5f, y: > 0.5f } or { x: < -0.5f, y: < -0.5f } or 
            { x: > 0.5f, y: < 0.5f and > -0.5f } or { x: < 0.5f and > -0.5f, y: > 1.1f })
            return MoveLeft;
        
        if (resultDirection is { x: > 0.5f, y: > 0.5f } or { x: > 0.5f, y: < -0.5f } or
            { x: < -0.5f, y: < 0.5f and > -0.5f } or { x: < 0.5f and > -0.5f, y: < -1.1f })
            return MoveRight;
        
        return MoveForward;
    }

    private void ClearMovesBool()
    {
        playerAnimator.SetBool(MoveForward, false);
        playerAnimator.SetBool(MoveLeft, false);
        playerAnimator.SetBool(MoveRight, false);
        playerAnimator.SetBool(Stay, false);
    }
    
    private void Moving()
    {
        stayTimer = 0f;
        isStaying = false;
        playerAnimator.SetBool(GetMovingAnimation(), true);
        playerAnimator.speed = normalAnimationSpeed * playerMovement.CurrentSpeed;
    }

    private void Sprinting()
    {
        playerAnimator.SetBool(GetMovingAnimation(), true);
        playerAnimator.speed = normalAnimationSpeed * playerMovement.CurrentSpeed;
        stayTimer = 0f;
        isStaying = false;
    }

    private void Crouching()
    {
        playerAnimator.SetBool(GetMovingAnimation(), true);
        playerAnimator.speed = normalAnimationSpeed * playerMovement.CurrentSpeed;
        stayTimer = 0f;
        isStaying = false;
    }

    private void FixedUpdate()
    {
        if (isStaying) return;
        stayTimer += Time.fixedDeltaTime;
        if(stayTimer > 0.2f)
        {
            playerAnimator.SetTrigger(Stay);
            isStaying = true;
        }
        
    }
}


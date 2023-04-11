
using UnityEngine;
using System;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private KeyboardController keyboardController;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private Character character;
    [SerializeField] private PlayerMovement playerMovement;
    private InteractionObject _interactionObject;

    private float _interactTime;
    public float InteractTime => _interactTime;
    public float timer;

    private bool isPlayerMoving;
    private bool isInteracting;
    
    public Action OnInteraction;
    public Action OnInteractionEnd;
    public Action OnProgress;

    private void OnEnable()
    {
        OnInteraction += Interacting;
        keyboardController.OnInteract += Interact;
    }
    private void OnDisable()
    {
        OnInteraction -= Interacting;
        keyboardController.OnInteract -= Interact;
    }

    private void Start()
    {
        playerTransform = playerTransform == null ? GetComponent<Transform>() : playerTransform;
    }

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, playerTransform.up, interactionDistance, visibleLayer);
        if (hit.collider != null)
        {
            Debug.DrawLine(playerTransform.position, hit.point, Color.blue);
            _interactionObject = hit.collider.GetComponent<InteractionObject>();
            _interactTime = _interactionObject.TimeCost * 100f / character.Parameter.actionSpeed;
            OnInteraction?.Invoke();
            playerMovement.isEnableToRotation = false;
        }
        else
        {
            Debug.DrawRay(playerTransform.position, playerTransform.up * interactionDistance, Color.yellow);
            isInteracting = false;
        }
        
    }

    private void IsPlayerMoving()
    {
        isPlayerMoving = true;
    }
    
    private void InteractionEnd(bool isSuccessfulInteraction)
     {
         playerMovement.OnStep -= IsPlayerMoving;
         if (isSuccessfulInteraction)
         {
             _interactionObject.InteractionCompleted();
         }
         isInteracting = false;
         isPlayerMoving = false;
         playerMovement.isEnableToRotation = true;
         OnInteractionEnd?.Invoke();

     }
     
     private void Interacting()
     {
         playerMovement.OnStep += IsPlayerMoving;
         Tick.OnFixedUpdate += Progress;
     }

     private void Progress()
     {
         if(timer == 0f)
         {
             timer = Tick.time;
         }

         if (timer == 0) return;
         if (_interactTime + timer < Tick.time)
         {
             Tick.OnFixedUpdate -= Progress;
             timer = 0f;
             isPlayerMoving = false;
             InteractionEnd(isSuccessfulInteraction: true);
         }
         else if (isPlayerMoving)
         {
             Tick.OnFixedUpdate -= Progress;
             timer = 0f;
             InteractionEnd(isSuccessfulInteraction: false);
         }
         else
         {
             OnProgress?.Invoke();
         }
     }
 }

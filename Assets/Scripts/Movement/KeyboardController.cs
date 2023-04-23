using UnityEngine;
using System;

public abstract class KeyboardController : MonoBehaviour
{
    private OrderTypes orderTypes;

    public Action OnMoveForward;
    public Action OnMoveBack;
    public Action OnMoveLeft;
    public Action OnMoveRight;
    public Action OnJump;
    public Action OnFirstAttack;
    public Action OnSecondAttack;
    public Action OnThirdAttack;
    public Action OnInteract;
    public Action OnSprint;
    public Action OnCrouch;
    public Action OnStandardMove;
    public Action OnParry;
    public Action OnStopParry;
    public Action OnSlowdown;
    public Action OnRotation;
    public Action OnReleaseSpell;
    public Action OnChangeWeapon;

    private struct OrderTypes
    {
        public bool Forward;
        public bool Back;
        public bool Left;
        public bool Right;
        public bool Jump;
        public bool FirstAttack;
        public bool SecondAttack;
        public bool ThirdAttack;
        public bool Interact;
        public bool Sprint;
        public bool Crouch;
        public bool Parry;
        public bool StopParry;
        public bool ReleaseSpell;
        public bool ChangeWeapon;

        public void Zero()
        {
            Forward = false;
            Back = false;
            Left = false;
            Right = false;
            Jump = false;
            FirstAttack  = false;
            SecondAttack  = false;
            ThirdAttack  = false;
            Interact  = false;
            Sprint  = false;
            Crouch  = false;
            Parry  = false;
            StopParry  = false;
            ReleaseSpell = false;
            ChangeWeapon = false;

        }
    }

    private void Start()
    {
        orderTypes = new OrderTypes();
    }

    protected abstract bool IsPressRight();
    protected abstract bool IsPressLeft();
    protected abstract bool IsPressForward();
    protected abstract bool IsPressBack();
    protected abstract bool IsPressJump();
    protected abstract bool IsPressInteract();
    protected abstract bool IsPressSprint();
    protected abstract bool IsPressCrouch();
    protected abstract bool IsPressFirstAttack();
    protected abstract bool IsPressSecondAttack();
    protected abstract bool IsPressThirdAttack();
    protected abstract bool IsDownParry();
    protected abstract bool IsUpParry();
    protected abstract bool IsReleasedSpell();
    protected abstract bool IsChangeWeapon();
    
    
    private void Update()
    {
        if (IsPressRight()) orderTypes.Right = true;
        if (IsPressLeft()) orderTypes.Left = true;
        if (IsPressForward()) orderTypes.Forward = true;
        if (IsPressBack()) orderTypes.Back = true;
        if (IsPressJump()) orderTypes.Jump = true;
        if (IsPressInteract()) orderTypes.Interact = true;
        if (IsPressSprint()) orderTypes.Sprint = true;
        else if (IsPressCrouch()) orderTypes.Crouch = true;
        if (IsPressThirdAttack()) orderTypes.ThirdAttack = true;
        else if (IsPressFirstAttack()) orderTypes.FirstAttack = true;
        else if (IsPressSecondAttack()) orderTypes.SecondAttack = true;
        if (IsChangeWeapon()) orderTypes.ChangeWeapon = true;
        if (IsReleasedSpell()) orderTypes.ReleaseSpell = true;
        if (IsDownParry()) orderTypes.Parry = true;
        if (IsUpParry()) orderTypes.StopParry = true;
    }

    private void FixedUpdate()
    {
        if (orderTypes.Right) OnMoveRight?.Invoke();
        if (orderTypes.Left) OnMoveLeft?.Invoke();
        if (orderTypes.Forward) OnMoveForward?.Invoke();
        if (orderTypes.Back) OnMoveBack?.Invoke();
        if (orderTypes.Jump) OnJump?.Invoke();
        if (orderTypes.FirstAttack) OnFirstAttack?.Invoke();
        if (orderTypes.SecondAttack) OnSecondAttack?.Invoke();
        if (orderTypes.ThirdAttack) OnThirdAttack?.Invoke();
        if (orderTypes.Sprint && IsPressMove()) OnSprint?.Invoke();
        else if (orderTypes.Crouch && IsPressMove()) OnCrouch?.Invoke();
        else OnStandardMove?.Invoke();
        if(orderTypes.ChangeWeapon) OnChangeWeapon?.Invoke();
        if(orderTypes.ReleaseSpell) OnReleaseSpell?.Invoke();
        if (orderTypes.Parry) OnParry?.Invoke();
        else if (orderTypes.StopParry) OnStopParry?.Invoke();
        if (orderTypes.Interact && !IsPressMove()) OnInteract?.Invoke();
        
        if(orderTypes is { Right: false, Left: false, Forward: false, Back: false, Jump: false }) OnSlowdown?.Invoke();
        
        OnRotation?.Invoke();
        
        orderTypes.Zero();
    }

    private bool IsPressMove()
    {
        return orderTypes.Right || orderTypes.Left || orderTypes.Forward || orderTypes.Back;
    }
}

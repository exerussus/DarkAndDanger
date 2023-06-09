﻿
using UnityEngine;
using System;

public class PhysicalAttack : MonoBehaviour
{
    [SerializeField] private ColliderObserver colliderObserver;
    [SerializeField] private PhysicalWeapon weapon;
    private KeyboardController keyboardController;
    private const float AttackCooldown = 0.2f;
    private const float ParryCooldown = 0.25f;
    public PhysicalWeapon Weapon => weapon;
    public WeaponPattern WeaponPattern { get; private set; }
    public PhysicalDamage Damage;
    public AttackType attackType;
    public bool isStaminaEnough;
    private bool _isAttacking;
    private bool _isParryProcessing;
    public bool isParring;
    public bool isBlocking;
    public float attackCooldownTimer;
    public float parryCooldownTimer;
    
    public Action OnTryAttack;
    public Action OnStartAttack;
    public Action OnAttackEnd;
    public Action OnThirdAttack;
    public Action OnSecondAttack;
    public Action OnFirstAttack;
    public Action OnTouchPhysicalObject;
    public Action OnTouchDestructibleObject;
    public Action OnTouchEnemy;
    public Action OnGetHitInBlock;
    public Action OnStartParry;
    public Action OnStopParry;
    public Action OnGetHitInParry;
    public Action BeforeParryStart;
    public Action OnStartingWeakDamage;
    public Action OnFullDamage;
    public Action OnEndingWeakDamage;
    public Action OnNoDamage;
    public Action OnMutualTouchWeapons;


    private void OnEnable()
    {
        colliderObserver.OnTouchHitBox += TouchHitBox;
        colliderObserver.OnTouchPhysicalObject += TouchPhysicalObject;
        colliderObserver.OnTouchDestructibleObject += TouchDestructibleObject;
        colliderObserver.OnTouchEnemyWeapon += TouchEnemyWeapon;
    }

    private void OnDisable()
    {
        colliderObserver.OnTouchPhysicalObject -= TouchPhysicalObject;
        colliderObserver.OnTouchDestructibleObject -= TouchDestructibleObject;
        colliderObserver.OnTouchEnemyWeapon -= TouchEnemyWeapon;
        colliderObserver.OnTouchHitBox -= TouchHitBox;
        if (keyboardController != null) UnsubscribeKeyboardController();
    }

    private void Start()
    {
        colliderObserver = colliderObserver == null ? GetComponent<ColliderObserver>() : colliderObserver;
    }

    public void StopParry()
    {
        if (_isParryProcessing) StopToParry();
    }
    
    public void TryParry()
    {
        if (_isAttacking || !IsCooldownParry()) return;

        if (!isParring && !isBlocking && !_isParryProcessing) 
        {
            _isParryProcessing = true;
            BeforeParryStart?.Invoke();
            if (isStaminaEnough)
            {
                OnStartParry?.Invoke();
            }
        }
    }

    private void DoParry()
    {
        colliderObserver.ActivateCollider();
        isParring = true;
    }
    
    private void DoBlock()
    {
        isParring = false;
        isBlocking = true;
    }
    
    public bool TryToAttack()
    {
        if (_isAttacking || !IsCooldownAttack()) return false;
        OnTryAttack?.Invoke();
        return isStaminaEnough;
    }

    public void FirstAttack()
    {
        if (!TryToAttack()) return;
        _isAttacking = true;
        OnStartAttack?.Invoke();
        OnFirstAttack?.Invoke();
    }

    public void SetWeapon(PhysicalWeapon weapon)
    {
        this.weapon = weapon;
        WeaponPattern = new WeaponPattern(Weapon.FirstAttack, Weapon.SecondAttack, Weapon.ThirdAttack);
    }

    public void UnsubscribeKeyboardController()
    {
        keyboardController.OnFirstAttack -= FirstAttack;
        keyboardController.OnSecondAttack -= SecondAttack;
        keyboardController.OnThirdAttack -= ThirdAttack;
        keyboardController.OnParry -= TryParry;
        keyboardController.OnStopParry -= StopParry;
    }
    
    public void SetKeyboardController(KeyboardController keyboardController)
    {
        this.keyboardController = keyboardController;
        keyboardController.OnFirstAttack += FirstAttack;
        keyboardController.OnSecondAttack += SecondAttack;
        keyboardController.OnThirdAttack += ThirdAttack;
        keyboardController.OnParry += TryParry;
        keyboardController.OnStopParry += StopParry;
    }
    
    public void SecondAttack()
    {
        if (!TryToAttack()) return;
        _isAttacking = true;
        OnStartAttack?.Invoke();
        OnSecondAttack?.Invoke();
    }
    
    public void ThirdAttack()
    {
        if (!TryToAttack()) return;
        _isAttacking = true;
        OnStartAttack?.Invoke();
        OnThirdAttack?.Invoke();
    }

    public void SetStartingWeakDamage()
    {
        OnStartingWeakDamage?.Invoke();
        colliderObserver.ActivateCollider();
    }

    public void SetFullDamage()
    {
        OnFullDamage?.Invoke();
    }
    
    public void SetEndingWeakDamage()
    {
        OnEndingWeakDamage?.Invoke();
    }

    public void SetNoDamage()
    {
        OnNoDamage?.Invoke();
        colliderObserver.DeactivateCollider();
    }

    public void AttackEnd()
    {
        attackCooldownTimer = Time.fixedTime;
        OnAttackEnd?.Invoke();
        _isAttacking = false;
    }

    
    public void StopToParry()
    {
        parryCooldownTimer = Time.fixedTime;
        isParring = false;
        isBlocking = false;
        colliderObserver.DeactivateCollider();
        OnStopParry?.Invoke();
        _isParryProcessing = false;
    }

    public void HitInBlock(PhysicalDamage damage, float weight)
    {
        OnGetHitInBlock?.Invoke();
    }

    public void HitInParry()
    {
        OnGetHitInParry?.Invoke();
    }

    private void TouchPhysicalObject()
    {
        colliderObserver.DeactivateCollider();
        OnTouchPhysicalObject?.Invoke();
        if (_isAttacking)
        {
            AttackEnd();
        }
        else StopToParry();
    }

    private bool IsCooldownAttack()
    {
        return attackCooldownTimer + AttackCooldown < Time.fixedTime;
    }
    
    private bool IsCooldownParry()
    {
        return parryCooldownTimer + ParryCooldown < Time.fixedTime;
    }

    private void TouchEnemyWeapon(Collider2D touchedCollider)
    {
        colliderObserver.DeactivateCollider();
        if (_isAttacking)
        {
            PhysicalAttack enemyAttackSystem = touchedCollider.GetComponent<PhysicalAttack>();
            if (enemyAttackSystem.isBlocking)
            {
                enemyAttackSystem.HitInBlock(Damage, Weapon.Item.Weight);
                AttackEnd();
            }
            else if (enemyAttackSystem.isParring)
            {
                enemyAttackSystem.HitInParry();
                AttackEnd();
            }
            else if(enemyAttackSystem._isAttacking) OnMutualTouchWeapons?.Invoke();
        }
    }

    private void TouchHitBox(Collider2D touchedCollider)
    {
        if (_isParryProcessing) return;
        colliderObserver.DeactivateCollider();
        var enemyAttackSystem = touchedCollider.gameObject.GetComponent<HitsObserver>();
        enemyAttackSystem.PhysicalHit(Damage, Weapon.Item.Weight);
        OnTouchEnemy?.Invoke();
        AttackEnd();
    }
    
    private void TouchDestructibleObject(Collider2D touchedCollider)
    {
        if (_isParryProcessing) return;
        colliderObserver.DeactivateCollider();
        DestructibleObject destructibleObject = touchedCollider.gameObject.GetComponent<DestructibleObject>();
        destructibleObject.TakeDamage(Damage);
        OnTouchDestructibleObject?.Invoke();
        AttackEnd();
    }
}



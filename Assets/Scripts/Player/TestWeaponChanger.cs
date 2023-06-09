﻿
using UnityEngine;

public class TestWeaponChanger : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private Weapon firstWeapon;
    [SerializeField] private Weapon secondWeapon;
    private bool _isFirstWeaponActive = true;
    private bool _isChanging;

    private void Start()
    {
        _isChanging = true;
        if (_isFirstWeaponActive) SetActiveFirstWeapon();
        else SetActiveSecondWeapon();
    }

    private void OnEnable()
    {
        weaponHandler.OnEndWeaponChange += SetChangingFalse;
        keyboardController.OnChangeWeapon += ChangeWeapon;
    }

    private void OnDisable()
    {
        weaponHandler.OnEndWeaponChange -= SetChangingFalse;
        keyboardController.OnChangeWeapon -= ChangeWeapon;
    }

    private void SetChangingFalse()
    {
        _isChanging = false;
    }
    
    private void SetActiveFirstWeapon()
    {
        if(firstWeapon is MagicalWeapon magicalWeapon) weaponHandler.SetMagicalWeapon(magicalWeapon); 
        else if(firstWeapon is PhysicalWeapon physicalWeapon) weaponHandler.SetPhysicalWeapon(physicalWeapon); 
        _isFirstWeaponActive = true;
    }
    
    private void SetActiveSecondWeapon()
    {
        if(secondWeapon is MagicalWeapon magicalWeapon) weaponHandler.SetMagicalWeapon(magicalWeapon);
        else if(secondWeapon is PhysicalWeapon physicalWeapon) weaponHandler.SetPhysicalWeapon(physicalWeapon);
        _isFirstWeaponActive = false;
    }
    
    private void ChangeWeapon()
    {
        if (_isChanging) return;
        _isChanging = true;
        if (_isFirstWeaponActive) SetActiveSecondWeapon();
        else SetActiveFirstWeapon();
    }
}

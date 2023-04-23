
using System;
using UnityEngine;

public class MagicalAttack : MonoBehaviour
{
    private KeyboardController _keyboardController;
    private MagicalWeapon _weapon;
    private Spell _actuallySpell;
    private Character _casterCharacter;
    private SpellEffectHandler _casterSpellEffectHandler;
    private int _actuallySpellIndex;
    private float _castTime;
    private bool _isCasting;
    private bool _isManaEnough = true;
    public Spell ActuallySpell => _actuallySpell;
    public Action OnTryToCast;
    public Action OnStartCast;
    public Action OnEndCast;
    
    public void SetWeapon(MagicalWeapon magicalWeapon)
    {
        _weapon = magicalWeapon;
        _actuallySpell = _weapon.SpellList[0];
    }

    public void SetManaEnough(bool isManaEnough)
    {
        _isManaEnough = isManaEnough;
    }
    
    public void SetCasterSpellEffectHandler(SpellEffectHandler spellEffectHandler)
    {
        _casterSpellEffectHandler = spellEffectHandler;
    }
    
    public void SetCharacter(Character character)
    {
        _casterCharacter = character;
    }
    
    public void SetKeyboardController(KeyboardController keyboardController)
    {
        _keyboardController = keyboardController;
        _keyboardController.OnThirdAttack += CastSpell;
        _keyboardController.OnParry += SwitchSpell;
    }

    public void UnsubscribeKeyboardController()
    {
        _keyboardController.OnThirdAttack -= CastSpell;
        _keyboardController.OnParry -= SwitchSpell;
    }

    private void SwitchSpell()
    {
        var newIndex = _actuallySpellIndex + 1;
        if (newIndex < _weapon.SpellList.Count)
        {
            _actuallySpell = _weapon.SpellList[newIndex];
            _actuallySpellIndex = newIndex;
        }
        else
        {
            _actuallySpell = _weapon.SpellList[0];
            _actuallySpellIndex = 0;
        }
    }
    
    private void CastSpell()
    {
        if (_isCasting) return;
        OnTryToCast?.Invoke();
        if (!_isManaEnough) return; 
        _isCasting = true;
        _castTime = Time.fixedTime + _actuallySpell.TimeCastCost;
        Tick.OnFixedUpdate += Casting;
        _keyboardController.OnReleaseSpell += DisableCasting;
    }

    private void DisableCasting()
    {
        Unsubscribe();
        _isCasting = false;
    }

    private void Unsubscribe()
    {
        _keyboardController.OnReleaseSpell -= DisableCasting;
        Tick.OnFixedUpdate -= Casting;
    }
    
    private void Casting()
    {
        if (_castTime > Time.fixedTime) return;
        DisableCasting();
        EndCast();
    }
    
    private void EndCast()
    {
        OnEndCast?.Invoke();
        SpellCaster.CastSpell(transform.parent, _casterCharacter, _actuallySpell, _casterSpellEffectHandler);
    }
    
}

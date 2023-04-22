
using System;
using UnityEngine;

public class TestingSpellCasting : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;
    [SerializeField] private Spell spell;
    [SerializeField] private Character caster;
    [SerializeField] private Transform casterTransform;
    [SerializeField] private SpellEffectHandler casterSpellEffectHandler;
    private float _castTime;
    private bool _isCasting;

    public Action OnStartCasting;
    
    public void OnEnable()
    {
        keyboardController.OnJump += Cast;
    }

    public void OnDisable()
    {
        keyboardController.OnJump -= Cast;
    }
    
    private void Cast()
    {
        if (_isCasting) return;
        _isCasting = true;
        _castTime = Time.fixedTime + spell.TimeCastCost;
        Tick.OnFixedUpdate += Casting;
        keyboardController.OnInteract += CheckCancelCast;
        OnStartCasting?.Invoke();
    }

    private void Casting()
    {
        if (_castTime > Time.fixedTime) return;
        DisableCasting();
        EndCast();
    }

    private void CheckCancelCast()
    {
        DisableCasting();
    }

    private void DisableCasting()
    {
        Tick.OnFixedUpdate -= Casting;
        keyboardController.OnInteract -= CheckCancelCast;
        _isCasting = false;
    }
    
    private void EndCast()
    {
        SpellCaster.CastSpell(casterTransform, caster, spell, casterSpellEffectHandler);
    }
}


using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    private bool _isAlive = true;
    [SerializeField] private int userID;
    [SerializeField] private Personality personality;
    public Personality Personality => personality;
    public Parameter Parameter => personality.Parameter;
    private CharacterResource _currentHealth;
    private CharacterResource _currentStamina;
    private CharacterResource _currentMana;

    [SerializeField] private float currentHealth;
    [SerializeField] private float currentStamina;

    private float timeAfterDrainStaminaTimer;

    public Action OnTakeDamage;
    public Action OnRestoreHealth;
    public Action OnDrainStamina;
    public Action OnRestoreStamina;
    public Action OnDrainMana;
    public Action OnRestoreMana;
    public Action OnDead;

    public float Health => _currentHealth.Value;
    public float Stamina => _currentStamina.Value;
    public float Mana => _currentMana.Value;

    private void Awake()
    {
        personality ??= GetComponent<Personality>();
        personality.RecalculateParameter();
    }

    private void OnEnable()
    {
        Tick.OnTick  += StaminaRegeneration;
    }

    private void OnDisable()
    {
        Tick.OnTick -= StaminaRegeneration;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentHealth = new CharacterResource(Parameter.health);
        _currentStamina = new CharacterResource(Parameter.stamina);
        _currentMana = new CharacterResource(Parameter.mana);
        currentHealth = Health;
        currentStamina = Stamina;
    }
        
    public void TakeMagicalDamage(MagicalDamage damage)
    {
        _currentHealth.Value -= damage.Fire + damage.Water + damage.Air 
                                + damage.Earth + damage.Poison + damage.Holy 
                                + damage.Necro + damage.Arcane;
        _isAlive = _currentHealth.Value > 0;
        if(!_isAlive) Dead();
        OnTakeDamage?.Invoke();
        currentHealth = Health;
    }
    
    public void TakePhysicalDamage(PhysicalDamage damage)
    {
        _currentHealth.Value -= damage.Blunt + damage.Pierce + damage.Slash;
        _isAlive = _currentHealth.Value > 0;
        if(!_isAlive) Dead();
        OnTakeDamage?.Invoke();
        currentHealth = Health;
    }
    
    private void StaminaRegeneration()
    {
        if (Time.time - timeAfterDrainStaminaTimer > Parameter.timeAfterDrainStaminaCount)
        {
            timeAfterDrainStaminaTimer = Time.time;
            RestoreStamina(Parameter.staminaRegeneration);
        }
        currentStamina = Stamina;
    }
    
    public void RestoreStamina(float value)
    {
        if(value > 0) _currentStamina.Value += value;
        if (_currentStamina.Value > Parameter.stamina) _currentStamina.Value = Parameter.stamina;
        OnRestoreStamina?.Invoke();
        currentStamina = Stamina;
    }
    
    public void RestoreHealth(float value)
    {
        if(value > 0) _currentHealth.Value += value;
        if (_currentHealth.Value > Parameter.health) _currentHealth.Value = Parameter.health;
        OnRestoreHealth?.Invoke();
        currentHealth = Health;
    }
    
    public void RestoreMana(float value)
    {
        if(value > 0) _currentMana.Value += value;
        if (_currentMana.Value > Parameter.mana) _currentMana.Value = Parameter.mana;
        OnRestoreMana?.Invoke();
    }
    
    public bool isEnoughMana(float value)
    {
        return _currentMana.Value - value >= 0;
    }
    
    public bool isEnoughStamina(float value)
    {
        return _currentStamina.Value - value >= 0;
    }

    public void DrainStamina(float value)
    {
        if(value > 0) _currentStamina.Value -= value;
        OnDrainStamina?.Invoke();
        timeAfterDrainStaminaTimer = Time.time;
        currentStamina = Stamina;
    }
    
    public void DrainMana(float value)
    {
        if(value > 0) _currentMana.Value -= value;
        OnDrainMana?.Invoke();
    }
    
    public void Dead()
    {
        OnDead?.Invoke();
    }
}

public struct CharacterResource
{
    private float _value;
    public float Value
    {
        get => _value;
        set
        {
            if (value < 0) _value = 0;
            else _value = value;
        }
    }

    public CharacterResource(float value)
    {
        _value = value;
    }

}

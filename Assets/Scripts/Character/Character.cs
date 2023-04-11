
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class Character : MonoBehaviour
{
    private bool _isAlive = true;
    [SerializeField] private int userID;
    [SerializeField] private Parameter parameter;
    public Parameter Parameter => parameter;
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


    private void Awake()
    {
        parameter = UserCharactersData.GetUserParameter(userID);
    }

    private void OnEnable()
    {
        
        _currentHealth = new CharacterResource(parameter.health);
        _currentStamina = new CharacterResource(parameter.stamina);
        _currentMana = new CharacterResource(parameter.mana);
        
        Tick.OnTick += StaminaRegeneration;
    }

    private void OnDisable()
    {
        Tick.OnTick -= StaminaRegeneration;
    }

    public float Health => _currentHealth.Value;
    public float Stamina => _currentStamina.Value;
    public float Mana => _currentMana.Value;


    private void Start()
    {
        currentHealth = Health;
        currentStamina = Stamina;
    }

    public void TakeDamage(PhysicalDamage damage)
    {

        _currentHealth.Value -= damage.Blunt + damage.Pierce + damage.Slash;
        _isAlive = _currentHealth.Value > 0;
        if(!_isAlive) Dead();
        OnTakeDamage?.Invoke();
        

        currentHealth = Health;
    }
    
    private void StaminaRegeneration()
    {
        if (Time.time - timeAfterDrainStaminaTimer > parameter.timeAfterDrainStaminaCount)
        {
            timeAfterDrainStaminaTimer = Time.time;
            RestoreStamina(parameter.staminaRegeneration);
        }
        
        currentStamina = Stamina;
    }
    
    public void RestoreStamina(float value)
    {
        if(value > 0) _currentStamina.Value += value;
        if (_currentStamina.Value > parameter.stamina) _currentStamina.Value = parameter.stamina;
        OnRestoreStamina?.Invoke();
        currentStamina = Stamina;
    }
    
    public void RestoreHealth(float value)
    {
        if(value > 0) _currentHealth.Value += value;
        if (_currentHealth.Value > parameter.health) _currentHealth.Value = parameter.health;
        OnRestoreHealth?.Invoke();
        currentHealth = Health;
    }
    
    public void RestoreMana(float value)
    {
        if(value > 0) _currentMana.Value += value;
        if (_currentMana.Value > parameter.mana) _currentMana.Value = parameter.mana;
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


using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesBar : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider StaminaSlider;
    [SerializeField] private Slider ManaSlider;
    
    
    public Resource Health;
    public Resource Stamina;
    public Resource Mana;


    private void OnEnable()
    {
        character.OnRestoreHealth += UpdateHealth;
        character.OnTakeDamage += UpdateHealth;
        character.OnRestoreStamina += UpdateStamina;
        character.OnDrainStamina += UpdateStamina;
        character.OnRestoreMana += UpdateMana;
        character.OnDrainMana += UpdateMana;
        character.OnRecalculateParameter += SetMaxAfterRecalculate;
    }
    private void OnDisable()
    {
        character.OnRestoreHealth -= UpdateHealth;
        character.OnTakeDamage -= UpdateHealth;
        character.OnRestoreStamina -= UpdateStamina;
        character.OnDrainStamina -= UpdateStamina;
        character.OnRestoreMana -= UpdateMana;
        character.OnDrainMana -= UpdateMana;
        character.OnRecalculateParameter -= SetMaxAfterRecalculate;
    }

    private void Start()
    {
        Health = new Resource(HealthSlider, character.Health);
        Stamina = new Resource(StaminaSlider, character.Stamina);
        Mana = new Resource(ManaSlider, character.Mana);
    }

    private void UpdateMana()
    {
        Mana.SetValue(character.Mana);
    }

    private void UpdateStamina()
    {
        Stamina.SetValue(character.Stamina);
    }
    
    private void UpdateHealth()
    {
        Health.SetValue(character.Health);
    }

    private void SetMaxAfterRecalculate()
    {
        Health.SetMaxValue(character.Parameter.health);
        Stamina.SetMaxValue(character.Parameter.stamina);
        Mana.SetMaxValue(character.Parameter.mana);
    }
    
    public struct Resource
    {
        private Slider _slider;

        public Resource(Slider slider, float maxValue)
        {
            _slider = slider;
            _slider.maxValue = maxValue;
            _slider.value = maxValue;
        }
        
        public void SetMaxValue(float value)
        {
            _slider.maxValue = value;
            _slider.value = value;
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }
    }
    
}

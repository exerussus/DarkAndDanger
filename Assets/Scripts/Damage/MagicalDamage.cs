
using UnityEngine;

public class MagicalDamage
{
    private float _fire;
    private float _water;
    private float _air;
    private float _earth;
    private float _poison;
    private float _holy;
    private float _necro;
    private float _arcane;
    
    private const float MagicResistanceMultiply = 0.03f;
    
    public float Fire =>  _fire;
    public float Water =>  _water;
    public float Air =>  _air;
    public float Earth =>  _earth;
    public float Poison =>  _poison;
    public float Holy =>  _holy;
    public float Necro =>  _necro;
    public float Arcane =>  _arcane;
    
    public MagicalDamage(
        float fire, 
        float water, 
        float air, 
        float earth, 
        float poison, 
        float holy, 
        float necro, 
        float arcane )
    {
        _fire = fire;
        _water = water;
        _air = air;
        _earth = earth;
        _poison = poison;
        _holy = holy;
        _necro = necro;
        _arcane = arcane;
    }
    
    public void Zero()
    {
        _fire = _water = _air = _earth = _poison = _holy = _necro = _arcane = 0.0f;
    }

    public void SetDamage(MagicalDamage magicalDamage)
    {
        _fire = magicalDamage.Fire;
        _water = magicalDamage.Water;
        _air = magicalDamage.Air;
        _earth = magicalDamage.Earth;
        _poison = magicalDamage.Poison;
        _holy = magicalDamage.Holy;
        _necro = magicalDamage.Necro;
        _arcane = magicalDamage.Arcane;
    }
        
    public void AddDamage(MagicalDamage magicalDamage)
    {
        _fire += magicalDamage.Fire;
        _water += magicalDamage.Water;
        _air += magicalDamage.Air;
        _earth += magicalDamage.Earth;
        _poison += magicalDamage.Poison;
        _holy += magicalDamage.Holy;
        _necro += magicalDamage.Necro;
        _arcane += magicalDamage.Arcane;
    }

    public void SubtractDamage(MagicalDamage magicalDamage)
    {
        _fire -= magicalDamage.Fire;
        _water -= magicalDamage.Water;
        _air -= magicalDamage.Air;
        _earth -= magicalDamage.Earth;
        _poison -= magicalDamage.Poison;
        _holy -= magicalDamage.Holy;
        _necro -= magicalDamage.Necro;
        _arcane -= magicalDamage.Arcane;
    }

    public MagicalDamage GetDifference(MagicalDamage magicalDamage)
    {
        return new MagicalDamage(
        _fire - magicalDamage.Fire,
        _water - magicalDamage.Water,
        _air - magicalDamage.Air,
        _earth - magicalDamage.Earth,
        _poison - magicalDamage.Poison,
        _holy - magicalDamage.Holy,
        _necro - magicalDamage.Necro,
        _arcane - magicalDamage.Arcane
        );
    }
    
    public MagicalDamage GetDamageWithResistance(MagicalDamage damageResistance)
    {
        return new MagicalDamage(
           fire: GetDamageValueWithResistance(damage: _fire, damageResistance: damageResistance.Fire),
           water: GetDamageValueWithResistance(damage: _water, damageResistance: damageResistance.Water),
           air: GetDamageValueWithResistance(damage: _air, damageResistance: damageResistance.Air),
           earth: GetDamageValueWithResistance(damage: _earth, damageResistance: damageResistance.Earth),
           poison: GetDamageValueWithResistance(damage: _poison, damageResistance: damageResistance.Poison),
           holy: GetDamageValueWithResistance(damage: _holy, damageResistance: damageResistance.Holy),
           necro: GetDamageValueWithResistance(damage: _necro, damageResistance: damageResistance.Necro),
           arcane: GetDamageValueWithResistance(damage: _arcane, damageResistance: damageResistance.Arcane)
        );
    }
    
    private float GetDamageValueWithResistance(float damage, float damageResistance)
    {
        var armor = 100f * (damageResistance * MagicResistanceMultiply) / (1f + damageResistance * MagicResistanceMultiply);
        var decrease = armor == 0 ? 0 : (damage / 100f * armor);
        return Mathf.Round(damage - decrease);
    }
}

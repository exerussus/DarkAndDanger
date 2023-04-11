
using UnityEngine;

public class PhysicalDamage
{
    private float _blunt;
    private float _pierce;
    private float _slash;

    private const float ArmorMultiply = 0.03f;
    public float Blunt => _blunt;
    public float Pierce => _pierce;
    public float Slash => _slash;

    public PhysicalDamage(float blunt, float pierce, float slash)
    {
        _blunt = blunt;
        _pierce = pierce;
        _slash = slash;
    }

    public void Clear()
    {
        _blunt = _pierce = _slash = 0f;
    }

    public void SetDamage(PhysicalDamage physicalDamage)
    {
        _blunt = physicalDamage.Blunt;
        _pierce = physicalDamage.Pierce;
        _slash = physicalDamage.Slash;
    }
    
    public void AddDamage(PhysicalDamage physicalDamage)
    {
        _blunt += physicalDamage.Blunt;
        _pierce += physicalDamage.Pierce;
        _slash += physicalDamage.Slash;
    }

    public void SubtractDamage(PhysicalDamage physicalDamage)
    {
        _blunt -= physicalDamage.Blunt;
        _pierce -= physicalDamage.Pierce;
        _slash -= physicalDamage.Slash;
    }

    public PhysicalDamage GetDifference(PhysicalDamage subtrahend)
    {
        return new PhysicalDamage(
            _blunt - subtrahend.Blunt, 
            _pierce - subtrahend.Pierce, 
            _slash - subtrahend.Slash);
    }

    public PhysicalDamage GetReducedDamage(float denominator)
    {
        return new PhysicalDamage(
            _blunt / denominator,
            _pierce / denominator,
            _slash / denominator
        );
    }

    public PhysicalDamage GetDamageWithArmor(PhysicalDamage damageResistance)
    {
        return new PhysicalDamage(
            blunt: GetDamageValueWithArmor(damage: _blunt, damageResistance: damageResistance.Blunt),
            pierce: GetDamageValueWithArmor(damage: _pierce, damageResistance: damageResistance.Pierce),
            slash: GetDamageValueWithArmor(damage: _slash, damageResistance: damageResistance.Slash)
        );
    }

    private float GetDamageValueWithArmor(float damage, float damageResistance)
    {
        var armor = 100f * (damageResistance * ArmorMultiply) / (1f + damageResistance * ArmorMultiply);
        var decrease = armor == 0 ? 0 : (damage / 100f * armor);
        return Mathf.Round(damage - decrease);
    }

    public PhysicalDamage GetBluntSlashDamage()
    {
        return new PhysicalDamage(blunt: _blunt, pierce: 0f, slash: _slash);
    }
    
    public PhysicalDamage GetBluntPierceDamage()
    {
        return new PhysicalDamage(blunt: _blunt, pierce: _pierce, slash: 0f);
    }
    
    public PhysicalDamage GetPierceDamage()
    {
        return new PhysicalDamage(blunt: 0f, pierce: _pierce, slash: 0f);
    }

    public void Zero()
    {
        _blunt = 0f;
        _pierce = 0f;
        _slash = 0f;
    }
}

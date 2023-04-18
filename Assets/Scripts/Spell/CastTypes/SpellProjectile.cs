
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    private GameObject _caster;
    private Spell _spell;
    private Vector3 _direction;
    private bool _isActivated;

    public SpellProjectile(GameObject caster, Spell spell)
    {
        _caster = caster;
        _spell = spell;
    }

    public void SetSpell(Spell spell)
    {
        _spell = spell;
    }
    
    public void SetCaster(GameObject caster)
    {
        _caster = caster;
    }
    
    public void Activate()
    {
        _isActivated = true;
    }

    private void FixedUpdate()
    {
        if (!_isActivated) return;
        
        var movingDistance = _spell.Distance / _spell.ProjectileSpeed * Time.fixedTime;
        transform.Translate(new Vector3(0f, movingDistance, 0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

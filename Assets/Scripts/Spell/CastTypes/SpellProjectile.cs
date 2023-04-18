
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    private Spell _spell;
    private Vector3 _direction;
    private bool _isActivated;
    private bool _isTouched;
    private List<Collider2D> _detectedCollidersList;

    public SpellProjectile(GameObject caster, Spell spell)
    {
        _spell = spell;
    }

    public void SetSpell(Spell spell)
    {
        _spell = spell;
    }

    public void Activate()
    {
        _isActivated = true;
    }

    private void FixedUpdate()
    {
        if (!_isActivated || _isTouched) return;
        
        var movingDistance = _spell.Distance / _spell.ProjectileSpeed * Time.fixedTime;
        transform.Translate(new Vector3(0f, movingDistance, 0f));
    }

    private bool IsTouchingObjects(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox") ||
            collision.gameObject.CompareTag("PhysicalObject") ||
            collision.gameObject.CompareTag("DestructibleObject"))
            return true;
        return false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isTouched) return;
        if (IsTouchingObjects(collision))
        {
            _isTouched = true;
            GetDirection();
            AddSpellEffects();
            Destroy(this);
        }
    }

    private void GetDirection()
    {
        float j = 0;
        int rayCount = 36;
        for (int i = 0; i < rayCount; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += (360 / rayCount) * Mathf.Deg2Rad;
            
            Vector2 direction = transform.TransformDirection(new Vector2(x, y));
            DetectObjects(direction);
            
            if (x != 0)
            {
                direction = transform.TransformDirection(new Vector3(-x, y, 0));
                DetectObjects(direction);
            }
        }
    }
    
    private void DetectObjects(Vector2 direction)
    {
        foreach (var layerTarget in _spell.LayerTargets)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _spell.Area, layerTarget);
            if (hit.collider == null) return;
            if(!_detectedCollidersList.Contains(hit.collider)) _detectedCollidersList.Add(hit.collider);
            break;
        }
    }

    private void AddSpellEffects()
    {
        foreach (var collider in _detectedCollidersList)
        {
            var spellEffectHandler = collider.GetComponent<SpellEffectHandler>();
            spellEffectHandler.AddSpell(_spell);
        }
        
    }
}

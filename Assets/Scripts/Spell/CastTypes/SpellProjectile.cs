
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    private Character _caster;
    private Spell _spell;
    private Vector3 _direction;
    private bool _isActivated;
    private bool _isProjectileLifeEnd;
    private List<Collider2D> _detectedCollidersList;
    [SerializeField] private GameObject projectilePrefab;

    private Vector2 debugPosition;
    private Vector2 debugDirection;

    public Action<Vector2> OnGetDirection;
    public Action<Collider2D> OnDetected;
    public Action<Character, SpellEffectHandler, Spell> OnAddSpellToHandler;

    private float timeLife;
    
    public SpellProjectile(Character caster, Spell spell)
    {
        _caster = caster;
        _spell = spell;
    }
    
    public void SetCaster(Character caster)
    {
        _caster = caster;
    }
    
    public void SetSpell(Spell spell)
    {
        _spell = spell;
    }

    public void Activate()
    {
        _detectedCollidersList = new List<Collider2D>();
        _isActivated = true;
        timeLife = _spell.Distance / _spell.ProjectileSpeed + Time.fixedTime;
    }

    private void FixedUpdate()
    {
        if (!_isActivated || _isProjectileLifeEnd) return;
        var movingDistance = _spell.ProjectileSpeed * Time.fixedDeltaTime;
        transform.Translate(new Vector3(0f, movingDistance, 0f));
        if (timeLife < Time.fixedTime) BlowUpProjectile();
    }

    private bool IsTouchingObjects(Collider2D collider)
    {

        return collider.gameObject.CompareTag("HitBox") ||
               collider.gameObject.CompareTag("PhysicalObject") ||
               collider.gameObject.CompareTag("DestructibleObject");
    }

    private void BlowUpProjectile()
    {
        _isProjectileLifeEnd = true;
        GetDirection();
        BlowEffect();
        AddSpellEffects();
        Destroy(projectilePrefab.gameObject);
    }

    private void BlowEffect()
    {
        var mainEffect = Instantiate(_spell.MainEffect);
        mainEffect.transform.position = transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isProjectileLifeEnd) return;
        if (!IsTouchingObjects(collider)) return;
        BlowUpProjectile();
    }

    private void DebugDraw(Vector2 point, bool isHit)
    {
        debugPosition = transform.position;
        if(isHit)
        {
            Debug.DrawLine(debugPosition, point, Color.red, duration: 1f);
        }
        else Debug.DrawRay(debugPosition, debugDirection * _spell.Area, Color.yellow, duration:1f);
    }
    
    private void GetDirection()
    {
        float j = 0;
        int rayCount = 36;
        for (int i = 0; i < rayCount; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += (180 / rayCount) * Mathf.Deg2Rad;
            
            Vector2 direction = transform.TransformDirection(new Vector2(x, y));
            debugDirection = direction;
            OnGetDirection?.Invoke(direction);
            DetectObjects(direction);
            
            if (x != 0)
            {
                direction = transform.TransformDirection(new Vector2(-x, y));
                debugDirection = direction;
                OnGetDirection?.Invoke(direction);
                DetectObjects(direction);
            }
        }
    }
    
    private void DetectObjects(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _spell.Area, _spell.LayerTargets);
        DebugDraw(hit.point, hit.collider != null);
        if (hit.collider == null) return;
        if(!_detectedCollidersList.Contains(hit.collider)) _detectedCollidersList.Add(hit.collider);
        OnDetected?.Invoke(hit.collider);
    }

    private void AddSpellEffects()
    {
        if (_detectedCollidersList.Count == 0) return;
        foreach (var collider in _detectedCollidersList)
        {

            var spellEffectHandler = collider.GetComponent<SpellEffectHandler>();
            OnAddSpellToHandler?.Invoke(_caster, spellEffectHandler, _spell);
            spellEffectHandler.AddSpell(_spell, _caster.Parameter);

        }
        
    }
}

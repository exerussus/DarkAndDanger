
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    private GameObject _caster;
    private Spell _spell;
    private Vector3 _direction;
    private bool _isActivated;
    private bool _isTouched;
    private List<Collider2D> _detectedCollidersList;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private GameObject projectilePrefab;

    private Vector2 debugPosition;
    private Vector2 debugDirection;

    public Action<Vector2> OnGetDirection;
    public Action<Collider2D> OnDetected;
    public Action<GameObject, SpellEffectHandler, Spell> OnAddSpellToHandler;
    
    public SpellProjectile(GameObject caster, Spell spell)
    {
        _caster = caster;
        _spell = spell;
    }
    
    public void SetCaster(GameObject caster)
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
    }

    private void FixedUpdate()
    {
        if (!_isActivated || _isTouched) return;
        var movingDistance = _spell.Distance / _spell.ProjectileSpeed * Time.fixedDeltaTime;
        transform.Translate(new Vector3(0f, movingDistance, 0f));
    }

    private bool IsTouchingObjects(Collider2D collider)
    {

        return collider.gameObject.CompareTag("HitBox") ||
               collider.gameObject.CompareTag("PhysicalObject") ||
               collider.gameObject.CompareTag("DestructibleObject");
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isTouched) return;
        if (!IsTouchingObjects(collider)) return;
        _isTouched = true;
        GetDirection();
        AddSpellEffects();
        Destroy(projectilePrefab.gameObject);
    }

    private void DebugDraw(Vector2 point, bool isHit)
    {
        debugPosition = transform.position;
        if(isHit) Debug.DrawLine(debugPosition, point, Color.green, duration:10f, false);
        else Debug.DrawRay(debugPosition, debugDirection * _spell.Area, Color.red, duration:10f);
        Debug.Log("DebugDraw");
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
        foreach (var layerTarget in _spell.LayerTargets)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _spell.Area, layerTarget);
            DebugDraw(hit.point, hit.collider != null);
            if (hit.collider == null) return;
            if(!_detectedCollidersList.Contains(hit.collider)) _detectedCollidersList.Add(hit.collider);
            OnDetected?.Invoke(hit.collider);
            break;
        }
    }

    private void AddSpellEffects()
    {
        if (_detectedCollidersList.Count == 0) return;
        foreach (var collider in _detectedCollidersList)
        {
            var spellEffectHandler = collider.GetComponent<SpellEffectHandler>();
            OnAddSpellToHandler?.Invoke(_caster, spellEffectHandler, _spell);
            spellEffectHandler.AddSpell(_spell);
            Debug.Log("Один попался под спел");
        }
        
    }
}

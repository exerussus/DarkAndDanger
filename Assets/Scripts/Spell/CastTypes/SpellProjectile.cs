
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    private Spell _spell;
    private Vector3 _direction;
    private bool _isActivated;
    private bool _isTouched;
    private List<Collider2D> _detectedCollidersList;
    [SerializeField] private GameObject projectilePrefab;

    public SpellProjectile(Spell spell)
    {
        _spell = spell;
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
        if (collider.gameObject.CompareTag("HitBox") ||
            collider.gameObject.CompareTag("PhysicalObject") ||
            collider.gameObject.CompareTag("DestructibleObject"))
            return true;
        return false;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isTouched) return;
        if (IsTouchingObjects(collider))
        {
            _isTouched = true;
            GetDirection();
            AddSpellEffects();
            Destroy(projectilePrefab.gameObject);
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
        if (_detectedCollidersList.Count == 0) return;
        foreach (var collider in _detectedCollidersList)
        {
            var spellEffectHandler = collider.GetComponent<SpellEffectHandler>();
            spellEffectHandler.AddSpell(_spell);
        }
        
    }
}

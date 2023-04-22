
using System;
using UnityEngine;

public class ColliderObserver : MonoBehaviour
{
    [SerializeField] private Collider2D weaponCollider;
    

    public Action OnTouchPhysicalObject;
    public Action<Collider2D> OnTouchDestructibleObject;
    public Action<Collider2D> OnTouchEnemyWeapon;
    public Action<Collider2D> OnTouchHitBox;

    private void OnEnable()
    {
        DeactivateCollider();
    }

    public void ActivateCollider()
    {
        weaponCollider.enabled = true;
    }
    
    public void DeactivateCollider()
    {
        weaponCollider.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D touchedCollider)
    {
        if (touchedCollider.gameObject.CompareTag("PhysicalObject"))
        {
            OnTouchPhysicalObject?.Invoke();
        }
        
        if (touchedCollider.gameObject.CompareTag("Weapon"))
        {
            OnTouchEnemyWeapon?.Invoke(touchedCollider);
        }

        if (touchedCollider.gameObject.CompareTag("DestructibleObject"))
        {
            OnTouchDestructibleObject?.Invoke(touchedCollider);
        }
        else if (touchedCollider.gameObject.CompareTag("HitBox"))
        {
            OnTouchHitBox?.Invoke(touchedCollider);
        }
    }
}

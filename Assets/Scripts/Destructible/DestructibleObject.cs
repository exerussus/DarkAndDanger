
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{

    private bool _isAlive;
    [SerializeField] private float _healthMax;
    [SerializeField] private GameObject prefab;
    private CharacterResource _currentHealth;

    public void Start()
    {
        prefab = prefab == null ? GetComponent<GameObject>() : prefab;
        _currentHealth = new CharacterResource(_healthMax);
        
    }

    public void TakeDamage(PhysicalDamage damage)
    {
        _currentHealth.Value -= damage.Blunt + damage.Pierce + damage.Slash;
        _isAlive = _currentHealth.Value > 0;
        if(!_isAlive) Dead();
    }

    public void Dead()
    {
        Destroy(prefab);
    }

}

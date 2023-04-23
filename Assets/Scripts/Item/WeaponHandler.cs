
using System;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Weapon actuallyWeapon;
    [SerializeField] private PhysicalWeapon physicalWeapon;
    [SerializeField] private MagicalWeapon magicalWeapon;

    public PhysicalWeapon PhysicalWeapon => physicalWeapon;
    public MagicalWeapon MagicalWeapon => magicalWeapon;
    public Weapon ActuallyWeapon => actuallyWeapon;
    public GameObject WeaponGameObject { get; private set; }
    private float _putOnTime;

    public Action OnBeforeWeaponChange;
    public Action OnWeaponChange;

    private void ChangeWeapon(Weapon newWeapon)
    {
        OnBeforeWeaponChange?.Invoke();
        Destroy(WeaponGameObject);
        actuallyWeapon = newWeapon;
        _putOnTime = actuallyWeapon.Item.Weight + Time.fixedTime;
        Tick.OnFixedUpdate += SetNewWeapon;
    }

    private void SetNewWeapon()
    {
        if (_putOnTime > Time.fixedTime) return;
        Tick.OnFixedUpdate -= SetNewWeapon;
        CreateNewWeapon();
        OnWeaponChange?.Invoke();
    }
    
    public void SetPhysicalWeapon(PhysicalWeapon physicalWeapon)
    {
        magicalWeapon = null;
        this.physicalWeapon = physicalWeapon;
        ChangeWeapon(PhysicalWeapon);
    }
    
    public void SetMagicalWeapon(MagicalWeapon magicalWeapon)
    {
        physicalWeapon = null;
        this.magicalWeapon = magicalWeapon;
        ChangeWeapon(MagicalWeapon);
    }
    
    private void CreateNewWeapon()
    {
        WeaponGameObject = Instantiate(actuallyWeapon.Prefab, parent: gameObject.transform);
        WeaponGameObject.transform.localPosition = Vector3.zero;
    }
}

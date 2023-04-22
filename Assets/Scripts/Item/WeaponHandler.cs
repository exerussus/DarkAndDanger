
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

    public Action OnWeaponChange;
    
    public void Start()
    {
        if (physicalWeapon != null) SetPhysicalWeapon(physicalWeapon);
        else if(magicalWeapon != null) SetMagicalWeapon(magicalWeapon);
    }

    private void ChangeWeapon(Weapon newWeapon)
    {
        Destroy(WeaponGameObject);
        actuallyWeapon = newWeapon;
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

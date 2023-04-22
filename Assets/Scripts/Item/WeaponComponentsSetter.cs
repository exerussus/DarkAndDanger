
using UnityEngine;

public class WeaponComponentsSetter : MonoBehaviour
{
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private Character character;
    [SerializeField] private KeyboardController keyboardController;
    [SerializeField] private PlayerResourceController playerResourceController;
    public PhysicalAttack PhysicalAttack { get; private set; }

    private void OnEnable()
    {
        weaponHandler.OnWeaponChange += SetComponents;
        weaponHandler.OnBeforeWeaponChange += UnsubscribeComponents;
    }

    private void OnDisable()
    {
        weaponHandler.OnWeaponChange -= SetComponents;
        weaponHandler.OnBeforeWeaponChange -= UnsubscribeComponents;
    }
    
    private void SetComponents()
    {   
        if(weaponHandler.PhysicalWeapon != null)
        {
            PhysicalAttack = weaponHandler.WeaponGameObject.GetComponent<PhysicalAttack>();
            PhysicalAttack.SetKeyboardController(keyboardController);
            PhysicalAttack.SetWeapon(weaponHandler.PhysicalWeapon);
            var attackController = weaponHandler.WeaponGameObject.GetComponent<AttackController>();
            attackController.SetCharacter(character);
            playerResourceController.SetPhysicalAttack(PhysicalAttack);
        }
        else if (weaponHandler.MagicalWeapon != null)
        {
            
        }
    }

    private void UnsubscribeComponents()
    {
        PhysicalAttack.UnsubscribeKeyboardController();
    }
}


using UnityEngine;

public class WeaponComponentsSetter : MonoBehaviour
{
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private SpellEffectHandler spellEffectHandler;
    [SerializeField] private Character character;
    [SerializeField] private KeyboardController keyboardController;
    [SerializeField] private PlayerResourceController playerResourceController;
    [SerializeField] private MagicSpellBar magicSpellBar;
    public PhysicalAttack PhysicalAttack { get; private set; }
    public MagicalAttack MagicalAttack { get; private set; }

    private void OnEnable()
    {
        weaponHandler.OnEndWeaponChange += SetComponents;
        weaponHandler.OnBeforeWeaponChange += UnsubscribeComponents;
    }

    private void OnDisable()
    {
        weaponHandler.OnEndWeaponChange -= SetComponents;
        weaponHandler.OnBeforeWeaponChange -= UnsubscribeComponents;
    }
    
    private void SetComponents()
    {   
        if(weaponHandler.PhysicalWeapon != null)
        {
            MagicalAttack = null;
            PhysicalAttack = weaponHandler.WeaponGameObject.GetComponent<PhysicalAttack>();
            PhysicalAttack.SetKeyboardController(keyboardController);
            PhysicalAttack.SetWeapon(weaponHandler.PhysicalWeapon);
            var attackController = weaponHandler.WeaponGameObject.GetComponent<AttackController>();
            attackController.SetCharacter(character);
            playerResourceController.SetPhysicalAttack(PhysicalAttack);
        }
        if (weaponHandler.MagicalWeapon != null)
        {
            PhysicalAttack = null;
            MagicalAttack = weaponHandler.WeaponGameObject.GetComponent<MagicalAttack>();
            MagicalAttack.SetKeyboardController(keyboardController);
            MagicalAttack.SetWeapon(weaponHandler.MagicalWeapon);
            MagicalAttack.SetCharacter(character);
            MagicalAttack.SetCasterSpellEffectHandler(spellEffectHandler);
            playerResourceController.SetMagicalAttack(MagicalAttack);
            magicSpellBar.SetMagicalAttack(MagicalAttack);
        }
    }

    private void UnsubscribeComponents()
    {
        if(PhysicalAttack != null) PhysicalAttack.UnsubscribeKeyboardController();
        if(MagicalAttack != null) MagicalAttack.UnsubscribeKeyboardController();
    }
}

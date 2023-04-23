
using UnityEngine;
using UnityEngine.UI;

public class MagicSpellBar : MonoBehaviour
{
    [SerializeField] private Image spriteRenderer;
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private Sprite defaultSprite;
    private MagicalAttack magicalAttack;
    
    private void OnEnable()
    {
        weaponHandler.OnBeforeWeaponChange += UnsubscribeMagicalAttack;
    }
    private void OnDisable()
    {
        weaponHandler.OnBeforeWeaponChange -= UnsubscribeMagicalAttack;
    }

    public void SetMagicalAttack(MagicalAttack magicalAttack)
    {
        if (magicalAttack != null) UnsubscribeMagicalAttack();
        this.magicalAttack = magicalAttack;
        magicalAttack.OnSwitchSpell += SetSprite;
        SetSprite();
    }

    private void SetSprite()
    {
        spriteRenderer.sprite = magicalAttack.ActuallySpell.Icon;
    }

    private void ResetSprite()
    {
        spriteRenderer.sprite = defaultSprite;
    }
    
    private void UnsubscribeMagicalAttack()
    {
        ResetSprite();
        if(magicalAttack != null) magicalAttack.OnSwitchSpell -= SetSprite;
    }

}

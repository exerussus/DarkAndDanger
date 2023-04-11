
using UnityEngine;


public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private Attack weaponAttack;
    [SerializeField] private Character character;

    private const float StandardAttackSpeed = 100f;
    private const float MinAnimationSpeed = 1f;
    private const float MaxAnimationSpeed = 3f;
    private const float StartingDamageDenominator = 90f;
    private const float EndingDamageDenominator = 30f;

    private void OnEnable()
    {
        weaponAttack.OnThirdAttack += ThirdAttack;
        weaponAttack.OnFirstAttack += FirstAttack;
        weaponAttack.OnSecondAttack += SecondAttack;
        weaponAttack.OnAttackEnd += Idle;
        weaponAttack.OnStopParry += StopBlock;
        weaponAttack.OnStartParry += Parry;
        weaponAttack.OnStartingWeakDamage += SetStartingWeakDamage;
        weaponAttack.OnFullDamage += SetFullDamage;
        weaponAttack.OnEndingWeakDamage += SetEndingWeakDamage;
        weaponAttack.OnNoDamage += SetNoDamage;
    }

    private void OnDisable()
    {
        weaponAttack.OnThirdAttack -= ThirdAttack;
        weaponAttack.OnFirstAttack -= FirstAttack;
        weaponAttack.OnSecondAttack -= SecondAttack;
        weaponAttack.OnAttackEnd -= Idle;
        weaponAttack.OnStopParry -= StopBlock;
        weaponAttack.OnStartParry -= Parry;
        weaponAttack.OnStartingWeakDamage -= SetStartingWeakDamage;
        weaponAttack.OnFullDamage -= SetFullDamage;
        weaponAttack.OnEndingWeakDamage -= SetEndingWeakDamage;
        weaponAttack.OnNoDamage -= SetNoDamage;
    }

    private void SetNoDamage()
    {
        weaponAttack.Damage.Zero();
    }

    private void SetEndingWeakDamage()
    {
        weaponAttack.Damage = GetDamageWithType().GetReducedDamage(denominator:EndingDamageDenominator);
    }

    private void SetFullDamage()
    {
        weaponAttack.Damage = GetDamageWithType();
    }
    
    private void SetStartingWeakDamage()
    {
        weaponAttack.Damage = GetDamageWithType().GetReducedDamage(denominator:StartingDamageDenominator);
    }

    private PhysicalDamage GetDamageWithType()
    {
         var damage = new PhysicalDamage(
            character.Parameter.bluntDamage, 
            character.Parameter.pierceDamage, 
            character.Parameter.slashDamage);
        
        switch (weaponAttack.attackType)
        {
            case AttackType.Pierce: return damage.GetPierceDamage();
            case AttackType.BluntSlash: return damage.GetBluntSlashDamage();
            case AttackType.BluntPierce: return damage.GetBluntPierceDamage();
        }

        return null;
    }
    
    private void Parry() {weaponAnimator.Play("Blocking"); }

    private void ThirdAttack()
    {
        weaponAttack.attackType = weaponAttack.WeaponPattern.ThirdAttack;
        weaponAnimator.speed = GetAttackSpeed();
        weaponAnimator.Play("ThirdAttack");
    }

    private void FirstAttack() 
    { 
        weaponAttack.attackType = weaponAttack.WeaponPattern.FirstAttack;
        weaponAnimator.speed = GetAttackSpeed();
        weaponAnimator.Play("FirstAttack"); 
    }

    private void SecondAttack() 
    { 
        weaponAttack.attackType = weaponAttack.WeaponPattern.SecondAttack;
        weaponAnimator.speed = GetAttackSpeed();
        weaponAnimator.Play("SecondAttack"); 
    }

    private void Idle() 
    {
        weaponAnimator.speed = MinAnimationSpeed;
        weaponAnimator.Play("Idle"); 
    }
    
    private void StopBlock() 
    { 
        weaponAnimator.speed = MinAnimationSpeed;
        weaponAnimator.Play("StopBlocking"); 
    }

    private float GetAttackSpeed()
    {
        var weightMultiply = 0.5f;
        var speed = (MinAnimationSpeed / StandardAttackSpeed * character.Parameter.attackSpeed)
                    - (MinAnimationSpeed * weaponAttack.Weapon.Weight * weightMultiply);

        if (speed < MinAnimationSpeed) return MinAnimationSpeed;
        if (speed > MaxAnimationSpeed) return MaxAnimationSpeed;
        return speed;
    }
}


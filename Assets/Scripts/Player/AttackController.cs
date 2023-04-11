
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

    private const string Blocking = "Blocking";
    private const string ThirdAttack = "ThirdAttack";
    private const string FirstAttack = "FirstAttack";
    private const string SecondAttack = "SecondAttack";
    private const string Idle = "Idle";
    private const string StopBlocking = "StopBlocking";
    
    private void OnEnable()
    {
        weaponAttack.OnThirdAttack += PlayThirdAttack;
        weaponAttack.OnFirstAttack += PlayFirstAttack;
        weaponAttack.OnSecondAttack += PlaySecondAttack;
        weaponAttack.OnAttackEnd += PlayIdle;
        weaponAttack.OnStopParry += PlayStopBlock;
        weaponAttack.OnStartParry += Parry;
        weaponAttack.OnStartingWeakDamage += SetStartingWeakDamage;
        weaponAttack.OnFullDamage += SetFullDamage;
        weaponAttack.OnEndingWeakDamage += SetEndingWeakDamage;
        weaponAttack.OnNoDamage += SetNoDamage;
    }

    private void OnDisable()
    {
        weaponAttack.OnThirdAttack -= PlayThirdAttack;
        weaponAttack.OnFirstAttack -= PlayFirstAttack;
        weaponAttack.OnSecondAttack -= PlaySecondAttack;
        weaponAttack.OnAttackEnd -= PlayIdle;
        weaponAttack.OnStopParry -= PlayStopBlock;
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
    
    private void Parry() {weaponAnimator.Play(Blocking); }

    private void PlayThirdAttack()
    {
        weaponAttack.attackType = weaponAttack.WeaponPattern.ThirdAttack;
        weaponAnimator.speed = GetAttackSpeed();
        weaponAnimator.Play(ThirdAttack);
    }

    private void PlayFirstAttack() 
    { 
        weaponAttack.attackType = weaponAttack.WeaponPattern.FirstAttack;
        weaponAnimator.speed = GetAttackSpeed();
        weaponAnimator.Play(FirstAttack); 
    }

    private void PlaySecondAttack() 
    { 
        weaponAttack.attackType = weaponAttack.WeaponPattern.SecondAttack;
        weaponAnimator.speed = GetAttackSpeed();
        weaponAnimator.Play(SecondAttack); 
    }

    private void PlayIdle() 
    {
        weaponAnimator.speed = MinAnimationSpeed;
        weaponAnimator.Play(Idle); 
    }
    
    private void PlayStopBlock() 
    { 
        weaponAnimator.speed = MinAnimationSpeed;
        weaponAnimator.Play(StopBlocking); 
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


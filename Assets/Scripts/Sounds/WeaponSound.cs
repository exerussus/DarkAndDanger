
using UnityEngine;

public class WeaponSound : PlaySound
{
    [SerializeField] private Attack attack;

    private void OnEnable()
    {
        attack.OnTouchEnemy += Hit;
        attack.OnTouchPhysicalObject += WallTouch;
        attack.OnTouchDestructibleObject += DestructibleObjectTouch;
        attack.OnStartingWeakDamage += Attack;
        attack.OnGetHitInParry += Parry;
    }

    private void OnDisable()
    {
        attack.OnTouchEnemy -= Hit;
        attack.OnTouchPhysicalObject -= WallTouch;
        attack.OnTouchDestructibleObject -= DestructibleObjectTouch;
        attack.OnStartingWeakDamage -= Attack;
        attack.OnGetHitInParry -= Parry;
    }
    
    private void Parry()
    {
        PlayAudio(SoundSO.SoundName.Parring);
    }
    
    private void Attack()
    {
        PlayAudio(SoundSO.SoundName.TwoHandsSwordAttack);
    }
    
    private void DestructibleObjectTouch()
    {
        PlayAudio(SoundSO.SoundName.DestructibleObjectTouch);
    }
    
    private void WallTouch()
    {
        PlayAudio(SoundSO.SoundName.WallTouch);
    }

    private void Hit()
    {
        PlayAudio(SoundSO.SoundName.HitTouch);
    }
    
}

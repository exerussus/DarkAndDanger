
using UnityEngine;

public class WeaponSound : PlaySound
{
    [SerializeField] private PhysicalAttack attack;

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
        PlayAudioFromList(SoundSO.SoundName.Parring);
    }
    
    private void Attack()
    {
        PlayAudioFromList(SoundSO.SoundName.TwoHandsSwordAttack);
    }
    
    private void DestructibleObjectTouch()
    {
        PlayAudioFromList(SoundSO.SoundName.DestructibleObjectTouch);
    }
    
    private void WallTouch()
    {
        PlayAudioFromList(SoundSO.SoundName.WallTouch);
    }

    private void Hit()
    {
        PlayAudioFromList(SoundSO.SoundName.HitTouch);
    }
    
}

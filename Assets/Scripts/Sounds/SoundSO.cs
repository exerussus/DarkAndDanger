

using System;
using UnityEngine;

[Serializable]
public class SoundSO
{
    public SoundName Name;
    public AudioClip Sound;
    
    
    
    public enum SoundName
    {
        OneHandSwordAttack,
        TwoHandsSwordAttack,
        DaggerAttack,
        WallTouch,
        HitTouch,
        PhysicalObjectTouch,
        DestructibleObjectTouch,
        Interaction,
        Moving,
        Crouching,
        Sprinting,
        Dying,
        Parring
    }
}


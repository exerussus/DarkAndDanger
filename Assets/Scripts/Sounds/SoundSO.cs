
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundSO
{
    public SoundName Name;
    public List<AudioClip> Sounds;

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


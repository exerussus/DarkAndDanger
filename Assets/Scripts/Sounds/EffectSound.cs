
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectSoundPack", menuName = "Sound/EffectSoundPack", order = 51)]
public class EffectSound : ScriptableObject
{
    [SerializeField] private List<AudioClip> sounds;

    public AudioClip GetRandom()
    {
        return sounds[Random.Range(0, sounds.Count)];
    }
}

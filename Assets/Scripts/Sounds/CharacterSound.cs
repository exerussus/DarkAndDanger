
using UnityEngine;

public class CharacterSound : PlaySound
{
    [SerializeField] private Character character;

    private void OnEnable()
    {
        character.OnDead += Dead;
    }

    private void OnDisable()
    {
        character.OnDead -= Dead;
    }

    private void Dead()
    {
        PlayAudio(SoundSO.SoundName.Dying);
    }
}

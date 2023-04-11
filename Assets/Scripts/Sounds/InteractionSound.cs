

using System;
using UnityEngine;

public class InteractionSound : PlaySound
{
    [SerializeField] private Interaction interaction;

    private void OnEnable()
    {
        interaction.OnInteraction += Interact;
    }

    private void OnDisable()
    {
        interaction.OnInteraction -= Interact;
    }


    private void Interact()
    {
        PlayAudio(SoundSO.SoundName.Interaction);
    }
}

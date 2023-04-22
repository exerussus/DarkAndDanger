
using UnityEngine;

public class MovementSound : PlaySound
{
    [SerializeField] private StepObserver stepObserver;
    [SerializeField] private PlayerMovement playerMovement;
    private void OnEnable()
    {
        stepObserver.OnStep += PlayStepSound;
    }

    private void OnDisable()
    {
        stepObserver.OnStep -= PlayStepSound; 
    }

    private void PlayStepSound()
    {
        if (playerMovement.IsSprinting)
            PlayAudioFromList(SoundSO.SoundName.Sprinting);
        else if(playerMovement.IsCrouching)
            PlayAudioFromList(SoundSO.SoundName.Crouching);
        else PlayAudioFromList(SoundSO.SoundName.Moving);
            
    }
}

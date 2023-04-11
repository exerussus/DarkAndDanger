
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
            PlayAudio(SoundSO.SoundName.Sprinting);
        else if(playerMovement.IsCrouching)
            PlayAudio(SoundSO.SoundName.Crouching);
        else PlayAudio(SoundSO.SoundName.Moving);
            
    }
}


using UnityEngine;

public class MovementSound : PlaySound
{
    [SerializeField] private StepObserver stepObserver;

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
        PlayAudio(SoundSO.SoundName.Moving);
    }
}

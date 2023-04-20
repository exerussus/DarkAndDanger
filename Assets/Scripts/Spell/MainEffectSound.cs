
using UnityEngine;

public class MainEffectSound : MonoBehaviour
{
    [SerializeField] private EffectSound sounds;
    [SerializeField] private AudioSource audioSource;

    private void PlaySound()
    {
        audioSource.PlayOneShot(sounds.GetRandom());
    }
    
    
    private void StopPlay()
    {
        audioSource.Stop();
    }
}


using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] protected SoundsSO sounds;
    [SerializeField] protected AudioSource audioSource;
    
    protected void PlayAudio(SoundSO.SoundName soundName)
    {
        AudioClip audioClip = GetAudioClip(soundName);
        audioSource.PlayOneShot(audioClip);
    }
    
    private AudioClip GetAudioClip(SoundSO.SoundName soundName)
    {
        foreach (var nameAndSound in sounds.SoundList)
        {
            if (nameAndSound.Name == soundName) return nameAndSound.Sound;
        }

        return null;
    }
}

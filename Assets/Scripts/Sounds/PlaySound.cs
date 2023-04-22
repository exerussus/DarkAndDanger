
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] protected SoundsSO sounds;
    [SerializeField] protected AudioSource audioSource;
    private int _lastSoundIndex;


    protected void PlaySingleShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    
    protected void PlayAudioFromList(SoundSO.SoundName soundName)
    {
        AudioClip audioClip = GetAudioClip(soundName);
        audioSource.PlayOneShot(audioClip);
    }
    
    private AudioClip GetAudioClip(SoundSO.SoundName soundName)
    {
        foreach (var nameAndSound in sounds.SoundList)
        {
            if (nameAndSound.Name == soundName)
            {
                return nameAndSound.Sounds[GetNextClip(nameAndSound)];
            }
        }
        return null;
    }

    private int GetNextClip(SoundSO sounds)
    {
        var listCount = sounds.Sounds.Count;
        if (listCount == 1) return 0;
        
        _lastSoundIndex = GetNex(listCount);
        return _lastSoundIndex;
    }

    private int GetNex(int listCount)
    {
        var newIndex = _lastSoundIndex + 1;
        if (newIndex <= listCount - 1) return newIndex;
        return 0;
    }
}

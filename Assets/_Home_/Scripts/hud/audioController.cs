using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using Sirenix.OdinInspector;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public HashSet<AudioClip> playingSounds = new HashSet<AudioClip>();
    private AudioSourceExtended beepSource;

    [Button]
    public async void PlaySound(AudioClip audioClip)
    {
        if (playingSounds.Contains(audioClip))
        {
            Debug.Log("Audio is already playing");
            return;
        }
        playingSounds.Add(audioClip);
        AudioSourceExtended source = (await AudioManager.GetInstance()).PlaySound(audioClip);
        source.onRelease += () => OnSoundStop(audioClip);
    }

    private void OnSoundStop(AudioClip audioClip)
    {
        Debug.Log("Sound " + audioClip + " ended!");
        playingSounds.Remove(audioClip);
    }
}

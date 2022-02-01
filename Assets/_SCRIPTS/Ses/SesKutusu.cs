using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKutusu : MonoBehaviour
{
    public static SesKutusu instance;
    AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = KAYIT.GetSesSeviyesi();
    }
    public void Play(NameOfAudioClip name) { audioSource.PlayOneShot(GetASoundClip.Hangi(name)); }
    public void PlayIfDontPlay(NameOfAudioClip name) { if(!audioSource.isPlaying) audioSource.PlayOneShot(GetASoundClip.Hangi(name)); }
    public void Stop() { audioSource.Stop(); }
    public void SetVolume (float volume) { audioSource.volume = volume; }
}


using System;
using System.Collections.Generic;
using UnityEngine;

public class SesKutusu : MonoBehaviour
{
    public static SesKutusu instance;
    AudioSource _audioSource;
    readonly Dictionary<NameOfAudioClip, AudioClip> _cache = new();

    void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = KAYIT.GetSesSeviyesi();

        foreach (NameOfAudioClip clip in Enum.GetValues(typeof(NameOfAudioClip)))
        {
            AudioClip loaded = GetASoundClip.Hangi(clip);
            if (loaded != null) _cache[clip] = loaded;
        }
    }

    public void Play(NameOfAudioClip name)
    {
        if (_cache.TryGetValue(name, out AudioClip clip)) _audioSource.PlayOneShot(clip);
    }

    public void PlayIfDontPlay(NameOfAudioClip name)
    {
        if (!_audioSource.isPlaying) Play(name);
    }

    public void Stop() => _audioSource.Stop();

    public void SetVolume(float volume) => _audioSource.volume = volume;
}

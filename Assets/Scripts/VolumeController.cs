using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider music;
    [SerializeField] Slider sfx;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] randomSounds;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";
    
    public void PlayRandomSound()
    {
        if (randomSounds.Length == 0) return;

        int i = UnityEngine.Random.Range(0, randomSounds.Length);
        sfxSource.PlayOneShot(randomSounds[i]);
    }

    private void Start()
    {
        musicSource.loop = true;
        musicSource.Play();

        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 1f);

        music.value = musicVolume;
        sfx.value = sfxVolume;

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        music.onValueChanged.AddListener(SetMusicVolume);
        sfx.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(sfxVolume, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat(SFXKey, sfxVolume);
    }

    private void SetMusicVolume(float musicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(musicVolume, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat(MusicKey, musicVolume);
    }
}

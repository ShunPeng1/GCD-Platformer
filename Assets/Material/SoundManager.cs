using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource worldMusicSource;
    [SerializeField] private AudioSource worldSoundEffect;
    
    [SerializeField] private List<AudioClip> gameplayClips = new();
    [SerializeField] private AudioClip gameOverClip;
    
    void Start()
    {
        int index = Random.Range(0, gameplayClips.Count);
        worldMusicSource.clip = gameplayClips[index];
        worldMusicSource.Play();
    }

    public void OnGameOver()
    {
        worldMusicSource.Stop();
        worldSoundEffect.PlayOneShot(gameOverClip);
    }

    public void PlaySoundEffect(AudioClip soundEffectClip)
    {
        worldSoundEffect.PlayOneShot(soundEffectClip);
    }
    
}

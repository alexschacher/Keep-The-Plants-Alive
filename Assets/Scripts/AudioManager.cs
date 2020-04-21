using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    [SerializeField]
    private AudioSource plantAudioSource;
    [SerializeField]
    private AudioSource burnAudioSource;
    [SerializeField]
    private AudioSource dieAudioSource;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }

        burnAudioSource.clip = (AudioClip)Resources.Load("Audio/Burn1", typeof(AudioClip));
        plantAudioSource.clip = (AudioClip)Resources.Load("Audio/Plant", typeof(AudioClip));
        dieAudioSource.clip = (AudioClip)Resources.Load("Audio/Die", typeof(AudioClip));
    }

    public static void PlayBurn()
    {
        _instance.burnAudioSource.Play();
    }

    public static void PlayPlant()
    {
        _instance.plantAudioSource.Play();
    }

    public static void PlayDie()
    {
        _instance.dieAudioSource.Play();
    }
}

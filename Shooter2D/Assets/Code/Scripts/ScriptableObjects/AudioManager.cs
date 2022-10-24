using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
[CreateAssetMenu(fileName = "AudioManager", menuName = "Shooter2D/AudioManager", order = 0)]
public class AudioManager : ScriptableObject
{

    [SerializeField] public SoundEntry[] SoundList;

    public void PlaySound(SoundEntry audio, AudioSource source)
    {
        source.PlayOneShot(audio.clip);
    }

    public void PlaySound(int id, AudioSource source)
    {
        source.PlayOneShot(SoundList[id].clip);
    }

    [Serializable]
    public struct SoundEntry
    {
        public string identifier;
        public AudioClip clip;
    }

}

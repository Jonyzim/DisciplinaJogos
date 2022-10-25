using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using NaughtyAttributes;

namespace MyAudio
{

    [Serializable]
    [CreateAssetMenu(fileName = "AudioManager", menuName = "Shooter2D/AudioManager", order = 0)]
    public class AudioManager : ScriptableObject
    {

        [SerializeField] public SoundEntry[] SoundList;


        [SerializeField] private AudioSource _defaultSource;

        [HideInInspector]
        public AudioSource DefaultSource { get => _defaultSource; set => _defaultSource = value; }


        public void PlaySound(SoundEntry audio, AudioSource source)
        {
            source.PlayOneShot(audio.clip);
        }

        public void PlaySound(int id, AudioSource source)
        {
            source.PlayOneShot(SoundList[id].clip);
        }

        public void PlaySound(int id)
        {
            if (_defaultSource == null)
                Debug.LogError("No default audio source set!");
            _defaultSource.PlayOneShot(SoundList[id].clip);
        }

        public void PlayLoop(int id)
        {
            if (_defaultSource == null)
                Debug.LogError("No default audio source set!");
            _defaultSource.clip = SoundList[id].clip;
            _defaultSource.loop = true;
            _defaultSource.Play();
        }

        [Serializable]
        public struct SoundEntry
        {
            public string identifier;
            public AudioClip clip;
        }

    }
}

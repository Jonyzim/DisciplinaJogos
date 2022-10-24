using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class DefaultAudioSource : MonoBehaviour
    {
        public AudioManager audioManager;

        private void Awake()
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioManager.DefaultSource = source;
        }

    }
}
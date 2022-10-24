using System;
using UnityEngine;

[Serializable]
public class Sound
{
    [SerializeField] public int Id;

    [SerializeField] public Audio.AudioManager Manager;

    public void Play(AudioSource source)
    {
        Manager.PlaySound(Id, source);
    }

    public void Play()
    {
        Manager.PlaySound(Id);
    }
}

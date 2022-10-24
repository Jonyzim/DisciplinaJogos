using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public int Id;

    public Audio.AudioManager Manager { set => _manager = value; }

    private Audio.AudioManager _manager;

    public void Play(AudioSource source)
    {
        _manager.PlaySound(Id, source);
    }

    public void Play()
    {
        _manager.PlaySound(Id);
    }
}

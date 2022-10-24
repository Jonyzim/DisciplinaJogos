using UnityEngine;

public class Sound : MonoBehaviour
{
    public int Id;

    public AudioManager Manager { set => _manager = value; }

    private AudioManager _manager;

    public void Play(AudioSource source)
    {
        _manager.PlaySound(Id, source);
    }
}

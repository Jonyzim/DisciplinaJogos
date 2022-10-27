/* !DEPRECATED
[Serializable]
public class Sound
{
    [SerializeField] public int Id;

    [SerializeField] public MyAudio.AudioManager Manager;

    public void Play(AudioSource source)
    {
        Manager.PlaySound(Id, source);
    }

    public void Play()
    {
        Manager.PlaySound(Id);
    }

    public void PlayLoop()
    {
        Manager.PlayLoop(Id);
    }
}
*/

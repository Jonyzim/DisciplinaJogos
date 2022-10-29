/* !DEPRECATED
namespace MyAudio
{
    public class DefaultAudioSource : MonoBehaviour
    {
        public AudioManager AudioManager;
        public bool Loop;
        public AudioMixerGroup MixerGroup;

        private void Awake()
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = MixerGroup;
            AudioManager.DefaultSource = source;

        }

    }
}
*/


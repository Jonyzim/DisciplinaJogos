using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.ScriptableObjects.Audio
{
    public class PlayBGM : MonoBehaviour
    {
        [FormerlySerializedAs("BgmEvent")] [SerializeField] private EventReference bgmEvent;
        private EventInstance _bgmInstance;

        private void Start()
        {
            _bgmInstance = RuntimeManager.CreateInstance(bgmEvent);
            _bgmInstance.start();
            RuntimeManager.AttachInstanceToGameObject(_bgmInstance, transform);
        }
    }
}
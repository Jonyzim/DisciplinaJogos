using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Temporary Script
public class PlayBGM : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference BgmEvent;
    private FMOD.Studio.EventInstance bgmInstance;

    private void Start()
    {
        bgmInstance = FMODUnity.RuntimeManager.CreateInstance(BgmEvent);
        bgmInstance.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgmInstance, transform);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


// Temporary Script
public class PlayBGM : MonoBehaviour
{
    [SerializeField] private EventReference BgmEvent;
    private FMOD.Studio.EventInstance bgmInstance;

    private void Start()
    {
        bgmInstance = RuntimeManager.CreateInstance(BgmEvent);
        bgmInstance.start();
        RuntimeManager.AttachInstanceToGameObject(bgmInstance, GetComponent<Transform>());
    }

}

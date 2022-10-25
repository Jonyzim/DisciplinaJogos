using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Temporary Script
public class PlayBGM : MonoBehaviour
{
    [SerializeField] private Sound BGMToPlay;

    private void Start()
    {
        BGMToPlay.PlayLoop();
    }

}

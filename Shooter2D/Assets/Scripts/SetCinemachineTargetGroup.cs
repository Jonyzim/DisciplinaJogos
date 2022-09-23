using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SetCinemachineTargetGroup : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CinemachineTargetGroup group;
    static private SetCinemachineTargetGroup instance;
    static public SetCinemachineTargetGroup Instance => instance;

    void Awake(){
        instance = this;
    }

    public void AddCharacter(Character character){
        if(vcam.m_Follow == null){
            vcam.m_Follow = character.gameObject.transform;
        }
        group.AddMember(character.gameObject.transform, 1, 1);
    }
}

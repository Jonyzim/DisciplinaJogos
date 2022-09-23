using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SetCinemachineTargetGroup : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [SerializeField] private CinemachineTargetGroup _group;
    static private SetCinemachineTargetGroup instance;
    static public SetCinemachineTargetGroup Instance => instance;

    void Awake()
    {
        instance = this;
    }

    public void AddCharacter(Character character)
    {
        if (_vcam.m_Follow == null)
        {
            _vcam.m_Follow = character.gameObject.transform;
        }
        else
        {
            GameEvents.s_instance.isMultiplayer = true;
        }
        _group.AddMember(character.gameObject.transform, 1, 1);
    }
}

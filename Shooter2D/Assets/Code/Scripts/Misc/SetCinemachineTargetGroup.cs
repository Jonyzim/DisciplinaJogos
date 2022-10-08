using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SetCinemachineTargetGroup : MonoBehaviour
{
    public static SetCinemachineTargetGroup s_Instance => s_instance;
    private static SetCinemachineTargetGroup s_instance;
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [SerializeField] private CinemachineTargetGroup _group;

    void Awake()
    {
        s_instance = this;
    }

    public void AddCharacter(Character character)
    {
        if (_vcam.m_Follow == null)
        {
            _vcam.m_Follow = character.gameObject.transform;
        }
        else
        {
            GameEvents.Instance.IsMultiplayer = true;
        }
        _group.AddMember(character.gameObject.transform, 1, 1);
    }
}

using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Misc
{
    public class SetCinemachineTargetGroup : MonoBehaviour
    {
        [FormerlySerializedAs("_vcam")] [SerializeField] private CinemachineVirtualCamera vcam;
        [FormerlySerializedAs("_group")] [SerializeField] private CinemachineTargetGroup group;
        public static SetCinemachineTargetGroup SInstance { get; private set; }

        private void Awake()
        {
            SInstance = this;
        }

        public void AddCharacter(Character.Character character)
        {
            if (vcam.m_Follow == null)
                vcam.m_Follow = character.gameObject.transform;
            else
                GameEvents.Instance.isMultiplayer = true;
            group.AddMember(character.gameObject.transform, 1, 1);
        }
    }
}
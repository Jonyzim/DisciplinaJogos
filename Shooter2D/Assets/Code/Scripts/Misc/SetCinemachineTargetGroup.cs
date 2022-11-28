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

        public void AddCharacter(Character character)
        {
            if (vcam.m_Follow == null)
            {
                vcam.m_Follow = character.transform;
                character.OnDeath += ChangeMainCharacter;
            }
                
            else
                GameEvents.Instance.isMultiplayer = true;
            
            group.AddMember(character.transform, 1, 1);
            character.OnDeath += RemoveWeight;
            character.OnRevive += ReturnWeight;

        }

        private void ReturnWeight(Character character)
        {
            var i = group.FindMember(character.transform);
            group.m_Targets[i].weight = 1;
        }
        private void RemoveWeight(Character character)
        {
            var i = group.FindMember(character.transform);
            group.m_Targets[i].weight = 0;
        }

        private void ChangeMainCharacter(Character character)
        {
            foreach (var playerController in PlayerController.SActivePlayers)
            {
                if (!playerController.Pawn.isAlive) continue;


                vcam.m_Follow = playerController.Pawn.gameObject.transform;
                character.OnDeath -= ChangeMainCharacter;
                playerController.Pawn.OnDeath += ChangeMainCharacter;
                return;
            }
            
        }
    }
}
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace MWP.Misc
{
    public class CinemachineFollowMouse : MonoBehaviour
    {
        [FormerlySerializedAs("CameraOffset")] public CinemachineCameraOffset cameraOffset;
        [FormerlySerializedAs("TargetGroup")] public CinemachineTargetGroup targetGroup;

        [FormerlySerializedAs("OffsetMultiplier")] public float offsetMultiplier;
        [FormerlySerializedAs("OffsetSpeed")] public float offsetSpeed;
        [FormerlySerializedAs("MaxOffsetAmount")] public Vector2 maxOffsetAmount;
        private Camera _cam;
        private Vector2 _offset;

        // Start is called before the first frame update
        private void Start()
        {
            _cam = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameEvents.Instance.isMultiplayer)
                _offset = targetGroup.gameObject.transform.position - _cam.gameObject.transform.position;
            //TODO: Add offset for singleplayer controller aiming
            else
                _offset =
                    (_cam.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - new Vector3(0.5f, 0.5f, 0f)) * 2;

            if (PlayerController.SActivePlayers[0] == null) return;
            _offset = Vector2.Min(_offset, maxOffsetAmount);
            _offset = Vector2.Max(_offset, -maxOffsetAmount);
            cameraOffset.m_Offset = Vector3.Lerp(cameraOffset.m_Offset, _offset * offsetMultiplier,
                Time.deltaTime * offsetSpeed);
            ;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CinemachineFollowMouse : MonoBehaviour
{
    public CinemachineCameraOffset CameraOffset;
    public CinemachineTargetGroup TargetGroup;

    public float OffsetMultiplier;
    public float OffsetSpeed;
    public Vector2 MaxOffsetAmount;
    private Camera _cam;
    private Vector2 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

        if (GameEvents.s_Instance.IsMultiplayer)
        {
            _offset = TargetGroup.gameObject.transform.position - _cam.gameObject.transform.position;
        }
        //TODO: Add offset for singleplayer controller aiming
        else
        {
            _offset = (_cam.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - (new Vector3(0.5f, 0.5f, 0f))) * 2;
        }

        _offset = Vector2.Min(_offset, MaxOffsetAmount);
        _offset = Vector2.Max(_offset, -MaxOffsetAmount);

        CameraOffset.m_Offset = Vector3.Lerp(CameraOffset.m_Offset, _offset * OffsetMultiplier, Time.deltaTime * OffsetSpeed); ;
    }
}

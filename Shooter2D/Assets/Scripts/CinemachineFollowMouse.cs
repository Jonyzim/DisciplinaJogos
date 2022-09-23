using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CinemachineFollowMouse : MonoBehaviour
{
    public CinemachineCameraOffset cameraOffset;
    public CinemachineTargetGroup  targetGroup;

    public float offsetMultiplier;
    public float offsetSpeed;
    public Vector2 MaxOffsetAmount;
    private Camera cam;
    private Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameEvents.current.isMultiplayer){
            offset = targetGroup.gameObject.transform.position - cam.gameObject.transform.position;
        }
        //TODO add offset for singleplayer aiming
        else{
            offset = (cam.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - (new Vector3(0.5f, 0.5f, 0f))) * 2;
        }

        offset = Vector2.Min(offset, MaxOffsetAmount);
        offset = Vector2.Max(offset, -MaxOffsetAmount);

        Debug.Log(offset);
        cameraOffset.m_Offset = Vector3.Lerp(cameraOffset.m_Offset, offset*offsetMultiplier, Time.deltaTime * offsetSpeed);;
    }
}

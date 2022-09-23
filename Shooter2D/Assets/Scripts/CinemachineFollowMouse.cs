using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFollowMouse : MonoBehaviour
{
    public CinemachineCameraOffset cameraOffset;
    public CinemachineTargetGroup  targetGroup;

    public float offsetMultiplier;
    public float offsetSpeed;
    public Vector2 MaxOffsetAmount;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        //TODO Fix input later
        //Vector2 mousePos = (cam.ScreenToViewportPoint(Input.mousePosition) - (new Vector3(0.5f, 0.5f, 0f))) * 2;
        Vector2 offset = targetGroup.gameObject.transform.position - cam.gameObject.transform.position;

        offset = Vector2.Min(offset, MaxOffsetAmount);
        offset = Vector2.Max(offset, -MaxOffsetAmount);
        Debug.Log(offset);
        cameraOffset.m_Offset = Vector3.Lerp(cameraOffset.m_Offset, offset*offsetMultiplier, Time.deltaTime * offsetSpeed);;
    }
}

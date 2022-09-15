using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineFollowMouse : MonoBehaviour
{
    public CinemachineCameraOffset cameraOffset;

    public float offsetAmount;
    public float offsetSpeed;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = (cam.ScreenToViewportPoint(Input.mousePosition) - (new Vector3(0.5f, 0.5f, 0f))) * 2;

        cameraOffset.m_Offset = Vector3.Lerp(cameraOffset.m_Offset, mousePos*offsetAmount, Time.deltaTime * offsetSpeed);;
    }
}

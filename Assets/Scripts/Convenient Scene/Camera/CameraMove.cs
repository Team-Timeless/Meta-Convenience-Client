using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float camXspeed = 5.0f;
    [SerializeField]
    private float camYspeed = 3.0f;

    private float limitMinX = -80.0f;
    private float limitMaxX = 50;

    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * camYspeed;      // 마우스 좌/우 이동으로 카메라 Y 축회전
        eulerAngleX += mouseY * camXspeed;      // 마우스 상/히 이동으로 카메라 X 축회전

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleY, eulerAngleX, 0);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360) { angle -= 360; }
        return Mathf.Clamp(angle, min, max);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // 마우스 감도 나중에 조절가능하게 설정창에 넣기
    [SerializeField] private float camXspeed = 5.0f;
    [SerializeField] private float camYspeed = 3.0f;

    // 카메라, 캐릭터 회전 최소값 최대값
    [SerializeField] private float limitMinX = -30.0f;
    [SerializeField] private float limitMaxX = 50;

    // 카메라, 캐릭터 회전 각도
    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    public Camera cam = null;

    /**
     * @brief 카메라 Rotate 업데이트
     * @param float mouseX Input.GetAxis("MouseX") 값
     * @param float mouseY Input.GetAxis("MouseY") 값
     */
    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * camYspeed;      // 마우스 좌/우 이동으로 카메라 Y 축회전
        eulerAngleX -= mouseY * camXspeed;      // 마우스 상/하 이동으로 카메라 X 축회전

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        cam.transform.rotation = transform.rotation;
    }

    /**
     * @brief 카메라 각도 고정시키기
     * @param float angle 카메라의 각도
     * @param float min 카메라 고정할 각도 최소값
     * @param float max 카메라 고정할 각도 최대값
     */
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360) { angle -= 360; }
        return Mathf.Clamp(angle, min, max);
    }
}

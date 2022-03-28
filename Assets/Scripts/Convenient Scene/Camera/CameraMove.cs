using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float camXspeed = 5.0f;     // ���콺 ���� ���߿� ���������ϰ� ����â�� �ֱ�
    [SerializeField]
    private float camYspeed = 3.0f;

    private float limitMinX = -80.0f;
    private float limitMaxX = 50;

    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    private void Awake()
    {
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /**
     * @brief ī�޶� Rotate ������Ʈ
     * @param float mouseX Input.GetAxis("MouseX") ��
     * @param float mouseY Input.GetAxis("MouseY") ��
     */
    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * camYspeed;      // ���콺 ��/�� �̵����� ī�޶� Y ��ȸ��
        eulerAngleX -= mouseY * camXspeed;      // ���콺 ��/�� �̵����� ī�޶� X ��ȸ��

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    /**
     * @brief ī�޶� ���� ������Ű��
     * @param float angle ī�޶��� ����
     * @param float min ī�޶� ������ ���� �ּҰ�
     * @param float max ī�޶� ������ ���� �ִ밪
     */
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) { angle += 360; }
        if (angle > 360) { angle -= 360; }
        return Mathf.Clamp(angle, min, max);
    }
}

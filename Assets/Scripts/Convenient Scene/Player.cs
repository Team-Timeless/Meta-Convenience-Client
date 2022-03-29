using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    private PhotonView photonview = null;

    [SerializeField]
    private float camXspeed = 5.0f;     // 마우스 감도 나중에 조절가능하게 설정창에 넣기
    [SerializeField]
    private float camYspeed = 3.0f;

    private float limitMinX = -80.0f;
    private float limitMaxX = 50;

    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    private float playerSpeed = 5.0f;
    private float playerJumpForce = 7.0f;
    private Rigidbody rigid = null;
    private Vector3 vec = Vector3.zero;

    private void Awake()
    {
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (photonview.IsMine)
        {
            Camera.main.transform.position = transform.position;
            UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

    }

    void FixedUpdate()
    {
        if (photonview.IsMine)
        {
            PlayerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            PlayerJump();
        }
    }

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
        Camera.main.transform.rotation = transform.rotation;
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

    /**
     * @brief 사용자 이동
     * @param float moveX 키보드 A, D 입력으로 좌우 이동
     * @param float moveZ 키보드 W, S 입력으로 앞뒤 이동
     */
    void PlayerMove(float moveX, float moveZ)
    {
        vec = Vector3.right * moveX + Vector3.forward * moveZ;

        vec = Camera.main.transform.TransformDirection(vec); // 카메라가 보고 있는 방향으로 앞 방향 변경
        vec.Normalize(); // 균일한 이동 위해서 정규화

        transform.position += vec * playerSpeed * Time.deltaTime;
    }

    /**
     * @brief 사용자 점프
     */
    void PlayerJump()
    {
        if(Input.GetButtonDown("Jump")) // space 입력으로 점프
        {
            rigid.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
    }

}

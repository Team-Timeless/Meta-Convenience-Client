using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Valve.VR;

public class Player : MonoBehaviour
{
    [SerializeField] private PhotonView photonview = null;

    // 마우스 감도 나중에 조절가능하게 설정창에 넣기
    [SerializeField] private float camXspeed = 5.0f;     
    [SerializeField] private float camYspeed = 3.0f;

    // 카메라, 캐릭터 회전 최소값 최대값
    [SerializeField] private float limitMinX = -30.0f;
    [SerializeField] private float limitMaxX = 50;

    // 카메라, 캐릭터 회전 각도
    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    // Player 이동속도, 점프
    private float playerSpeed = 5.0f;
    private float playerJumpForce = 7.0f;

    // Player 물리, 이동 Vec
    [SerializeField] private Rigidbody rigid = null;
    private Vector3 vec = Vector3.zero;

    // 닉네임 textmesh
    [SerializeField] private TextMesh nicktext = null;

    private bool isTouch = false;
    private float touchTime = 0.0f;

    [SerializeField] private Item item = null;

    [SerializeField] private Camera cam = null;     // <! 메인 카메라

    // VR 왼손, 오른손 컨트롤러
    public SteamVR_Input_Sources right_hand;
    public SteamVR_Input_Sources left_hand;

    // VR 컨트롤러 액션
    public SteamVR_Action_Vector2 vrInputVec2 = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Move");
    public SteamVR_Action_Boolean jump = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Jump");

    private void Awake()
    {
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (photonview.IsMine) { nicktext.text = NetworkMng.I.nickname; }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine)
        {
            Debug.DrawRay(transform.position, transform.forward * 3.0f, Color.red);     // <! 디버그용
            
            if (!NetworkMng.I.isVR)
            {
                PlayerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                cam.transform.position = transform.position;
                ClickEvent();
                if (Input.GetKeyDown(KeyCode.Escape) && GameMng.I.itemDetails.gameObject.active)     // <! 임시
                {
                    GameMng.I.itemDetails.gameObject.SetActive(false);
                }
            }
            else
            {
                PlayerMove(vrInputVec2.GetAxis(left_hand).x, vrInputVec2.GetAxis(left_hand).y);
            }
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

    /**
     * @brief 사용자 이동, 점프
     * @param float moveX 키보드 A, D 입력으로 좌우 이동
     * @param float moveZ 키보드 W, S 입력으로 앞뒤 이동
     */
    void PlayerMove(float moveX, float moveZ)
    {
        vec = Vector3.right * moveX + Vector3.forward * moveZ;

        vec = cam.transform.TransformDirection(vec); // 카메라가 보고 있는 방향으로 앞 방향 변경
        vec.Normalize(); // 균일한 이동 위해서 정규화

        transform.position += vec * playerSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump")) // space 입력으로 점프
        {
            rigid.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
        else if(jump.GetStateDown(right_hand))
        {
            rigid.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
    }

    /**
     * @brief 마우스 클릭 & 홀드 구별
     */
    void ClickEvent()
    {
        if (Input.GetMouseButtonDown(0) && !isTouch)
        {
            if (GameMng.I.getRayCastGameObject(this.transform) && !GameMng.I.itemDetails.gameObject.active)
            {
                item = GameMng.I.getRayCastGameObject(this.transform).GetComponent<Item>();
            }
            else
            {
                item = null;
            }
            isTouch = true;
            // TODO : UI 켜주기
        }

        if (Input.GetMouseButton(0))
        {
            if (isTouch)
            {
                touchTime += Time.deltaTime;
                GameMng.I.holdimg.fillAmount = Mathf.Lerp(0.0f, 1.0f, touchTime);
            }
            if (touchTime > 1f)
            {
                if (item && item.itemActive.Equals(ITEM_ACTIVE.NONE))
                {
                    if (!item.gameObject.GetComponent<Rigidbody>())
                    {
                        item.gameObject.AddComponent<Rigidbody>();
                    }
                    item.GetComponent<Item>().itemActive = ITEM_ACTIVE.HOLD;

                    if (!GameMng.I.basket.ContainsKey(item.name))
                        GameMng.I.basket.Add(item.name, item);
                }
            }
        }
        if (item && item.itemActive.Equals(ITEM_ACTIVE.HOLD))
        {
            item.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (item && touchTime < 1f)
            {
                Cursor.lockState = CursorLockMode.None;
                GameMng.I.itemDetails._gameobject.SetActive(true);
                GameMng.I.itemDetails._itemname = item.getName;
                GameMng.I.itemDetails._itemcost = item.getPrice.ToString();
                GameMng.I.itemDetails._itemdetails = item.getDesc;
            }
            GameMng.I.holdimg.fillAmount = 0f;

            if (item)
                item.itemActive = ITEM_ACTIVE.NONE;

            isTouch = false;
            touchTime = 0.0f;
        }
    }
}

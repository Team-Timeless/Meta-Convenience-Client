using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Valve.VR;

public class Player : MonoBehaviour
{
    [SerializeField] private PhotonView photonview = null;

    // ���콺 ���� ���߿� ���������ϰ� ����â�� �ֱ�
    [SerializeField] private float camXspeed = 5.0f;
    [SerializeField] private float camYspeed = 3.0f;

    // ī�޶�, ĳ���� ȸ�� �ּҰ� �ִ밪
    [SerializeField] private float limitMinX = -30.0f;
    [SerializeField] private float limitMaxX = 50;

    // ī�޶�, ĳ���� ȸ�� ����
    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    // Player �̵��ӵ�, ����
    private float playerSpeed = 5.0f;
    private float playerJumpForce = 4.0f;

    // Player ����, �̵� Vec
    [SerializeField] private Rigidbody rigid = null;
    private Vector3 vec = Vector3.zero;

    // �г��� textmesh
    [SerializeField] private TextMesh nicktext = null;

    [SerializeField] private bool isTouch = false;
    private float touchTime = 0.0f;

    [SerializeField] private Item item = null;

    [SerializeField] private Camera cam = null;     // <! ���� ī�޶�

    [SerializeField] private PlayerVR_input player_input;

    private void Awake()
    {
        // Cursor.visible = false;
        player_input ??= GetComponent<PlayerVR_input>();
        Cursor.lockState = CursorLockMode.Locked;
        if (photonview.IsMine) { nicktext.text = NetworkMng.I.nickname; }
    }

    void Start()
    {
        if (photonview.IsMine)
        {
            cam.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine)
        {
            Debug.DrawRay(transform.position, transform.forward * 3.0f, Color.red);     // <! ����׿�

            PlayerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (player_input == null)
            {
                UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                cam.transform.position = transform.position;
                ClickEvent();
                if (Input.GetKeyDown(KeyCode.Escape) && GameMng.I.itemDetails[0].gameObject.activeSelf)     // <! �ӽ�
                {
                    GameMng.I.itemDetails[0].UnActiveUI();
                }
            }
            else
            {
                if (!player_input.isBasket)
                {
                    PlayerMove(player_input.vrInputVec2.GetAxis(player_input.left_hand).x, player_input.vrInputVec2.GetAxis(player_input.left_hand).y);
                }
                else
                {
                    player_input.scroll.value += player_input.vrInputVec2.GetAxis(player_input.left_hand).y * Time.deltaTime;
                }
            }
        }
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
        cam.transform.rotation = transform.rotation;
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

    /**
     * @brief ����� �̵�, ����
     * @param float moveX Ű���� A, D �Է����� �¿� �̵�
     * @param float moveZ Ű���� W, S �Է����� �յ� �̵�
     */
    void PlayerMove(float moveX, float moveZ)
    {
        vec = Vector3.right * moveX + Vector3.forward * moveZ;

        vec = cam.transform.TransformDirection(vec); // ī�޶� ���� �ִ� �������� �� ���� ����
        vec.Normalize(); // ������ �̵� ���ؼ� ����ȭ

        transform.position += vec * playerSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump")) // space �Է����� ����
        {
            rigid.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
        else if (player_input != null && player_input.jump.GetStateDown(player_input.right_hand))
        {
            rigid.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
    }

    /**
     * @brief ���콺 Ŭ�� & Ȧ�� ����
     */
    void ClickEvent()
    {
        if (Input.GetMouseButtonDown(0) && !isTouch)        // <! �ѹ� Ŭ�� ���� 
        {
            if (GameMng.I.getRayCastGameObject(this.transform) && !GameMng.I.itemDetails[NetworkMng.I.intIsVR].getUiActive)
            {
                item = GameMng.I.getRayCastGameObject(this.transform).GetComponent<Item>();
            }
            else
            {
                item = null;
            }
            isTouch = true;
            // TODO : UI ���ֱ�
        }

        if (Input.GetMouseButton(0))        // <! Ŭ�� ����
        {
            if (isTouch)        // <! �󸶳� ���ȴ��� ���
            {
                touchTime += Time.deltaTime;
                GameMng.I.holdimg.fillAmount = Mathf.Lerp(0.0f, 1.0f, touchTime);
            }
            if (touchTime > 1f)
            {
                if (item != null && item.itemActive.Equals(ITEM_ACTIVE.NONE))
                {
                    if (!item.gameObject.GetComponent<Rigidbody>())
                    {
                        item.gameObject.AddComponent<Rigidbody>();
                    }
                    item.itemActive = ITEM_ACTIVE.HOLD;

                    if (!GameMng.I.basket.ContainsKey(item.name))
                        GameMng.I.basket.Add(item.name, item);
                }
            }
        }
        
        if (item != null && item.itemActive.Equals(ITEM_ACTIVE.HOLD))
        {
            item.transform.position = transform.position + transform.forward * 1.4f;//new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 0.5f);
            item.transform.rotation = transform.rotation;
        }

        if (Input.GetMouseButtonUp(0))      // <! ���콺���� �� ������ 
        {
            if (item != null && touchTime < 1f)
            {
                Cursor.lockState = CursorLockMode.None;
                GameMng.I.setItemDetails(item);
            }
            GameMng.I.holdimg.fillAmount = 0f;

            if (item != null)
                item.itemActive = ITEM_ACTIVE.NONE;

            isTouch = false;
            touchTime = 0.0f;
        }
    }
}

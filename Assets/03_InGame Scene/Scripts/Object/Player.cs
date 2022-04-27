using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonview = null;

    [SerializeField]
    private float camXspeed = 5.0f;     // ���콺 ���� ���߿� ���������ϰ� ����â�� �ֱ�
    [SerializeField]
    private float camYspeed = 3.0f;

    // ī�޶�, ĳ���� ȸ�� �ּҰ� �ִ밪
    [SerializeField]
    private float limitMinX = -30.0f;
    [SerializeField]
    private float limitMaxX = 50;

    // ī�޶�, ĳ���� ȸ�� ����
    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    // Player �̵��ӵ�, ����
    private float playerSpeed = 5.0f;
    private float playerJumpForce = 7.0f;

    // Player ����, �̵� Vec
    [SerializeField]
    private Rigidbody rigid = null;
    private Vector3 vec = Vector3.zero;

    // �г��� textmesh
    [SerializeField]
    private TextMesh nicktext = null;

    private bool isTouch = false;
    private float touchTime = 0.0f;

    private Item item = null;


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
            Debug.DrawRay(transform.position, transform.forward * 15.0f, Color.red);
            UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            PlayerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Camera.main.transform.position = transform.position;
            ClickEvent();
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
        Camera.main.transform.rotation = transform.rotation;
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

        vec = Camera.main.transform.TransformDirection(vec); // ī�޶� ���� �ִ� �������� �� ���� ����
        vec.Normalize(); // ������ �̵� ���ؼ� ����ȭ

        transform.position += vec * playerSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump")) // space �Է����� ����
        {
            rigid.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
    }

    /**
     * @brief ���콺 Ŭ�� & Ȧ�� ����
     */
    void ClickEvent()
    {
        if (Input.GetMouseButtonDown(0) && !isTouch)
        {
            if (GameMng.I.getRayCastGameObject(this.transform))
            {
                item = GameMng.I.getRayCastGameObject(this.transform).GetComponent<Item>();
            }
            isTouch = true;
            // TODO : UI ���ֱ�
            Debug.Log("click");
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
                if (item && item.CompareTag("item") && item.itemActive == ITEM_ACTIVE.NONE)
                {
                    if (!item.gameObject.GetComponent<Rigidbody>())
                    {
                        item.gameObject.AddComponent<Rigidbody>(); 
                    }
                    item.GetComponent<Item>().itemActive = ITEM_ACTIVE.HOLD;
                }
                // TODO : ������ ����
                // 0.5 �ʰ� ������������
                Debug.Log("hold");
            }
        }
        if(item && item.itemActive == ITEM_ACTIVE.HOLD)
        {
            item.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.0f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameMng.I.holdimg.fillAmount = 0f;
            if (item)
                item.itemActive = ITEM_ACTIVE.NONE;
            isTouch = false;
            touchTime = 0.0f;
        }
    }
}

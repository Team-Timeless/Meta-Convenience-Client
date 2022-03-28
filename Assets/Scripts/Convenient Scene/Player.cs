using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    private PhotonView photonview = null;
    private CameraMove cameramove = null;

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        cameramove = Camera.main.GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(photonview.IsMine)
        {
            Camera.main.transform.position = transform.position;
            cameramove.UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}

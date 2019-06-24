using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HeadPosition : MonoBehaviour
{
    public GameObject HeadCamera;

    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        if (PV == null) PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            if (HeadCamera == null) HeadCamera = GameObject.Find("Main Camera");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            transform.position = HeadCamera.transform.position;
            transform.rotation = HeadCamera.transform.rotation;
            
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PhotonView PV;
    public GameObject LeapCamera;


    

    void Start()
    {
        LeapCamera = GameObject.FindWithTag("MainCamera");
        PV = GetComponent<PhotonView>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            transform.position = LeapCamera.transform.position;
            transform.rotation = LeapCamera.transform.rotation;

        }
    }
}
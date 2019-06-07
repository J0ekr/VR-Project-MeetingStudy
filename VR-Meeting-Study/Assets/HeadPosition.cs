using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HeadPosition : MonoBehaviour
{
    public GameObject MainCamera;
    public PhotonView PV;
    private void Awake()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            transform.position = MainCamera.transform.position;
            transform.rotation = MainCamera.transform.rotation;
            
        }
    }
}

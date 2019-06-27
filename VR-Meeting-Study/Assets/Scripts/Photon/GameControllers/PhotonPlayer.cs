﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public static PhotonPlayer photonPlayer;
    private PhotonView PV;
    public GameObject MyAvatar;

    public int mySpawn = 0;
    public int otherSpawn = 1;
    

    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (photonPlayer == null) photonPlayer = this;
        
        if (PV.IsMine)
        {

            
            MyAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonHands")
                , new Vector3(0,0,0), new Quaternion(0,0,0,0), 0);
            MyAvatar.name = "SlavePlayer";
            
            if (PhotonNetwork.IsMasterClient)
            {
                MyAvatar.name = "MasterPlayer";
            }
            

        }
    }
    
}
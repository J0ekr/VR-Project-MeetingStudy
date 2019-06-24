﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json.Bson;

public class SetPlayerSpawn : MonoBehaviour
{

    public static SetPlayerSpawn setSpawn;
    void Start()
    {
        if (setSpawn == null) setSpawn = this;
    }

    public void SetPosition()
    {
        if (PhotonPlayer.photonPlayer.MyAvatar != null)
            transform.position = PhotonPlayer.photonPlayer.MyAvatar.transform.position;
    }

    public void SetRotation()
    {
         if (PhotonPlayer.photonPlayer.MyAvatar != null)
             transform.rotation = PhotonPlayer.photonPlayer.MyAvatar.transform.rotation;
    }

    public void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            transform.position = GameSetup.GS.SpawnPoints[0].position;
            transform.rotation = GameSetup.GS.SpawnPoints[0].rotation;
        }        
        if (Input.GetKeyDown("b"))
        {
            transform.position = GameSetup.GS.SpawnPoints[1].position;
            transform.rotation = GameSetup.GS.SpawnPoints[1].rotation;
        }
    }
}

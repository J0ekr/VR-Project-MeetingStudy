using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject MyAvatar;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.GS.SpawnPoints.Length);
        if (PV.IsMine)
        {
            MyAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonHands")
                , GameSetup.GS.SpawnPoints[spawnPicker].position, GameSetup.GS.SpawnPoints[spawnPicker].rotation, 0);
        }
    }
}
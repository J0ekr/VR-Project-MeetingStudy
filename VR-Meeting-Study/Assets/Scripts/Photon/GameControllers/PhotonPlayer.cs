using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

// Lets the instantiated PhotonNetworkPlayer instatiate the avatar
public class PhotonPlayer : MonoBehaviour
{
    public static PhotonPlayer photonPlayer;
    private PhotonView PV;
    public GameObject MyAvatar;


    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (photonPlayer == null) photonPlayer = this;

        if (PV.IsMine)
        {
            MyAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonHands")
                , new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), 0);

            
            // Depending if unity gets started first or second (master / slave) one photon player gets named slave or master
            MyAvatar.name = "SlavePlayer";

            if (PhotonNetwork.IsMasterClient)
            {
                MyAvatar.name = "MasterPlayer";
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log ("Player Connected "+ player.name);
    }
 
 
    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {    
        Debug.Log ("Player Disconnected "+ player.name);
    }
}

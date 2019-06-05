using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonCapsuleHand : Leap.Unity.CapsuleHand
{

    public PhotonView capsule_PV;
    public static PhotonCapsuleHand pvh;

    void Start()
    {
        pvh = this;
        capsule_PV = GetComponent<PhotonView>();
    }
    
    [PunRPC]
    public override void UpdateHand()
    {
        base.UpdateHand();
    }


}

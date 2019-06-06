using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            MapHandMovement.mhp.AvatarHand_Right = transform.Find("R_Hand_Human_Normal").gameObject;
            MapHandMovement.mhp.AvatarHand_Left = transform.Find("L_Hand_Human_Normal").gameObject;
            MapHandMovement.mhp.AvatarBones_Left =
                MapHandMovement.mhp.GetChildRecursive(MapHandMovement.mhp.AvatarHand_Right);
            MapHandMovement.mhp.AvatarBones_Right =
                MapHandMovement.mhp.GetChildRecursive(MapHandMovement.mhp.AvatarHand_Left);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MapHandMovement : MonoBehaviour
{
    public static MapHandMovement mhp;
    public PhotonView PV;
    public GameObject LeapHand_Right;
    public GameObject LeapHand_Left;

    public GameObject AvatarHand_Right;
    public GameObject AvatarHand_Left;

    public GameObject[] LeapBones_Right;
    public GameObject[] LeapBones_Left;
    public GameObject[] AvatarBones_Right;

    public GameObject[] AvatarBones_Left;

    public bool isConnected = false;
    // Start is called before the first frame update


    public GameObject[] GetChildRecursive(GameObject obj)
    {
        List<GameObject> listOfChildren = new List<GameObject>();


        foreach (Transform g in obj.GetComponentsInChildren<Transform>())
        {
            listOfChildren.Add(g.gameObject);
        }

        return listOfChildren.ToArray();
    }

    void Start()
    {
        if (mhp == null)
        {
            mhp = this;
        }

        PV = GetComponent<PhotonView>();

        LeapBones_Right = GetChildRecursive(LeapHand_Right);
        LeapBones_Left = GetChildRecursive(LeapHand_Left);
    }

    [PunRPC]
    private void map(GameObject[] leap, GameObject[] avatar)
    {
        avatar[2].transform.position = leap[3].transform.position; //palm
        avatar[9].transform.position = leap[4].transform.position; //index_meta
        avatar[10].transform.position = leap[5].transform.position; //index_1
        avatar[11].transform.position = leap[6].transform.position; //index_2
        avatar[12].transform.position = leap[9].transform.position; //mid_meta
        avatar[13].transform.position = leap[10].transform.position; //mid_1
        avatar[14].transform.position = leap[11].transform.position; //mid_2
        avatar[15].transform.position = leap[14].transform.position; //pinky_meta
        avatar[16].transform.position = leap[15].transform.position; //pinky_1
        avatar[17].transform.position = leap[16].transform.position; //pinky_2
        avatar[18].transform.position = leap[19].transform.position; //ring_meta
        avatar[19].transform.position = leap[20].transform.position; //ring_1
        avatar[20].transform.position = leap[21].transform.position; //ring_2
        avatar[21].transform.position = leap[24].transform.position; //thumb_meta
        avatar[22].transform.position = leap[25].transform.position; //thumb_1
        avatar[23].transform.position = leap[26].transform.position; //thumb_2
        
        avatar[2].transform.rotation = leap[3].transform.rotation; //palm
        avatar[9].transform.rotation = leap[4].transform.rotation; //index_meta
        avatar[10].transform.rotation = leap[5].transform.rotation;; //index_1
        avatar[11].transform.rotation = leap[6].transform.rotation; //index_2
        avatar[12].transform.rotation = leap[9].transform.rotation; //mid_meta
        avatar[13].transform.rotation = leap[10].transform.rotation; //mid_1
        avatar[14].transform.rotation = leap[11].transform.rotation; //mid_2
        avatar[15].transform.rotation = leap[14].transform.rotation; //pinky_meta
        avatar[16].transform.rotation = leap[15].transform.rotation; //pinky_1
        avatar[17].transform.rotation = leap[16].transform.rotation; //pinky_2
        avatar[18].transform.rotation = leap[19].transform.rotation; //ring_meta
        avatar[19].transform.rotation = leap[20].transform.rotation; //ring_1
        avatar[20].transform.rotation = leap[21].transform.rotation; //ring_2
        avatar[21].transform.rotation = leap[24].transform.rotation; //thumb_meta
        avatar[22].transform.rotation = leap[25].transform.rotation; //thumb_1
        avatar[23].transform.rotation = leap[26].transform.rotation; //thumb_2
    }
    
    /* 
    void Update(){
        if (AvatarBones_Right.Length > 0 && LeapBones_Right.Length > 0 && PV.IsMine)
        {
            map(LeapBones_Right, AvatarBones_Right);
            if (!isConnected)
            {
                LeapBones_Right[1].GetComponent<SkinnedMeshRenderer>().enabled = false;
                isConnected = true;
                PV.RPC("map",RpcTarget.All, LeapBones_Right, AvatarBones_Right);
            }
        }

        if (AvatarBones_Left.Length > 0 && LeapBones_Left.Length > 0 && PV.IsMine)
        {
            map(LeapBones_Left, AvatarBones_Left);
            if (!isConnected)
            {
                LeapBones_Left[1].GetComponent<SkinnedMeshRenderer>().enabled = false;
                isConnected = true;
                PV.RPC("map",RpcTarget.All, LeapBones_Left, AvatarBones_Left);
            }
        }
    }
    
    */
    public void OnEvent()
    {
        
    }
    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


}
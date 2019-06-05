using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Leap.Unity;
using Leap.Unity.Interaction;
using System.Linq;


public class PlayerHand : MonoBehaviour
{
    // Start is called before the first frame update

    public PhotonView PV;
    public Chirality whichHand;

    public GameObject HandModels;
    public RiggedHand cHand;

    public Transform palm;

    public Transform mesh; 
   private void Awake()
   {
       HandModels = GameObject.Find("Hand Models");
       switch (whichHand)
       {
           case Chirality.Left:
               cHand = HandModels.transform.Find("LoPoly Rigged Hand Left").GetComponent<RiggedHand>();
               palm = cHand.transform.GetChild(1).Find("L_Palm");
               mesh = cHand.transform.GetChild(0).Find("LoPoly_Hand_Mesh_Left");
               break;
           case Chirality.Right:
               cHand = HandModels.transform.Find("LoPoly Rigged Hand Right").GetComponent<RiggedHand>();
               palm = cHand.transform.GetChild(1).Find("R_Palm");
               mesh = cHand.transform.GetChild(0).Find("LoPoly_Hand_Mesh_Right");
               break;
           default:
               Debug.Log("no hands found");
               break;

       }

       cHand.gameObject.SetActive(true);

   }

   private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            //Sets hand based on which hand is needed from whichHand variable.
            

            //Asserts that the hand has found something and been assigned.
            if (cHand == null)
            {
                Debug.LogError("Failed to find local player hand. Disabling hand preview on cilent " +
                               PhotonNetwork.NickName);
            }
            else
            {
                Debug.Log("Local Player " + whichHand + " was set to be interaction hand" + cHand.name);
            }
            mesh.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            //GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [PunRPC]
    private void showHand(bool show)
    {
        mesh.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = show;
        //GetComponent<MeshRenderer>().enabled = show;
        //gameObject.SetActive(true);
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            transform.position = palm.position;
            transform.rotation = palm.rotation;

            PV.RPC("showHand", RpcTarget.OthersBuffered, cHand.IsTracked);
            //PhotonCapsuleHand.pvh.capsule_PV.RPC("UpdateHand", RpcTarget.OthersBuffered, cHand.IsTracked);
        }
    }
}
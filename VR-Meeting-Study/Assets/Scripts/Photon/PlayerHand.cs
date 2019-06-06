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
    
    public GameObject hand;

    public GameObject[] LeapBones;
    public GameObject[] AvatarBones;
    
    public bool isConnected = false;
   private void Awake()
   {
       HandModels = GameObject.Find("Hand Models");
       switch (whichHand)
       {
           case Chirality.Left:
               hand = HandModels.transform.Find("LoPoly Rigged Hand Left").gameObject;
               cHand = hand.GetComponent<RiggedHand>();
               LeapBones = GetChildRecursive(hand);
               AvatarBones = GetChildRecursive(gameObject);
               break;
           case Chirality.Right:
               hand = HandModels.transform.Find("LoPoly Rigged Hand Right").gameObject;
               cHand = hand.GetComponent<RiggedHand>();
               LeapBones = GetChildRecursive(hand);
               AvatarBones = GetChildRecursive(gameObject);
               break;
           default:
               Debug.Log("no hands found");
               break;

       }

       hand.SetActive(false);

   }
   public GameObject[] GetChildRecursive(GameObject obj)
   {
       List<GameObject> listOfChildren = new List<GameObject>();


       foreach (Transform g in obj.GetComponentsInChildren<Transform>())
       {
           listOfChildren.Add(g.gameObject);
       }

       return listOfChildren.ToArray();
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

            hand.SetActive(false);
            //GetComponent<MeshRenderer>().enabled = false;
        }
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

    [PunRPC]
    private void showHand(bool show)
    {
        AvatarBones[1].GetComponent<SkinnedMeshRenderer>().enabled = show;
        //Debug.Log("scur");
        //hand.SetActive(show);
        //GetComponent<MeshRenderer>().enabled = show;
        //gameObject.SetActive(true);
        //TODO: set parent game object active
        //TODO: maybe put this scrip to top level component and transform.child.child.position = palm.position
    }
    
    private void Update()
    {
        
        
        if (AvatarBones.Length > 0 && LeapBones.Length > 0 && PV.IsMine)
        {
            map(LeapBones, AvatarBones);
            PV.RPC("showHand",RpcTarget.All, cHand.IsTracked);
            if (!isConnected)
            {
                LeapBones[1].GetComponent<SkinnedMeshRenderer>().enabled = false;
                isConnected = true;
            }
        }
        
    }
}
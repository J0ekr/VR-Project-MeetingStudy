using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Leap.Unity;
using Leap.Unity.Interaction;
using System.Linq;
using TMPro;


public class PlayerHand : MonoBehaviour
{
    // Start is called before the first frame update

    public PhotonView PV;
    public Chirality whichHand;

    public GameObject HandModels;
    public RiggedHand cHand;

    public GameObject hand;
    public GameObject palm;

    public GameObject[] LeapBones;
    public GameObject[] AvatarBones;

    private Quaternion leap_quad = new Quaternion(0, 0, 1, 0);

    public bool isConnected = false;
    
    public float[] distances = new float[6];

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
       
    }


    //hand.SetActive(false);


    public GameObject[] GetChildRecursive(GameObject obj)
    {
        List<GameObject> listOfChildren = new List<GameObject>();
        foreach (Transform g in obj.GetComponentsInChildren<Transform>())
        {
            listOfChildren.Add(g.gameObject);
        }

        return listOfChildren.ToArray();
    }

   /* public void addPhotonViews(GameObject obj)
    {
        foreach (Transform g in obj.GetComponentsInChildren<Transform>())
        {

            if (g.GetComponent<PhotonView>() == null)
            {
                PhotonView tmp_pv = g.gameObject.AddComponent<PhotonView>();
                tmp_pv.ViewID = myPhotonIDManager.id_mgr.GetNewId();

                PhotonTransformView tmp_ptv = g.gameObject.AddComponent<PhotonTransformView>();
                tmp_pv.ObservedComponents = new List<Component>();
                tmp_pv.ObservedComponents.Add(tmp_ptv);
            }

        }
    }    
    */
    private void Start()
    {
        HandModels = GameObject.Find("Hand Models");
        
        
        //if (PV.IsMine)
        //{
            switch (whichHand)
            {
                case Chirality.Left:

                    hand = HandModels.transform.Find("LoPoly Rigged Hand Left").gameObject;
                    cHand = hand.GetComponent<RiggedHand>();
                    palm = hand.transform.GetChild(1).gameObject;
                    LeapBones = GetChildRecursive(hand);
                    AvatarBones = GetChildRecursive(gameObject);
                    break;
                case Chirality.Right:
                    hand = HandModels.transform.Find("LoPoly Rigged Hand Right").gameObject;
                    cHand = hand.GetComponent<RiggedHand>();
                    palm = hand.transform.GetChild(1).gameObject;
                    LeapBones = GetChildRecursive(hand);
                    AvatarBones = GetChildRecursive(gameObject);
                    break;
                default:
                    Debug.Log("no hands found");
                    break;
            }
            
            if (cHand == null)
            {
                Debug.LogError("Failed to find local player hand. Disabling hand preview on cilent " +
                               PhotonNetwork.NickName);
            }
            else
            {
                Debug.Log("Local Player " + whichHand + " was set to be interaction hand" + cHand.name);
            }

            AvatarBones[1].GetComponent<SkinnedMeshRenderer>().enabled = false;
            
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = Vector3.Distance(palm.transform.position, AvatarBones[3].transform.position);
            }
            
            

        }
    

    private void map(GameObject[] leap, GameObject[] avatar)
    {
        avatar[2].transform.position = leap[3].transform.position; //palm
        avatar[21].transform.position = leap[24].transform.position; //thumb_meta

        
        //index, mid, pinky, ring, thumb
        int[] leap_index = new int[] { 5, 10, 15, 20, 24 };
        int[] avatar_index = new int[] { 9, 12, 15, 18, 21 };

        //Loops over fingers to correct their rotation
        for (int i = 0; i < leap_index.Length; i++)
        {
            //loops over bones
            for (int j = 0; j < 3; j++)
            {
                avatar[avatar_index[i] + j].transform.rotation = leap[leap_index[i] + j].transform.rotation * leap_quad;
            }
        }
        avatar[2].transform.rotation = leap[3].transform.rotation * leap_quad ; //palm

        //TODO: fix this, arm rotation and position
//        avatar[3].transform.position = leap[2].transform.position + distances[0] * leap[2].transform.up;
        //avatar[3].transform.rotation = leap[2].transform.rotation * Quaternion.Euler(180, 0, -90);
//        for (int i = 0; i < 5; i++)
//        {
//            avatar[4 + i].transform.rotation = avatar[3].transform.rotation * Quaternion.Euler(i * 18, 0, 0);
//            avatar[4 + i].transform.position = leap[2].transform.position + distances[i + 1] * leap[2].transform.up;
//        }
       
    } 

    [PunRPC]
    private void showHand(bool show)
    {
        if (AvatarBones[1] != null) AvatarBones[1].GetComponent<SkinnedMeshRenderer>().enabled = show;
        //TODO: set parent game object active
        //TODO: maybe put this scrip to top level component and transform.child.child.position = palm.position
    }

    private void Update()
    {
        if (AvatarBones.Length == 24 && LeapBones.Length == 28 && PV.IsMine)
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
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Leap.Unity;


// Class for the hand mapping, leap hands are only used as controller, and human hand movement is mapped from leap to them.
public class PlayerHand : MonoBehaviour
{
    // Start is called before the first frame update

    public PhotonView PV;
    public static PlayerHand playerHand;
    public Chirality whichHand;

    public GameObject HandModels;
    public RigidHand cHand;

    public GameObject hand;
    public GameObject palm;

    public GameObject[] LeapBones;
    public GameObject[] AvatarBones;

    private Vector3 fingerRotRight = new Vector3(180, 90, 0);
    private Vector3 palmRotRight = new Vector3(180, -90, 0);
    private Vector3 armRotRight = new Vector3(90, 0, -90);

    private Vector3 fingerRotLeft = new Vector3(0, -90, 0);
    private Vector3 palmRotLeft = new Vector3(0, 90, 0);
    private Vector3 armRotLeft = new Vector3(-90, -90, 0);

    public float[] distances = new float[6];


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (playerHand == null) playerHand = this;
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
        HandModels = GameObject.Find("Hand Models");
        //depending on handness setup controller (leap) hand and avatar hand
        switch (whichHand)
        {
            case Chirality.Left:

                hand = HandModels.transform.Find("RigidRoundHand_L").gameObject;
                cHand = hand.GetComponent<RigidHand>();
                LeapBones = GetChildRecursive(hand);
                AvatarBones = GetChildRecursive(gameObject);
                palm = LeapBones[21];
                break;
            case Chirality.Right:
                hand = HandModels.transform.Find("RigidRoundHand_R").gameObject;
                cHand = hand.GetComponent<RigidHand>();
                LeapBones = GetChildRecursive(hand);
                AvatarBones = GetChildRecursive(gameObject);
                palm = LeapBones[21];
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
        //index, mid, pinky, ring, thumb
        int[] leap_index = new int[] {6, 10, 14, 18, 2};
        int[] avatar_index = new int[] {9, 12, 15, 18, 21};


        switch (whichHand)
        {
            case Chirality.Left:

                avatar[2].transform.position = cHand.GetWristPosition();


                avatar[2].transform.rotation = leap[21].transform.rotation * Quaternion.Euler(palmRotLeft); //palm


                //Loops over fingers to correct their rotation
                for (int i = 0; i < leap_index.Length; i++)
                {
                    //loops over bones
                    for (int j = 0; j < 3; j++)
                    {
                        avatar[avatar_index[i] + j].transform.rotation =
                            leap[leap_index[i] + j].transform.rotation * Quaternion.Euler(fingerRotLeft);
                    }
                }


                avatar[3].transform.position = cHand.GetElbowPosition();
                avatar[3].transform.rotation = leap[22].transform.rotation * Quaternion.Euler(armRotLeft);

                break;

            case Chirality.Right:

                avatar[2].transform.position = cHand.GetWristPosition();


                avatar[2].transform.rotation = leap[21].transform.rotation * Quaternion.Euler(palmRotRight); //palm


                //Loops over fingers to correct their rotation
                for (int i = 0; i < leap_index.Length; i++)
                {
                    //loops over bones
                    for (int j = 0; j < 3; j++)
                    {
                        avatar[avatar_index[i] + j].transform.rotation =
                            leap[leap_index[i] + j].transform.rotation * Quaternion.Euler(fingerRotRight);
                    }
                }


                avatar[3].transform.position = cHand.GetElbowPosition();
                avatar[3].transform.rotation = leap[22].transform.rotation * Quaternion.Euler(armRotRight);

                break;
        }


        SaveHandPosition.Save(avatar[2].gameObject, avatar[0].GetComponent<PlayerHand>().whichHand, PV.ViewID);
    }

    [PunRPC]
    private void showHand(bool show)
    {
        if (AvatarBones != null && AvatarBones.Length > 0 && AvatarBones[1] != null)
        {
            AvatarBones[1].GetComponent<SkinnedMeshRenderer>().enabled = show;
        }
    }


    private void Update()
    {
        if (AvatarBones.Length == 24 && LeapBones.Length == 23 && PV.IsMine)
        {
            map(LeapBones, AvatarBones);
            if (MySceneManager.sceneManager.currentScene == 0 || MySceneManager.sceneManager.currentScene == 1)
            {
                PV.RPC("showHand", RpcTarget.All, cHand.IsTracked);
            }
            else
            {
                showHand(cHand.IsTracked);
            }
        }
    }


    public void DeactivatePhotonTransforms()
    {
        Debug.Log("Deactivate Photon Transforms");
        Debug.Log(PV.ViewID);
        foreach (var bone in AvatarBones) bone.GetComponent<PhotonTransformView>().enabled = false;
    }

    public void ActivatePhotonTransforms()
    {
        Debug.Log("Activate Transforms");
        Debug.Log(PV.ViewID);
        foreach (var bone in AvatarBones) bone.GetComponent<PhotonTransformView>().enabled = true;
    }
}
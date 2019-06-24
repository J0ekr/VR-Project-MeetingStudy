using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using UnityEditor;
using UnityEngine;
using Valve.Newtonsoft.Json.Bson;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager sceneManager;

    public PhotonView PV;
    public int currentScene;

    public GameObject Master;
    public GameObject Slave;

    //public HandModelManager hMananger;

    //public GameObject HandModels;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = 0; // 1: Real, 2: fake, 3: no
        if (sceneManager == null)
        {
            sceneManager = this;
        }

        if (PV == null)
        {
            PV = GetComponent<PhotonView>();
        }


       
        //if (hMananger == null) hMananger = HandModels.GetComponent<HandModelManager>();
    }



    [PunRPC]
    public void syncScene(int scene)
    {
        currentScene = scene;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            PV.RPC("syncScene", RpcTarget.All, 0);
            PV.RPC("SetHandTransfer", RpcTarget.All, true);
            PV.RPC("showDummyHands", RpcTarget.All, false);
        }

        if (Input.GetKeyDown("1"))
        {
            PV.RPC("syncScene", RpcTarget.All, 1);
            PV.RPC("SetHandTransfer", RpcTarget.All, true);
            PV.RPC("showDummyHands", RpcTarget.All, false);
        }

        if (Input.GetKeyDown("2"))
        {
            PV.RPC("syncScene", RpcTarget.All, 2);
            PV.RPC("SetHandTransfer", RpcTarget.All, false);
            PV.RPC("showDummyHands", RpcTarget.All, true);
        }

        if (Input.GetKeyDown("3"))
        {
            PV.RPC("syncScene", RpcTarget.All, 3);
            PV.RPC("SetHandTransfer", RpcTarget.All, false);
            PV.RPC("showDummyHands", RpcTarget.All, true);
        }
    }

    [PunRPC]
    private void SetHandTransfer(bool mode)
    {
        if (mode)
        {
            PlayerHand.playerHand.ActivatePhotonTransforms();
        }
        else
        {
            PlayerHand.playerHand.DeactivatePhotonTransforms();
        }
    }

    public void GetGameObjects()
    {
        if (GameObject.Find("MasterPlayer") == null)
        {
            Master = GameObject.Find("PhotonHands(Clone)");
            Slave = GameObject.Find("SlavePlayer");
        }
        else
        {
            Slave = GameObject.Find("PhotonHands(Clone)");
            Master = GameObject.Find("MasterPlayer");
        }
    }
    [PunRPC]
    private void showDummyHands(bool show)
    {
        GetGameObjects();
        if (PhotonNetwork.IsMasterClient)
        {
            Slave.transform.Find("Head").Find("R_Dummy").gameObject.SetActive(show);
            Slave.transform.Find("Head").Find("L_Dummy").gameObject.SetActive(show);
        }
        else
        {
            Master.transform.Find("Head").Find("R_Dummy").gameObject.SetActive(show);
            Master.transform.Find("Head").Find("L_Dummy").gameObject.SetActive(show);
        }
    }
}
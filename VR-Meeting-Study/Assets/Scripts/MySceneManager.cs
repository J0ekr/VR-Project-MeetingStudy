using Photon.Pun;
using UnityEngine;


// In this class most of the Scene / study logic happens
// Switching between the different conditions (synchronized on both PCs and starting / stopping recording) with key G/H

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager sceneManager;

    public PhotonView PV;
    public int currentScene;

    private Animator animMaster;
    private Animator animSlave;


    public GameObject Master;
    public GameObject Slave;

    public bool isStudy = false;
    
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
        
    }


    // Synchronice current scene between both computers
    [PunRPC]
    public void syncScene(int scene)
    {
        currentScene = scene;
    }

    // Update is called once per frame
    // Input 0-3 switches between the different conditions
    // Start recording with G and stop with H
    
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

        if (Input.GetKeyDown(KeyCode.G))

        {
            PV.RPC("setStudy", RpcTarget.All, true);
            if (currentScene == 3)
            {
                PV.RPC("playAnimation", RpcTarget.All);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            PV.RPC("setStudy", RpcTarget.All, false);
            if (currentScene == 3)
            {
                PV.RPC("stopAnimation", RpcTarget.All);
            }
        }
    }


    [PunRPC]
    private void setStudy(bool active)
    {
        isStudy = active;
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

        if (currentScene == 2)
        {
            Master.transform.Find("ViveHead").position = new Vector3(0.95f, 0.78f, -0.29f);
            Slave.transform.Find("ViveHead").position = new Vector3(-0.95f, 0.8f, -0.19f);

            Master.transform.Find("ViveHead").rotation = Quaternion.Euler(-90, 14, 76);
            Slave.transform.Find("ViveHead").rotation = Quaternion.Euler(-90, -160, 67);
        }

        if (currentScene == 3)
        {
            Master.transform.Find("ViveHead").position = new Vector3(1.7f, 1.7f, -0.25f);
            Slave.transform.Find("ViveHead").position = new Vector3(-1.7f, 1.7f, -0.4f);

            Master.transform.Find("ViveHead").rotation = Quaternion.Euler(0, -90, 0);
            Slave.transform.Find("ViveHead").rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    // For Cond 2 & 3 show dummy hands instead of the real ones to the opponent
    // Depending who loads the scene master and slave are set accordingly
    [PunRPC]
    private void showDummyHands(bool show)
    {
        GetGameObjects();
        if (PhotonNetwork.IsMasterClient)
        {
            Slave.transform.Find("ViveHead").Find("R_Dummy").gameObject.SetActive(show);
            Slave.transform.Find("ViveHead").Find("L_Dummy").gameObject.SetActive(show);
        }
        else
        {
            Master.transform.Find("ViveHead").Find("R_Dummy").gameObject.SetActive(show);
            Master.transform.Find("ViveHead").Find("L_Dummy").gameObject.SetActive(show);
        }

        animMaster = Master.transform.Find("ViveHead").GetComponent<Animator>();
        animSlave = Slave.transform.Find("ViveHead").GetComponent<Animator>();
    }

    
    // For condition 3 starting and stopping the Fake animation, synced between both PCs.
    [PunRPC]
    private void playAnimation()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (null != animSlave)
            {
                //animSlaveRight.enabled = true;
                animSlave.Play("test", 0, 0.25f);
            }
        }
        else
        {
            if (null != animMaster)
            {
                //anmiMasterRight.enabled = true;
                animMaster.Play("test", 0, 0.25f);
            }
        }
    }

    [PunRPC]
    private void stopAnimation()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (null != animSlave)
            {
                animSlave.Play("idle", 0, 0.25f);
            }
        }
        else
        {
            if (null != animMaster)
            {
                animMaster.Play("idle", 0, 0.25f);
            }
        }
    }
}
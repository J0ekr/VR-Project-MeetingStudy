using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager sceneManager;

    public PhotonView PV;
    public int currentScene;
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
    }
    [PunRPC]
    public void syncScene(int scene)
    {
        currentScene = scene;
    }
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown("0"))
            {
                PV.RPC("syncScene", RpcTarget.All, 0);
            }

            if (Input.GetKeyDown("1"))
            {
                PV.RPC("syncScene", RpcTarget.All, 1);
            }

            if (Input.GetKeyDown("2"))
            {
                PV.RPC("syncScene", RpcTarget.All, 2);
            }

            if (Input.GetKeyDown("3"))
            {
                PV.RPC("syncScene", RpcTarget.All, 3);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PictureSwitcher : MonoBehaviour
{
    public PhotonView PV;
    public GameObject Panel;

    public GameObject Paper1;
    public GameObject Paper2;
    

    public Texture [] Textures;

    public Texture[] Questions;
    
    private Renderer renderer;
    private Renderer rendererPaper1;
    private Renderer rendererPaper2;

    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {

        renderer = Panel.GetComponent<Renderer>();
        rendererPaper1 = Paper1.GetComponent<Renderer>();
        rendererPaper2 = Paper2.GetComponent<Renderer>();

        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count = mod(count+1,Textures.Length);
            PV.RPC("setContent", RpcTarget.All, count);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count = mod(count-1,Textures.Length);
            PV.RPC("setContent", RpcTarget.All, count);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PV.RPC("setWhite", RpcTarget.All);
        }
    }
    private int mod(int x, int m) {
        return (x%m + m)%m;
    }

    [PunRPC]
    private void setContent(int count)
    {
        var tmp = Textures[count];
        var tmp2 = Questions[count];
        renderer.material.SetTexture("_MainTex",tmp);
        rendererPaper1.material.SetTexture("_MainTex",tmp2);
        rendererPaper2.material.SetTexture("_MainTex",tmp2);
    }

    [PunRPC]
    private void setWhite()
    {
        renderer.material.SetTexture("_MainTex",Texture2D.whiteTexture);
        rendererPaper1.material.SetTexture("_MainTex",Texture2D.whiteTexture);
        rendererPaper2.material.SetTexture("_MainTex",Texture2D.whiteTexture);
    }
}

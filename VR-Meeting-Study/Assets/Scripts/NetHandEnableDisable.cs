using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetHandEnableDisable : MonoBehaviour
{
    public PhotonView PV;
    
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            Debug.Log("Added local player events");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

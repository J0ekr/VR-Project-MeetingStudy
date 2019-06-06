using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using Photon.Pun;
using UnityEngine;

public class PhotonHandEnableDisable : HandTransitionBehavior
{
    public static PhotonHandEnableDisable PHED;
    public PhotonView PV;

    private void Start()
    {
        if (PHED == null)
        {
            PHED = this;
        }

        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void HandReset()
    {
        if (PV.IsMine)
        {
            gameObject.SetActive(true);
        }
    }

    protected override void HandFinish()
    {
        if (PV.IsMine)
        {
            gameObject.SetActive(false);
        }
    }
}
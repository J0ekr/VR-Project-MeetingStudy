using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPhotonIDManager : MonoBehaviour
{
    public static myPhotonIDManager id_mgr;
    public int newPhotonID;
    // Start is called before the first frame update
    void Awake()
    {
        newPhotonID = 4001;
        if (id_mgr == null)
        {
            id_mgr = this;
        }
    }

    public int GetNewId()
    {
        newPhotonID++;
        return newPhotonID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObj : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab;
    
    void Start()
    {
        if (isLocalPlayer == true) Instantiate(PlayerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

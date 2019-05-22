using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

namespace VR_Meeting{
public class myGameManager : MonoBehaviourPunCallbacks
{
    [Header ("UC Game Manager")]
    public GameObject PlayerPrefab;

    [HideInInspector]
    public GameObject LocalPlayer;
    // Start is called before the first frame update
    private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Menu");
                return;
            }
        }

        // Use this for initialization
        void Start()
        {
            LocalPlayer = PhotonNetwork.Instantiate(PlayerPrefab.gameObject.name, Vector3.zero, Quaternion.identity).GetComponent<GameObject>();
        }


    // Update is called once per frame
    void Update()
    {
        
    }
}}


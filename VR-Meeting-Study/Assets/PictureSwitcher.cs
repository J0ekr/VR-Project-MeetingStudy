using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PictureSwitcher : MonoBehaviour
{
    public GameObject Panel;

    public Texture [] Textures;
    
    private Renderer renderer;

    private List<byte[]> pics;

    private int count = 0;

    private byte[] pic;
    private Texture2D _texture;
    // Start is called before the first frame update
    void Start()
    {

        renderer = Panel.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count = mod(count+1,Textures.Length);
            var tmp = Textures[count];
            renderer.material.SetTexture("_MainTex",tmp);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count = mod(count-1,Textures.Length);
            Debug.Log(count);
            var tmp = Textures[count];
            renderer.material.SetTexture("_MainTex",tmp);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            renderer.material.SetTexture("_MainTex",Texture2D.whiteTexture);
        }
    }
    private int mod(int x, int m) {
        return (x%m + m)%m;
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class EchoSurface : MonoBehaviour
{
    
    private Material material;
    private GameObject echoManager;

    public Vector2 textureSize = new Vector2(2048, 2048);
    public Texture2D texture;
    [SerializeField]
    private Texture2D blackTexture;


    private List <EchoData> PendingEchoData = new List <EchoData>();
    private bool needsTextureUpdate;
    private bool blackOutComplete; 

    private bool fadingToBlack = true;
    private float fadeSpeed;
    private float fadingFactor = 1.0f;

    private float echoRadius = 0f;
    private float maxRadius;
    private float radiusRate;


    private struct EchoData
    {
        public int x;
        public int y;
        public int penSize;
        public Color[] colors;
    }

    void Start()
    {
        var r = GetComponent<Renderer>();

        echoManager = GameObject.Find("EchoManager");

        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        blackTexture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        BlackOutInitialize();
        material = r.material;
        r.material.mainTexture = texture;
        fadeSpeed = echoManager.GetComponent<EchoCaster>().fadeSpeed;
        maxRadius = echoManager.GetComponent<EchoCaster>().maxDistance;
        radiusRate = echoManager.GetComponent<EchoCaster>().radiusRate;

       
    }

    void Update()
    {
        
        if (needsTextureUpdate)
        {
            BlackOut();
            blackOutComplete = true;
        }

        if (blackOutComplete)
        {
            ApplyEchoCast();
            blackOutComplete = false;
        }

        if (echoRadius < maxRadius)
        {
            echoRadius = echoRadius + radiusRate;
            material.SetFloat("_Radius", echoRadius);

        }
        //else 
        //{
        //    //FadeBlack(fadeSpeed);
        //}
    }

    /// <summary>
    /// queues a new echo cast in the shader. 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="penSize"></param>
    /// <param name="colors"></param>
    public void QueueEcho(int x, int y, int penSize, Color[] colors)
    {
        
        EchoData data = new EchoData { x = x , y = y, penSize = penSize, colors = colors };
        PendingEchoData.Add(data);
        needsTextureUpdate = true;
    }


    /// <summary>
    /// this function reviews the Pending Echo Data, clamps it to the boundaries and applies it to the surface texture. 
    /// </summary>
    private void ApplyEchoCast()
    {
        // reset shader position 
        Vector3 newCenterX = echoManager.GetComponent<EchoCaster>()._origin.position;
        material.SetVector("_Center", newCenterX);

        echoRadius = 0f;
        material.SetFloat("_Radius", echoRadius);

        foreach (EchoData data in PendingEchoData) 
        {
            int clampedX = Mathf.Clamp(data.x, 0, texture.width - data.penSize);
            int clampedY = Mathf.Clamp(data.y, 0, texture.height - data.penSize);
            int clampedWidth = Mathf.Clamp(data.penSize, 0, texture.width - clampedX);
            int clampedHeight = Mathf.Clamp(data.penSize, 0, texture.height - clampedY);
            texture.SetPixels(clampedX, clampedY, clampedWidth, clampedHeight, data.colors);        }
        texture.Apply();
        PendingEchoData.Clear();
        needsTextureUpdate=false;

        echoManager.GetComponent<EchoCaster>().RegisterPaintedUV(this);

    }

    private void BlackOutInitialize()
    {
        Color[] pixelsStart = blackTexture.GetPixels();
        for (int i = 0; i < pixelsStart.Length; i++)
        {
            pixelsStart[i] = Color.black;
        }

        blackTexture.SetPixels(pixelsStart);
        BlackOut();
    }

    /// <summary>
    /// This function changes all pixels the texture to turn black 
    /// </summary>
    public void BlackOut()
    {
        Graphics.CopyTexture(blackTexture, texture);
        texture.Apply();
    }

    /// <summary>
    /// This function should've provided a fade to black. 
    /// </summary>
    /// <param name="_fadeSpeed"></param>
    private void FadeBlack(float _fadeSpeed)
    {
        
        fadingFactor += _fadeSpeed;
        material.SetFloat("_FadingFactor", fadingFactor);

        if (fadingFactor == 1)
        {
            fadingToBlack = false;
        }
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EchoSurface : MonoBehaviour
{
    public Texture2D texture;
    private Material material;

    public Vector2 textureSize = new Vector2(2048, 2048);

    private List <EchoData> PendingEchoData = new List <EchoData>();
    private bool needsTextureUpdate;


    private float fadeSpeed;
    private bool fadingToBlack = true;

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

        

        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        material = r.material;
        r.material.mainTexture = texture;
        fadeSpeed = GameObject.Find("EchoManager").GetComponent<EchoCaster>().fadeSpeed;
        maxRadius = GameObject.Find("EchoManager").GetComponent<EchoCaster>().maxDistance;
        radiusRate = GameObject.Find("EchoManager").GetComponent<EchoCaster>().radiusRate;

        BlackOut(); 
    }

    void Update()
    {
        if (needsTextureUpdate)
        {

            ApplyEchoCast();
            fadingToBlack = true;

        }

        if (!needsTextureUpdate && fadingToBlack )
        {
            FadeBlack(fadeSpeed); 

        }

        if (echoRadius < maxRadius)
        {
            echoRadius = echoRadius + radiusRate;
            material.SetFloat("_Radius", echoRadius);

        }


    }

    // queues a new echo cast in the shader. 
    public void QueueEcho(int x, int y, int penSize, Color[] colors)
    {
        echoRadius = 0f;
        material.SetFloat("_Radius", echoRadius);

        EchoData data = new EchoData { x = x , y = y, penSize = penSize, colors = colors };
        PendingEchoData.Add(data);
        needsTextureUpdate = true;
    }


    // this function 
    private void ApplyEchoCast()
    {
        BlackOut();
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
    }


    // This function changes all pixels the texture to turn black 
    private void BlackOut()
    {
        Color[] pixelsStart = texture.GetPixels();
        for (int i = 0; i < pixelsStart.Length; i++)
        {
            pixelsStart[i] = Color.black;
        }

        texture.SetPixels(pixelsStart);
        texture.Apply();
    }


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



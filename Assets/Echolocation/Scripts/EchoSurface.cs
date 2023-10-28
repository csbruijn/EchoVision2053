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

        BlackOut(); 
    }

    void Update()
    {
        if (needsTextureUpdate)
        {
            //texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

            ApplyEchoCast();
            fadingToBlack = true;

        }

        if (needsTextureUpdate ==false && fadingToBlack == true)
        {
            FadeBlack(fadeSpeed); 

            //StartCoroutine(FadeToBlack(fadeSpeed));
        }
    }

    // queues a new echo cast in the shader. 
    public void QueueEcho(int x, int y, int penSize, Color[] colors)
    {
        fadingFactor = 0f;
        material.SetFloat("_FadingFactor", fadingFactor);

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
            texture.SetPixels(data.x, data.y, data.penSize,data.penSize, data.colors);
        }
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

        if (fadingFactor == 0)
        {
            fadingToBlack = false;
        }
    }

    IEnumerator FadeToBlack(float _fadeSpeed)
    {
        Color[] pixels = texture.GetPixels();
        float fadingProgress = 1.0f; 

        while (fadingProgress > 0)
        {
            fadingProgress -= _fadeSpeed * Time.deltaTime;

            fadingProgress = Mathf.Clamp01(fadingProgress); 

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.Lerp(Color.black, pixels[i], fadingProgress);
            }

            texture.SetPixels(pixels);
            texture.Apply();

            yield return null;
        }
        fadingToBlack = false;
    }
}



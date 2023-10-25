using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EchoSurface : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    private List <EchoData> PendingEchoData = new List <EchoData>();
    private bool needsTextureUpdate;


    private float fadeSpeed;
    private bool fadingToBlack = true;



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
        r.material.mainTexture = texture;
        fadeSpeed = GameObject.Find("EchoManager").GetComponent<EchoCaster>().fadeSpeed;
        

    }

    void Update()
    {
        if (needsTextureUpdate)
        {
            ApplyEchoCast();
            fadingToBlack = true;

        }

        if (needsTextureUpdate ==false && fadingToBlack == true)
        {
            FadeBlack(fadeSpeed); 
            
            //StartCoroutine(FadeToBlack(fadeSpeed));
        }


    }


    public void QueueEcho(int x, int y, int penSize, Color[] colors)
    {
        EchoData data = new EchoData { x = x , y = y, penSize = penSize, colors = colors };
        PendingEchoData.Add(data);
        needsTextureUpdate = true;
    }

    private void ApplyEchoCast()
    {
         foreach (EchoData data in PendingEchoData) 
        {
            texture.SetPixels(data.x, data.y, data.penSize,data.penSize, data.colors);
        }
        texture.Apply();
        PendingEchoData.Clear();
        needsTextureUpdate=false;
    }

    private void FadeBlack(float _fadeSpeed)
    {

        Color[] pixels = texture.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.Lerp(pixels[i], Color.black, _fadeSpeed * Time.deltaTime);
        }

        texture.SetPixels(pixels);
        texture.Apply();
        Debug.Log("Fade applied");

        // Check if the fading is complete.
        if (pixels[0] == Color.black)
        {
            fadingToBlack = false;
        }
    }

    IEnumerator FadeToBlack(float _fadeSpeed)
    {

        Color[] pixels = texture.GetPixels();
        float step = 1 - _fadeSpeed;


        for (int i =0; i < pixels.Length; i++)
        {
            pixels[i] = new Color(
                pixels[i].r * (step),
                pixels[i].g * ( step),
                pixels[i].b * (step),
                pixels[i].a
            );
        }

        texture.SetPixels(pixels);
        texture.Apply();

        if (pixels.Length > 0 && pixels[0].r > _fadeSpeed)
        { yield  return null; }

        fadingToBlack = false;
    }


}



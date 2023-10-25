using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoSurface : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    private List <EchoData> PendingEchoData = new List <EchoData>();
    private bool needsTextureUpdate;


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
    }

    void Update()
    {
        if (needsTextureUpdate)
        {
            ApplyEchoCast();
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
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocationBehaviour : MonoBehaviour

{

    public float testVolume = 5f;
    public Vector3 testLocation; 

    public Material EchoRender; 
    public Transform SoundLocation;

    //public Transform[] spawnPositions;


    public float maxRadius = 10.0f;
    public float Radiusrate = .025f;

    private float newRadius = 0f;
    private float newRadius2 = 0f;

    private Vector3 newCenterX;
    private bool echoActive;

    



    private void Start()
    {
        //EchoRender = GameObject.Find("EchoDark").GetComponent<Material>();

        if (EchoRender == null)
        {
            Debug.LogError("Target Renderer is not assigned!");
            return;
        }
    }

    private void Update()
    {

        ExtraEcho(testVolume, testLocation);


        if (!echoActive)
        {
            EchoLocaUpdate();
            echoActive = true;
        }


        if (echoActive)
        {
            SendEcho();
        }
    }

    //private void SendEchoPerformed(SendEcho.CallbackContext context)
    //{
    //    if (!echoActive)
    //    {
    //        EchoLocaUpdate();
    //        echoActive = true;
    //    }
    //}

    void SendEcho()
    {
        //foreach (Transform position in spawnPositions)

        if (newRadius < maxRadius)
        {
            newRadius = newRadius + Radiusrate;
        }
        else 
        { 
            newRadius = 0f; 
            echoActive = false;

        }
        EchoRender.SetFloat("_Radius", newRadius);
    }


    

    void EchoLocaUpdate()
    {
        Vector3 newCenterX = SoundLocation.position;
        EchoRender.SetVector("_Center", newCenterX);
    }

    void ExtraEcho(float _volume, Vector3 _position)
    {
        EchoRender.SetVector("_Center2", _position);

        if (newRadius2 < _volume)
        {
            newRadius2 = newRadius2 + Radiusrate;
        }
        else
        {
            newRadius2 = 0f;

        }
        EchoRender.SetFloat("_Radius2", newRadius2);

    }

}


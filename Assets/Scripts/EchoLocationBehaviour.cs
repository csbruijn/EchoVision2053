using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocationBehaviour : MonoBehaviour

{
    public Material targetMaterial; // Drag and drop the GameObject's material here in the Inspector
    public Transform PlayerLocation;




    public float maxRadius = 10.0f;
    public float Radiusrate = .025f;

    private float newRadius = 0f;

    private Vector3 newCenterX;
    private bool echoActive;




    private void Start()
    {
        

        if (targetMaterial == null)
        {
            Debug.LogError("Target Material is not assigned!");
            return;
        }
    }

    private void Update()
    {
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
        if (newRadius < maxRadius)
        {
            newRadius = newRadius + Radiusrate;
        }
        else 
        { 
            newRadius = 0f; 
            echoActive = false;

        }
        targetMaterial.SetFloat("_Radius", newRadius);
    }

    void EchoLocaUpdate()
    {
        Vector3 newCenterX = PlayerLocation.position;
        targetMaterial.SetVector("_Center", newCenterX);
    }
}


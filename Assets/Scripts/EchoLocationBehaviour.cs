using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocationBehaviour : MonoBehaviour

{

    public float testVolume = 5f;
    public Vector3 testLocation;

    [Header("Health Settings")]
    public Material EchoRender; 
    public Transform SoundLocation;

    //public Transform[] spawnPositions;

    [Header("Echo Variables ")]
    public float maxRadius = 10.0f;
    public float Radiusrate = .025f;




    private float newRadius = 0f;
    private float newRadius2 = 0f;

    private Vector3 newCenterX;
    private bool echoActive;
    private bool echoExternalActive;

    //public string tagToDetect = "interactableEcho";
    //private Vector3 collisionPoint;


    private void Start()
    {
        //EchoRender = GameObject.Find("EchoDark").GetComponent<Material>();

        echoExternalActive = false;
        newRadius = 0f;
        newRadius2 = 0f;



        if (EchoRender == null)
        {
            Debug.LogError("Target Renderer is not assigned!");
        }

        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material = EchoRender;
        }

    }

    private void Update()
    {
        if (!echoActive) {
            EchoLocaUpdate();
            echoActive = true;
        }


        if (echoActive) {
            SendEcho();
        }

        if (echoExternalActive)
        {
            ExternalEchoUpdate(testVolume);
        }
    }

    

    public void SetCollisionPoint(Vector3 collisionPoint, float volume)
    {
        testVolume = volume;
        EchoRender.SetVector("_Center2", collisionPoint);


        echoExternalActive = true;
    }


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
        EchoRender.SetFloat("_Radius", newRadius);
    }


    void EchoLocaUpdate()
    {
        Vector3 newCenterX = SoundLocation.position;
        EchoRender.SetVector("_Center", newCenterX);
    }

    void ExternalEchoUpdate(float maxRadius)
    {
        if (echoExternalActive)
        {
            if (newRadius2 < maxRadius)
            {
                newRadius2 = newRadius2 + Radiusrate;
            }
            else
            {
                newRadius2 = 0f;
                echoExternalActive = false;
            }
            EchoRender.SetFloat("_Radius2", newRadius2);
        }
    }

}


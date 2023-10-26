
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Collections;
using UnityEngine;

public class EchoCaster : MonoBehaviour
{

    public int rayCount = 360;
    public float _startDistance = 40f;
    public float maxDistance = 40f; 

    [SerializeField] private Transform _origin;
    [SerializeField] private int _penSize = 5;
    [SerializeField] private Renderer _renderer;
    
    private RaycastHit _touch;
    private int Index = 0; 


    public bool echoActive; 
    private float _distance;
    public Gradient gradient;  // Define a gradient for the color effect

    [Range(0.0f, 1.0f)] public float fadeSpeed;


    private float timer;
    public float timeBetweenEcho; 

    void Start()
    {
        echoActive = true;


         
        _distance = maxDistance;
    }

    void Update()
    {
        if (echoActive)
        {
            //CastEchoSlow();
            CastEchoFast();

        }

        timer--; 
        if (timer < 0 && !echoActive)
        {
            echoActive = true; 


        }


    }

    private void CastOutwards()
    {

        // REUSE THIS TO HAVE THE ECHO CAST OUTWARDS FRAME BY FRAME


        if (Index == rayCount)
        {
            Index = 0;
            echoActive = false;
            return;
        }
        else if (Index < rayCount)
        {

            Index++;


        }
    }

    private void CastEchoFast()
    {
        Color[] raycastColors = new Color[_penSize * _penSize];

        for (int i = 0; i < raycastColors.Length; i++)
        {
            raycastColors[i] = Color.clear;
        }



        for (int I = 0; I < rayCount; I++)
        {
            float angleVert = I * 360f / rayCount;

            for (int i = 0; i < rayCount; i++)
            {
                float angleHor = i * 360f / rayCount;

                Vector3 direction = Quaternion.Euler(angleVert, angleHor, 0) * transform.forward;

                if (Physics.Raycast(_origin.position, direction, out _touch, _distance))
                {
                    if (_touch.transform.CompareTag("EchoSurface"))
                    {

                        EchoSurface _echoSurface = _touch.transform.GetComponent<EchoSurface>();


                        Vector2 _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                        var x = (int)(_touchPos.x * _echoSurface.textureSize.x - (_penSize / 2));
                        var y = (int)(_touchPos.y * _echoSurface.textureSize.y - (_penSize / 2));


                        float distance = Vector3.Distance(_origin.position, _touch.point);
                        float t = Mathf.Clamp01(distance / maxDistance);
                        Color raycastColor = gradient.Evaluate(t);

                        //int index = x + y * _penSize;
                        //raycastColors[index] = raycastColor;

                        raycastColors = Enumerable.Repeat(raycastColor, _penSize * _penSize).ToArray();
                        _echoSurface.QueueEcho(x, y, _penSize, raycastColors);

                    }
                }
            }
            echoActive = false;
            timer = timeBetweenEcho; 
        }
    }
}

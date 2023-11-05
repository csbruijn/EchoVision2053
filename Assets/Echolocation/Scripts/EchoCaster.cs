

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class EchoCaster : MonoBehaviour
{

    public int rayCount = 360;
    private int iterations; 

    public float maxDistance = 40f;

    
    public Transform _origin;
    [SerializeField]
    private Material echoMat;

    [SerializeField] 
    private int _penSize = 5;    
    private RaycastHit _touch;
    private int layerMask;

    private bool echoActive; 
    private float _distance;
    public Gradient gradient;  

    [Range(0.0f, 1.0f)] public float fadeSpeed;

    public float radiusRate = .02f;

    public XRIDefaultInputActions _xrInput;
    private InputAction castEcho; 


    public List<EchoSurface> paintedUVs = new List<EchoSurface>();


    private void Awake()
    {
        _xrInput = new XRIDefaultInputActions();
    }

    void Start()
    {
        echoActive = true;
        layerMask = 1 << LayerMask.NameToLayer("EchoSurface");

        _distance = maxDistance;

        Vector3 newCenterX = _origin.position;
        echoMat.SetVector("_Center", newCenterX);

    }
    
    private void SendEcho(InputAction.CallbackContext context)
    {
        Debug.Log("Cast Echo");
        echoActive = true;
    }

    void Update()
    {
        if (echoActive)
        {
            CastEcho();
        }                
    }

    private void CastEcho()
    {

        ResetUVs();

        Color[] raycastColors = new Color[_penSize * _penSize];

        for (int i = 0; i < raycastColors.Length; i++)
        {
            raycastColors[i] = Color.clear;
        }

        for (int I = 0; I < rayCount/2; I++)
        {
            float angleVert =  (I * 360f / rayCount)  ;

            if (I <= rayCount / 4)
            {
                iterations = I*2;
            }
            else
            {
                iterations = (rayCount/2  - I) *2 ;
            }


            for (int i = 0; i < iterations; i++)
            {
                float angleHor = (i * 360f / iterations) ;

                Vector3 direction = Quaternion.Euler(angleVert, angleHor, 0) * transform.up;

                if (Physics.Raycast(_origin.position, direction, out _touch, _distance, layerMask))
                {

                    EchoSurface _echoSurface = _touch.transform.GetComponent<EchoSurface>();

                    if (_echoSurface != null)
                    {
                        Vector2 _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                        var x = (int)(_touchPos.x * _echoSurface.textureSize.x - (_penSize / 2));
                        var y = (int)(_touchPos.y * _echoSurface.textureSize.y - (_penSize / 2));

                        float distance = Vector3.Distance(_origin.position, _touch.point);
                        float t = Mathf.Clamp01(distance / maxDistance);
                        Color raycastColor = gradient.Evaluate(t);

                        raycastColors = Enumerable.Repeat(raycastColor, _penSize * _penSize).ToArray();
                        _echoSurface.QueueEcho(x, y, _penSize, raycastColors);

                    }
                }
            }
            echoActive = false;
        }
    }

    public void RegisterPaintedUV(EchoSurface uv)
    {
        paintedUVs.Add(uv);
        
    }

    private void ResetUVs()
    {
        foreach (EchoSurface  uv in paintedUVs)
        {
            uv.BlackOut();
        }
        paintedUVs.Clear();
    }

    private void OnEnable()
    {
        castEcho = _xrInput.XRIRightHand.SendEcho;
        castEcho.Enable();
        castEcho.performed += SendEcho;
    }

    private void OnDisable()
    {
        _xrInput.Disable();
    }
}

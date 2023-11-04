
using System.Linq;
using UnityEngine;

public class EchoCaster : MonoBehaviour
{

    public int rayCount = 360;
    private int iterations; 

    //public float _startDistance = 40f;
    public float maxDistance = 40f; 

    public Transform _origin;
    [SerializeField] private int _penSize = 5;    
    private RaycastHit _touch;
    private int Index = 0;
    private int layerMask;

    private bool echoActive; 
    private float _distance;
    public Gradient gradient;  // Define a gradient for the color effect

    [Range(0.0f, 1.0f)] public float fadeSpeed;


    private float timer;
    public float timeBetweenEcho;

    public float radiusRate = .02f;

    void Start()
    {
        echoActive = true;
        layerMask = 1 << LayerMask.NameToLayer("EchoSurface");


        _distance = maxDistance;


        //InvokeRepeating("CastEchoFast", 2.0f, timeBetweenEcho);


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



        for (int I = 0; I < rayCount/2; I++)
        {
            float angleVert =  (I * 360f / rayCount)  ;

            if (I <= rayCount / 4)
            {
                iterations = I;
            }
            else
            {
                iterations = rayCount / 2 - I;
            }

            //float offset = Random.Range(-10f, 10f);

            for (int i = 0; i < iterations; i++)
            {
                float angleHor = (i * 360f / iterations) ;

                Vector3 direction = Quaternion.Euler(angleVert, angleHor, 0) * transform.up;

                if (Physics.Raycast(_origin.position, direction, out _touch, _distance, layerMask))
                {

                    EchoSurface _echoSurface = _touch.transform.GetComponent<EchoSurface>();

                    //if (_touch.transform.CompareTag("EchoSurface"))
                    if (_echoSurface != null)
                    {



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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Collections;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{

    public int rayCount = 360;
    public float _startDistance = 40f;

    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;
    [SerializeField] private Renderer _renderer;
    
    private Color[] _colors;

    private RaycastHit _touch;
    private int Index = 0; 


    public bool echoActive; 
    private float _distance;

    void Start()
    {
        echoActive = true;

        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        _distance = _startDistance;
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
        if (Index == rayCount)
        {
            Index = 0;
            echoActive = false;
            return;
        } 
        else if (Index < rayCount)
        {
            float angleVert = Index * 360f / rayCount;
            Index++;

            for (int i = 0; i < rayCount; i++)
            {
                float angleHor = i * 360f / rayCount;
                Vector3 direction = Quaternion.Euler(angleVert, angleHor, 0) * transform.forward;

                if (Physics.Raycast(_tip.position, direction, out _touch, _distance))
                {
                    if (_touch.transform.CompareTag("EchoSurface"))
                    {
                        EchoSurface _whiteboard = _touch.transform.GetComponent<EchoSurface>();

                        Vector2 _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                        var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                        var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                        _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                        _whiteboard.texture.Apply();
                    }
                }
            }


        }
    }

    private void Draw()
    {
        

        for (int I = 0; I < rayCount; I++)
        {
            float angleVert = I * 360f / rayCount;

            for (int i = 0; i < rayCount; i++)
            {
                float angleHor = i * 360f / rayCount;

                Vector3 direction = Quaternion.Euler(angleVert, angleHor, 0) * transform.forward;

                if (Physics.Raycast(_tip.position, direction, out _touch, _distance))
                {
                    if (_touch.transform.CompareTag("EchoSurface"))
                    {

                        EchoSurface _whiteboard = _touch.transform.GetComponent<EchoSurface>();


                        Vector2 _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                        var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                        var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                        _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                        _whiteboard.texture.Apply();
                    }
                }
            }
            echoActive = false; 
        }
    }
}

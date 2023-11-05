
using System.Linq;
using UnityEngine;

public class EchoPainter : MonoBehaviour
{




    public Color paintColor;
    public int rayCount = 360;
    private float distance;
    public float startDistance;
    [SerializeField] float maxDistance;
    private Texture2D canvasTexture;
    [SerializeField] private Transform _echoOrigin;
    private RaycastHit _echoRay;
    private EchoSurface echoSurface;
    private Vector2 _touchPos;
    private int _penSize;
    private Renderer _renderer;
    private Color[] _colors;
    private bool _touchedLastFrame;





    void Start()
    {
        _renderer = _echoOrigin.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        distance = startDistance;
    }

    void Update()
    {
        //CastEcho();
        Draw(); 

    }

    private void CastEcho()
    {

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * 360f / rayCount;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * _echoOrigin.forward;

            Ray ray = new Ray(_echoOrigin.position, direction);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, distance))
            {
                Debug.Log("Object hit");



                echoSurface = hit.collider.GetComponent<EchoSurface>();


                if (echoSurface != null)
                {
                    Debug.Log("EchoSurface Detected");

                    // Get the UV coordinates of the hit point on the EchoSurface's texture
                    Vector2 uv = hit.textureCoord;

                    // Calculate pixel coordinates
                    var x = (int)(uv.x * echoSurface.texture.width);
                    var y = (int)(uv.y * echoSurface.texture.height);

                    // Change the color of the pixel on the EchoSurface's texture
                    echoSurface.texture.SetPixel(x, y, paintColor);
                    echoSurface.texture.Apply();
                    Debug.Log("Texture applied");

                }
            }
        }
    }


    private void Draw()
    {
        Vector3 direction = _echoOrigin.forward;




        if (Physics.Raycast(_echoOrigin.position, direction, out _echoRay, distance))
        {
            if (_echoRay.transform.CompareTag("EchoSurface"))
            {

                //echoSurface = _echoRay.collider.GetComponent<EchoSurface>();

                if (echoSurface == null)
                {
                    echoSurface = _echoRay.transform.GetComponent<EchoSurface>();
                }
                _touchPos = new Vector2(_echoRay.textureCoord.x, _echoRay.textureCoord.y);

                var x = (int)(_touchPos.x * echoSurface.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * echoSurface.textureSize.y - (_penSize / 2));

                //if (y < 0 || y > echoSurface.textureSize.y || x < 0 || x > echoSurface.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    echoSurface.texture.SetPixels(x, y, _penSize, _penSize, _colors);


                    echoSurface.texture.Apply();
                }

                _touchedLastFrame = true;
                return;
            }
        }

        echoSurface = null;
        _touchedLastFrame = false;
    }
}

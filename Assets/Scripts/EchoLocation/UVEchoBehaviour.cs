using UnityEngine;
using System.Collections.Generic;

public class UVEchoBehaviour : MonoBehaviour
{

    private Vector3 origin;
    private Vector3 direction;

    public List<GameObject> currentHitObjects = new List<GameObject>();
    public LayerMask layerMask;

    public float radius = 1.0f;
    public float maxDistance = 10.0f;

    public Color paintColor = Color.red;

    public float timeBetweenEchos = 1f;

    private float currentHitDistance; 

    private Material paintMaterial;

    void Update()
    {
        origin = transform.position;
        direction = transform.forward;

        currentHitDistance = maxDistance;
        currentHitObjects.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        foreach (RaycastHit hit in hits)
        {
            currentHitObjects.Add(hit.transform.gameObject);
            currentHitDistance = hit.distance;
            PaintOnUV(hit);

            Debug.Log("Hit: " + hit.transform.name + " at " + hit.point);
            Debug.Log("Normal: " + hit.normal);
            Debug.Log("UV: " + hit.textureCoord);


        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, radius);
    }

    void PaintOnUV(RaycastHit hit)
    {
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider != null)
        {
            Vector2 uv = hit.textureCoord;

            Texture2D texture = paintMaterial.mainTexture as Texture2D;

            int pixelX = (int)(uv.x * texture.width);
            int pixelY = (int)(uv.y * texture.height);

            texture.SetPixel(pixelX, pixelY, paintColor);
            texture.Apply();
        }
    }

    /*
    void Start()
    {

        Shader shader = Shader.Find("Standard"); // You can use other shaders if needed.
        paintMaterial = new Material(shader);

        Texture2D newTexture = new Texture2D(1024, 1024);
        paintMaterial.mainTexture = newTexture;


        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = paintMaterial;
        }
    }

    void Update()
    {
        Ray ray = new Ray(origin.position, origin.forward);

        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Hit: " + hit.transform.name + " at " + hit.point);
            Debug.Log("Normal: " + hit.normal);
            Debug.Log("UV: " + hit.textureCoord);
            PaintOnUV(hit);
        }
    }

    void PaintOnUV(RaycastHit hit)
    {
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider != null)
        {
            Vector2 uv = hit.textureCoord;

            Texture2D texture = paintMaterial.mainTexture as Texture2D;

            int pixelX = (int)(uv.x * texture.width);
            int pixelY = (int)(uv.y * texture.height);

            texture.SetPixel(pixelX, pixelY, paintColor);
            texture.Apply(); 
        }
    }
    */



}

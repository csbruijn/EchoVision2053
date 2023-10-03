using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetector : MonoBehaviour
{

    public string tagToDetect = "interactableEcho";
    private Vector3 collisionPoint;
    public EchoLocationBehaviour listController;
    public float volume = 10f;

    private void start()
    {
        //listController = GameObject.Find("EchoManager").GetComponent<EchoLocationBehaviour>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagToDetect))
        {
            collisionPoint = collision.contacts[0].point;
        }
        Debug.Log("Collision detected with " + tagToDetect + " at point: " + collisionPoint);

        if (listController != null)
        {
            listController.SetCollisionPoint(collisionPoint, volume);
        }
        else
        {
            Debug.Log("listController not found");
        }
    }
}

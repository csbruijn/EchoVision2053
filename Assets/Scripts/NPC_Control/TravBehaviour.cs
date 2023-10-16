using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravBehaviour : MonoBehaviour
{
    //[SerializeField] private Animator TravellerAnimator;
    public Transform spawnPoint;
    public float speed = 3.5f;
    //private bool move;

    // Start is called before the first frame update
    void Start()
    {
        //TravellerAnimator.SetBool("IsWalking", true);
        //spawnPoint = transform.position;
        //move = true;
    }

    void Update()
    {
        //if (transform.position.x > 29f) //why dont you even do this ytou fuck 
        { 
            Vector3 movement = new Vector3(0, 0, 1) * speed * Time.deltaTime;
            transform.Translate(movement);
        }
        //else
        {
            //transform.position = spawnPoint.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTriggerScript : MonoBehaviour
{

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered");

            GameObject train = GameObject.FindWithTag("Train");

            if (train != null)
            {
                train.GetComponent<TrainScript>().StartMoving();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFaceObject : MonoBehaviour
{
    public Transform target; 
    public float rotationSpeed = 5.0f; 

    private void Update()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;

            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}

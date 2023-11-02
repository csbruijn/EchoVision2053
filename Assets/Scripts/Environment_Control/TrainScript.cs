using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public bool isMoving = false;
    private float targetX;

    public void StartMoving()
    {
        isMoving = true;
        targetX = transform.position.x + 10.0f;
    }

    void Update()
    {
        TrainMoving();
    }

    private void TrainMoving()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= targetX - 0.01f)
            {
                isMoving = false;
            }
        }
    }
}

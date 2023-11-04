using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    public GameObject timerDestroyer;
    public GameObject missionSuccessShower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(timerDestroyer);
            missionSuccessShower.SetActive(true);
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRevealsTextScript : MonoBehaviour
{
    public GameObject textToBeShowed;

    private UIControllerScript uiControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        textToBeShowed.SetActive(false);
        uiControllerScript = FindObjectOfType<UIControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextShower"))
        {
            textToBeShowed.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TextShower"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Pressed");
                textToBeShowed.SetActive(false);
                StartCoroutine(uiControllerScript.TheSequence());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TextShower"))
        {
            textToBeShowed.SetActive(false);
        }
    }
}



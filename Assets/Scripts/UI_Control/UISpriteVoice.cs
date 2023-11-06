using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpriteVoice : MonoBehaviour
{
    public TMP_Text startText;
    public TMP_Text missionText;
    public GameObject phoneToDestroy;

    public AudioSource audioToPlay;
    public GameObject imageToShow;
    public GameObject objectToDestroy;

    public AudioSource audioToPlay2;
    public GameObject imageToShow2;
    public GameObject objectToDestroy2;

    public AudioSource audioToPlay3;
    public GameObject imageToShow3;
    public GameObject objectToDestroy3;

    public AudioSource audioToPlay4;
    public GameObject imageToShow4;
    public GameObject objectToDestroy4;

    void Start()
    {
        StartCoroutine(StartText());
        missionText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextShower"))
        {
            audioToPlay.Play();
            imageToShow.SetActive(true);
            StartCoroutine(DestroyObject());
        }

        if (other.gameObject.CompareTag("TextShower2"))
        {
            audioToPlay2.Play();
            imageToShow2.SetActive(true);
            StartCoroutine(DestroyObject2());
        }

        if (other.gameObject.CompareTag("TextShower3"))
        {
            audioToPlay3.Play();
            imageToShow3.SetActive(true);
            StartCoroutine(DestroyObject3());
        }

        if (other.gameObject.CompareTag("TextShower4"))
        {
            audioToPlay4.Play();
            imageToShow4.SetActive(true);
            StartCoroutine(DestroyObject4());
        }

        if (other.gameObject.CompareTag("Phone"))
        {
            StartCoroutine(MissionText());
            Destroy(phoneToDestroy);
        }
    }

    private IEnumerator StartText()
    {
        yield return new WaitForSeconds(5f);
        startText.enabled = false;
    }

    private IEnumerator MissionText()
    {
        missionText.enabled = true;
        yield return new WaitForSeconds(5f);
        missionText.enabled = false;
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(6f);
        Destroy(objectToDestroy);
    }

    private IEnumerator DestroyObject2()
    {
        yield return new WaitForSeconds(6f);
        Destroy(objectToDestroy2);
    }

    private IEnumerator DestroyObject3()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(objectToDestroy3);
    }

    private IEnumerator DestroyObject4()
    {
        yield return new WaitForSeconds(3f);
        Destroy(objectToDestroy4);
    }
}
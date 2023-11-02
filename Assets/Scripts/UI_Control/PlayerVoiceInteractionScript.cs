using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerVoiceInteractionScript : MonoBehaviour
{
    public GameObject talkingSprite;
    private VoiceCoroutineScript voiceCoroutineScript;
    private bool isCoroutineRunning = false;
    [SerializeField] private AudioSource audioSource; 

    void Start()
    {
        voiceCoroutineScript = FindObjectOfType<VoiceCoroutineScript>();
        talkingSprite.SetActive(false);
    }

    void Update()
    {
        if (!isCoroutineRunning)
        {
            talkingSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextShower"))
        {
            Debug.Log("Played");
            StartCoroutine(PlaySequenceIfNotRunning());
            talkingSprite.SetActive(true);
        }
    }

    private IEnumerator PlaySequenceIfNotRunning()
    {
        if (!voiceCoroutineScript.IsCoroutineRunning())
        {
            isCoroutineRunning = true;
            yield return StartCoroutine(voiceCoroutineScript.TheSequence());
            isCoroutineRunning = false;
        }
    }
}

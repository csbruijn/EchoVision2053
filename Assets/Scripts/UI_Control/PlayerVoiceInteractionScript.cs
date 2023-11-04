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
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        voiceCoroutineScript = FindObjectOfType<VoiceCoroutineScript>();
        talkingSprite.SetActive(false);
        PlayerVoiceLine();
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
            PlayerVoiceLine();
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

    private void PlayerVoiceLine()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(0);
        Debug.Log("started");
    }
}

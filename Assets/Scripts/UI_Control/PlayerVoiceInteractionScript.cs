using UnityEngine;
using System.Collections;

public class PlayerVoiceInteractionScript : MonoBehaviour
{
    public GameObject talkingSprite;
    private VoiceCoroutineScript voiceCoroutineScript;
    private bool isCoroutineRunning = false;
    private bool hasEnteredCollider = false;

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
            Debug.Log("Entered");
            if (!hasEnteredCollider)
            {
                StartCoroutine(PlaySequenceIfNotRunning());
                hasEnteredCollider = true;
            }
            talkingSprite.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TextShower"))
        {
            hasEnteredCollider = false;
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

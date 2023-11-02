using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoiceCoroutineScript : MonoBehaviour
{
    //[SerializeField]
    //private DialogueLine[] lines;
    //private int currentDialogueLine = 0;

    public AudioSource firstLineSubtitles;
    private bool isCoroutineRunning = false;

    public IEnumerator TheSequence()
    {
        //while (currentDialogueLine < lines.Length)
        //{
        //    yield return new WaitForSeconds(1);
        //    PlayDialogue();
        //    yield return new WaitForSeconds(3);
        //}

        isCoroutineRunning = true;

        firstLineSubtitles.Play();
        yield return new WaitForSeconds(6);
    }

    //private void PlayDialogue()
    //{
    //    lines[currentDialogueLine].line.Play();
    //    currentDialogueLine++;
    //}

    public bool IsCoroutineRunning()
    {
        return isCoroutineRunning;
    }
}

//public struct DialogueLine
//{
//    public AudioSource line;
//    public string writtenLine;
//}
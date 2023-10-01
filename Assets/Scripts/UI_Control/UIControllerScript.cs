using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControllerScript : MonoBehaviour
{

    public TMP_Text textBox;

    public AudioSource firstLineSubtitles;
    public AudioSource secondLineSubtitles;
    public AudioSource thridLineSubtitles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(1);
        firstLineSubtitles.Play();
        textBox.GetComponent<TMP_Text>().text = "Woah, I am playing VR!";
        yield return new WaitForSeconds(3);

        textBox.GetComponent<TMP_Text>().text = "";

        yield return new WaitForSeconds(1);
        secondLineSubtitles.Play();
        textBox.GetComponent<TMP_Text>().text = "Am I really doing it?";
        yield return new WaitForSeconds(3);

        textBox.GetComponent<TMP_Text>().text = "";

        yield return new WaitForSeconds(1);
        thridLineSubtitles.Play();
        textBox.GetComponent<TMP_Text>().text = "Let`s goo!";
        yield return new WaitForSeconds(3);

        textBox.GetComponent<TMP_Text>().text = "";
    }
}
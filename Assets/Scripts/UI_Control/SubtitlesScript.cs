using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesScript : MonoBehaviour
{

    public TMP_Text textBox;

    public AudioSource firstLineSubtitles;
    public AudioSource secondLineSubtitles;
    public AudioSource thridLineSubtitles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TheSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TheSequence()
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

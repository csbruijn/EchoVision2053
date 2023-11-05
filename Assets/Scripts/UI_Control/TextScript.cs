using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public TMP_Text startText;
    public TMP_Text missionText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartText()
    {
        startText.enabled = true;
        yield return new WaitForSeconds(5f);
        startText.enabled = false;
    }
}

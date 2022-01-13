using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    private AudioSource audioSource;
    TextManager textManager;
    bool audioOff = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(textManager.isTyping && audioOff)
        {
            audioOff = false;
            audioSource.Play();
        } 
        if(!textManager.isTyping && !audioOff)
        {
            audioOff = true;
            audioSource.Stop();
        }
    }
}

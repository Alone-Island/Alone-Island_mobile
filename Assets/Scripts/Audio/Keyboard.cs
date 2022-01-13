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
        float effectVolume = DataController.Instance.settingData.EffectSound;    // J : 설정 데이터의 효과음악 음량 가져오기
        bool effectMute = DataController.Instance.settingData.EffectMute;
        audioSource = GetComponent<AudioSource>();

        if (effectMute)
        {
            audioSource.volume = 0;
        } else
        {
            audioSource.volume = effectVolume;
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHappyEnding : MonoBehaviour
{
    private AudioSource audioSource;
    HappyEnding textManager;
    bool audioOff = true;
    // Start is called before the first frame update
    void Start()
    {
        float effectVolume = DataController.Instance.settingData.EffectSound;    // J : ���� �������� ȿ������ ���� ��������
        bool effectMute = DataController.Instance.settingData.EffectMute;
        audioSource = GetComponent<AudioSource>();

        if (effectMute)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = effectVolume;
        }

        textManager = GameObject.Find("HappyEnding").GetComponent<HappyEnding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textManager.isTyping && audioOff)
        {
            audioOff = false;
            audioSource.Play();
        }
        if (!textManager.isTyping && !audioOff)
        {
            audioOff = true;
            audioSource.Stop();
        }
    }
}

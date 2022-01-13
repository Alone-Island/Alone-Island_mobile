using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlay : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play(string objectName)
    {
        audioSource = GameObject.Find(objectName).GetComponent<AudioSource>();
        float effectVolume = DataController.Instance.settingData.EffectSound;    // J : ���� �������� ȿ������ ���� ��������
        bool effectMute = DataController.Instance.settingData.EffectMute;
        if (effectMute)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = effectVolume;
        }
        audioSource.Play();
    }

    public void Stop(string objectName)
    {
        audioSource = GameObject.Find(objectName).GetComponent<AudioSource>();
        audioSource.Stop();
    }
}

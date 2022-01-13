using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public Slider backgroundSlider; // J : ������� ���� ���� �����̴�
    public Slider effectSlider;       // J : ȿ������ ���� ���� �����̴�

    public AudioSource bgm;         // J : ������� component
    public List<AudioSource> effects;     // J : ȿ������ component

    public GameObject backgroundOn;       // J : ������� on Image
    public GameObject backgroundOff;      // J : ������� off Image
    public GameObject effectOn;       // J : ȿ������ on Image
    public GameObject effectOff;      // J : ȿ������ off Image

    public GameObject setting;  // J : ����â
    public bool nowSetting = false;

    private bool backgroundMute;
    private bool effectMute;

    void Start()
    {
        if (setting != null )// J : ���� ����â�� �ִ� ���
        { 
            float backgroundVolume = DataController.Instance.settingData.BackgroundSound;    // J : ���� �������� ������� ���� ��������
            backgroundMute = DataController.Instance.settingData.BackgroundMute;
            if (bgm != null)    // J : ���� BGM�� �ִ� ���
            {
                backgroundSlider.value = backgroundVolume;// J : ���� �����ͷ� ������� ���� ���� �����̴��� �ʱⰪ ����
                if (backgroundMute)
                {
                    bgm.volume = 0;  // K : ���� �����ͷ� ��� ���� ���Ұ� ������ ���� ���Ұ� ����
                    Debug.Log(bgm.volume);
                }
                else
                {
                    bgm.volume = backgroundVolume;  // J : ���� �����ͷ� ���� ���� �� ������� �ʱⰪ ����
                }
            }

            float effectVolume = DataController.Instance.settingData.EffectSound;    // J : ���� �������� ȿ������ ���� ��������
            effectMute = DataController.Instance.settingData.EffectMute;

            effectSlider.value = effectVolume;  // J : ���� �����ͷ� ȿ������ ���� ���� �����̴��� �ʱⰪ ����

            if (effectMute)
            {
                foreach (AudioSource effect in effects) // J : ��� effect sound�� ����, �����̴� �ʱⰪ ����
                    effect.volume = 0;    // J : ���� �����ͷ� ���� ���� �� ȿ������ �ʱⰪ ����
            }
            else
            {
                foreach (AudioSource effect in effects) // J : ��� effect sound�� ����, �����̴� �ʱⰪ ����
                    effect.volume = effectVolume;    // J : ���� �����ͷ� ���� ���� �� ȿ������ �ʱⰪ ����
            }

            SetBackgroundSoundImage(backgroundVolume);
            SetEffectSoundImage(effectVolume);  // J : �ʱⰪ���� �Ҹ� �̹��� ����
        }    
    }

    void Update()
    {   
        if (!backgroundMute)
        {
            BackgroundSoundSlider();
        }
        if (!effectMute)
        {
            EffectSoundSlider();
        }

        // K : ����â�� �������� ��, escŰ�� ������ ����â ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (nowSetting)
            {
                setting.SetActive(false);
                nowSetting = false;
            }
            else
            {
                setting.SetActive(true);
                nowSetting = true;
            }
        }
    }

    // J : ���� ��ư onclick
    public void SelectSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);// J : ����â ��Ȱ��ȭ
            nowSetting = false;      // J : �ð� ���߱�
        } else
        {
            setting.SetActive(true);// J : ����â Ȱ��ȭ
            nowSetting = true;      // J : �ð� ���
        }            
    }

    // J : ����â ������ ��ư onclick
    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : ����â ��Ȱ��ȭ
        nowSetting = false;         // J : �ð� ���

    }

    // J : �޴��� ���ư��� ��ư onclick
    public void SelectMenu()
    {
        Debug.Log("�޴��� ���ư���");
        setting.transform.Find("MenuAlert").gameObject.SetActive(true);    // J : �޴��� ���ư��� ���â Ȱ��ȭ
    }

    //J : �޴��� ���ư��� ���â Yes ��ư onclick
    public void SelectMenuYes()
    {
        Debug.Log("�޴��� ���ư��� O");
        SceneManager.LoadScene("GameMenu"); // J : ���Ӹ޴��� �̵�
    }

    //J : �޴��� ���ư��� ���â No ��ư onclick
    public void SelectMenuNo()
    {
        Debug.Log("�޴��� ���ư��� X");
        setting.transform.Find("MenuAlert").gameObject.SetActive(false);    // J : �޴��� ���ư��� ���â ��Ȱ��ȭ
    }

    // J : ���� ���� ��ư onclick
    public void SelectGameQuit()
    {
        Debug.Log("���� ����");
        setting.transform.Find("GameQuitAlert").gameObject.SetActive(true);    // J : ���� ���� ���â Ȱ��ȭ
    }

    //J : ���� ���� ���â Yes ��ư onclick
    public void SelectGameQuitYes()
    {
        Debug.Log("���� ���� O");
        Application.Quit(); // J : ���α׷� ����
    }

    //J : ���� ���� ���â No ��ư onclick
    public void SelectGameQuitNo()
    {
        Debug.Log("���� ���� X");
        setting.transform.Find("GameQuitAlert").gameObject.SetActive(false);    // J : �޴��� ���ư��� ���â ��Ȱ��ȭ
    }

    // J : ���� �ʱ�ȭ ��ư onclick
    public void SelectReset()
    {
        Debug.Log("���� �ʱ�ȭ");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(true); // J : ���� �ʱ�ȭ ���â Ȱ��ȭ
    }

    // J : ���� �ʱ�ȭ yes ��ư onclick
    public void SelectResetYes()
    {
        Debug.Log("���� �ʱ�ȭ O");        
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : ���� �ʱ�ȭ ���â ��Ȱ��ȭ
        SelectSettingQuit();    // J : ����â ��Ȱ��ȭ

        DataController.Instance.DeleteAllData();    // J : ��� ������ ����(.json) ����
    }

    // J : ���� �ʱ�ȭ no ��ư onclick
    public void SelectResetNo()
    {
        Debug.Log("���� �ʱ�ȭ X");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : ���� �ʱ�ȭ ���â ��Ȱ��ȭ
    }

    // J : �����̴��� ������ ���� ����+���� �����Ϳ� ����
    private void BackgroundSoundSlider()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            float backgroundVolume = backgroundSlider.value;    // J : ������� �����̴��� �� ��������
            bgm.volume = backgroundVolume;  // J : ������� ������ �����̴��� ������ ����
            DataController.Instance.settingData.BackgroundSound = backgroundVolume;  // J : ���� �����Ϳ� ����
            DataController.Instance.settingData.BackgroundMute = false;  // J : ���� �����Ϳ� ����

            SetBackgroundSoundImage(backgroundVolume);
        }
            
    }

    // J : �����̴��� ������ ���� ����+���� �����Ϳ� ����
    private void EffectSoundSlider()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {          
            float effectVolume = effectSlider.value;    // J : ȿ������ �����̴��� �� ��������
            foreach (AudioSource effect in effects) // J : ��� effect sound�� ���� ����
                effect.volume = effectVolume;    // J : ���� �����ͷ� ���� ���� �� ȿ������ �ʱⰪ ����
            DataController.Instance.settingData.EffectSound = effectVolume;  // J : ���� �����Ϳ� ����
            DataController.Instance.settingData.EffectMute = false;  // J : ���� �����Ϳ� ����

            SetEffectSoundImage(effectVolume);  // J : ������ �°� �Ҹ� �̹��� ����
        }

    }

    public void SetBackgroundSoundMute()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            backgroundMute = true;
            bgm.volume = 0;  // K : ��� ���� ���Ұ�
            DataController.Instance.settingData.BackgroundMute = true;  // J : ���� �����Ϳ� ����
            backgroundOn.SetActive(false);
            backgroundOff.SetActive(true);
        }
    }

    public void SetBackgroundSoundCancelMute()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            backgroundMute = false;
            float backgroundVolume = backgroundSlider.value;    // J : ������� �����̴��� �� ��������
            bgm.volume = backgroundVolume;  // J : ������� ������ �����̴��� ������ ����
            DataController.Instance.settingData.BackgroundSound = backgroundVolume;  // J : ���� �����Ϳ� ����
            DataController.Instance.settingData.BackgroundMute = false;  // J : ���� �����Ϳ� ����
            SetBackgroundSoundImage(backgroundVolume);
        }
    }

    public void SetEffectSoundMute()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            effectMute = true;
            foreach (AudioSource effect in effects) 
                effect.volume = 0;   // K : ȿ���� ���Ұ�
            DataController.Instance.settingData.EffectMute = true;  // J : ���� �����Ϳ� ����
            effectOn.SetActive(false);
            effectOff.SetActive(true);
        }
    }

    public void SetEffectSoundCancelMute()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            effectMute = false;
            float effectVolume = effectSlider.value;    // J : ȿ������ �����̴��� �� ��������
            foreach (AudioSource effect in effects) // J : ��� effect sound�� ���� ����
                effect.volume = effectVolume;    // J : ���� �����ͷ� ���� ���� �� ȿ������ �ʱⰪ ����
            DataController.Instance.settingData.EffectSound = effectVolume;  // J : ���� �����Ϳ� ����
            DataController.Instance.settingData.EffectMute = false;  // J : ���� �����Ϳ� ����
            SetEffectSoundImage(effectVolume);
        }
    }

    // J : ������ �°� �Ҹ� �̹��� ����
    private void SetBackgroundSoundImage(float backgroundVolume)
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            // J : ������� ������ 0���� ũ�� On �̹����� ����
            if (backgroundVolume > 0)
            {
                backgroundOn.SetActive(true);
                backgroundOff.SetActive(false);
            }
            // J : ������� ������ 0�̸� Off �̹����� ����
            else
            {
                backgroundOn.SetActive(false);
                backgroundOff.SetActive(true);
            }

            if (backgroundMute)
            {
                backgroundOn.SetActive(false);
                backgroundOff.SetActive(true);
            }
        }
    }

    private void SetEffectSoundImage(float effectVolume)
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {

            // J : ȿ������ ������ 0���� ũ�� On �̹����� ����
            if (effectVolume > 0)
            {
                effectOn.SetActive(true);
                effectOff.SetActive(false);
            }
            // J : ȿ������ ������ 0�̸� Off �̹����� ����
            else
            {
                effectOn.SetActive(false);
                effectOff.SetActive(true);
            }

            if (effectMute)
            {
                effectOn.SetActive(false);
                effectOff.SetActive(true);
            }
        }
    }
}

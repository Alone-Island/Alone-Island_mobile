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

    void Start()
    {
        float backgroundVolume = DataController.Instance.settingData.BackgroundSound;    // J : ���� �������� ������� ���� ��������
        if (bgm != null)    // J : ���� BGM�� �ִ� ���
        {
            bgm.volume = backgroundVolume;    // J : ���� �����ͷ� ���� ���� �� ������� �ʱⰪ ����
            backgroundSlider.value = backgroundVolume;  // J : ���� �����ͷ� ������� ���� ���� �����̴��� �ʱⰪ ����
        }
        
        float effectVolume = DataController.Instance.settingData.EffectSound;    // J : ���� �������� ȿ������ ���� ��������
        if (setting != null)    // J : ���� ����â�� �ִ� ���
            effectSlider.value = effectVolume;  // J : ���� �����ͷ� ȿ������ ���� ���� �����̴��� �ʱⰪ ����
        foreach (AudioSource effect in effects) // J : ��� effect sound�� ����, �����̴� �ʱⰪ ����
            effect.volume = effectVolume;    // J : ���� �����ͷ� ���� ���� �� ȿ������ �ʱⰪ ����

        SetSoundImage(backgroundVolume, effectVolume);  // J : �ʱⰪ���� �Ҹ� �̹��� ����
    }

    void Update()
    {
        SoundSlider();

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
    private void SoundSlider()
    {
        if (setting != null)    // J : ���� ����â�� �ִ� ���
        {
            float backgroundVolume = backgroundSlider.value;    // J : ������� �����̴��� �� ��������
            bgm.volume = backgroundVolume;  // J : ������� ������ �����̴��� ������ ����
            DataController.Instance.settingData.BackgroundSound = backgroundVolume;  // J : ���� �����Ϳ� ����

            float effectVolume = effectSlider.value;    // J : ȿ������ �����̴��� �� ��������
            foreach (AudioSource effect in effects) // J : ��� effect sound�� ���� ����
                effect.volume = effectVolume;    // J : ���� �����ͷ� ���� ���� �� ȿ������ �ʱⰪ ����
            DataController.Instance.settingData.EffectSound = effectVolume;  // J : ���� �����Ϳ� ����

            SetSoundImage(backgroundVolume, effectVolume);  // J : ������ �°� �Ҹ� �̹��� ����
        }
            
    }

    // J : ������ �°� �Ҹ� �̹��� ����
    private void SetSoundImage(float backgroundVolume, float effectVolume)
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
        }
    }
}

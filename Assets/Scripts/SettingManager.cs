using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public Slider backgroundSlider; // J : 배경음악 음량 조절 슬라이더
    public Slider effectSlider;       // J : 효과음악 음량 조절 슬라이더

    public AudioSource bgm;         // J : 배경음악 component
    public List<AudioSource> effects;     // J : 효과음악 component

    public GameObject backgroundOn;       // J : 배경음악 on Image
    public GameObject backgroundOff;      // J : 배경음악 off Image
    public GameObject effectOn;       // J : 효과음악 on Image
    public GameObject effectOff;      // J : 효과음악 off Image

    public GameObject setting;  // J : 설정창
    public bool nowSetting = false;

    void Start()
    {
        float backgroundVolume = DataController.Instance.settingData.BackgroundSound;    // J : 설정 데이터의 배경음악 음량 가져오기
        if (bgm != null)    // J : 씬에 BGM이 있는 경우
        {
            bgm.volume = backgroundVolume;    // J : 설정 데이터로 게임 시작 시 배경음악 초기값 세팅
            backgroundSlider.value = backgroundVolume;  // J : 설정 데이터로 배경음악 음량 조절 슬라이더의 초기값 세팅
        }
        
        float effectVolume = DataController.Instance.settingData.EffectSound;    // J : 설정 데이터의 효과음악 음량 가져오기
        if (setting != null)    // J : 씬에 설정창이 있는 경우
            effectSlider.value = effectVolume;  // J : 설정 데이터로 효과음악 음량 조절 슬라이더의 초기값 세팅
        foreach (AudioSource effect in effects) // J : 모든 effect sound의 음량, 슬라이더 초기값 세팅
            effect.volume = effectVolume;    // J : 설정 데이터로 게임 시작 시 효과음악 초기값 세팅

        SetSoundImage(backgroundVolume, effectVolume);  // J : 초기값으로 소리 이미지 세팅
    }

    void Update()
    {
        SoundSlider();

        // K : 세팅창이 켜져있을 때, esc키를 누르면 세팅창 꺼짐
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

    // J : 설정 버튼 onclick
    public void SelectSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);// J : 설정창 비활성화
            nowSetting = false;      // J : 시간 멈추기
        } else
        {
            setting.SetActive(true);// J : 설정창 활성화
            nowSetting = true;      // J : 시간 재생
        }            
    }

    // J : 설정창 나가기 버튼 onclick
    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : 설정창 비활성화
        nowSetting = false;         // J : 시간 재생

    }

    // J : 메뉴로 돌아가기 버튼 onclick
    public void SelectMenu()
    {
        Debug.Log("메뉴로 돌아가기");
        setting.transform.Find("MenuAlert").gameObject.SetActive(true);    // J : 메뉴로 돌아가기 경고창 활성화
    }

    //J : 메뉴로 돌아가기 경고창 Yes 버튼 onclick
    public void SelectMenuYes()
    {
        Debug.Log("메뉴로 돌아가기 O");
        SceneManager.LoadScene("GameMenu"); // J : 게임메뉴로 이동
    }

    //J : 메뉴로 돌아가기 경고창 No 버튼 onclick
    public void SelectMenuNo()
    {
        Debug.Log("메뉴로 돌아가기 X");
        setting.transform.Find("MenuAlert").gameObject.SetActive(false);    // J : 메뉴로 돌아가기 경고창 비활성화
    }

    // J : 게임 종료 버튼 onclick
    public void SelectGameQuit()
    {
        Debug.Log("게임 종료");
        setting.transform.Find("GameQuitAlert").gameObject.SetActive(true);    // J : 게임 종료 경고창 활성화
    }

    //J : 게임 종료 경고창 Yes 버튼 onclick
    public void SelectGameQuitYes()
    {
        Debug.Log("게임 종료 O");
        Application.Quit(); // J : 프로그램 종료
    }

    //J : 게임 종료 경고창 No 버튼 onclick
    public void SelectGameQuitNo()
    {
        Debug.Log("게임 종료 X");
        setting.transform.Find("GameQuitAlert").gameObject.SetActive(false);    // J : 메뉴로 돌아가기 경고창 비활성화
    }

    // J : 게임 초기화 버튼 onclick
    public void SelectReset()
    {
        Debug.Log("게임 초기화");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(true); // J : 게임 초기화 경고창 활성화
    }

    // J : 게임 초기화 yes 버튼 onclick
    public void SelectResetYes()
    {
        Debug.Log("게임 초기화 O");        
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : 게임 초기화 경고창 비활성화
        SelectSettingQuit();    // J : 설정창 비활성화

        DataController.Instance.DeleteAllData();    // J : 모든 데이터 파일(.json) 삭제
    }

    // J : 게임 초기화 no 버튼 onclick
    public void SelectResetNo()
    {
        Debug.Log("게임 초기화 X");
        GameObject.Find("Windows").transform.Find("ResetAlert").gameObject.SetActive(false);    // J : 게임 초기화 경고창 비활성화
    }

    // J : 슬라이더의 값으로 음량 조절+설정 데이터에 저장
    private void SoundSlider()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            float backgroundVolume = backgroundSlider.value;    // J : 배경음악 슬라이더의 값 가져오기
            bgm.volume = backgroundVolume;  // J : 배경음악 음량을 슬라이더의 값으로 세팅
            DataController.Instance.settingData.BackgroundSound = backgroundVolume;  // J : 설정 데이터에 저장

            float effectVolume = effectSlider.value;    // J : 효과음악 슬라이더의 값 가져오기
            foreach (AudioSource effect in effects) // J : 모든 effect sound의 음량 세팅
                effect.volume = effectVolume;    // J : 설정 데이터로 게임 시작 시 효과음악 초기값 세팅
            DataController.Instance.settingData.EffectSound = effectVolume;  // J : 설정 데이터에 저장

            SetSoundImage(backgroundVolume, effectVolume);  // J : 음량에 맞게 소리 이미지 세팅
        }
            
    }

    // J : 음량에 맞게 소리 이미지 세팅
    private void SetSoundImage(float backgroundVolume, float effectVolume)
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            // J : 배경음악 음량이 0보다 크면 On 이미지가 보임
            if (backgroundVolume > 0)
            {
                backgroundOn.SetActive(true);
                backgroundOff.SetActive(false);
            }
            // J : 배경음악 음량이 0이면 Off 이미지가 보임
            else
            {
                backgroundOn.SetActive(false);
                backgroundOff.SetActive(true);
            }

            // J : 효과음악 음량이 0보다 크면 On 이미지가 보임
            if (effectVolume > 0)
            {
                effectOn.SetActive(true);
                effectOff.SetActive(false);
            }
            // J : 효과음악 음량이 0이면 Off 이미지가 보임
            else
            {
                effectOn.SetActive(false);
                effectOff.SetActive(true);
            }
        }
    }
}

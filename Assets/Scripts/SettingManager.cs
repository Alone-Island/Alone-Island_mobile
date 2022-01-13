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

    private bool backgroundMute;
    private bool effectMute;

    void Start()
    {
        if (setting != null )// J : 씬에 설정창이 있는 경우
        { 
            float backgroundVolume = DataController.Instance.settingData.BackgroundSound;    // J : 설정 데이터의 배경음악 음량 가져오기
            backgroundMute = DataController.Instance.settingData.BackgroundMute;
            if (bgm != null)    // J : 씬에 BGM이 있는 경우
            {
                backgroundSlider.value = backgroundVolume;// J : 설정 데이터로 배경음악 음량 조절 슬라이더의 초기값 세팅
                if (backgroundMute)
                {
                    bgm.volume = 0;  // K : 설정 데이터로 배경 음악 음소거 설정시 음량 음소거 세팅
                    Debug.Log(bgm.volume);
                }
                else
                {
                    bgm.volume = backgroundVolume;  // J : 설정 데이터로 게임 시작 시 배경음악 초기값 세팅
                }
            }

            float effectVolume = DataController.Instance.settingData.EffectSound;    // J : 설정 데이터의 효과음악 음량 가져오기
            effectMute = DataController.Instance.settingData.EffectMute;

            effectSlider.value = effectVolume;  // J : 설정 데이터로 효과음악 음량 조절 슬라이더의 초기값 세팅

            if (effectMute)
            {
                foreach (AudioSource effect in effects) // J : 모든 effect sound의 음량, 슬라이더 초기값 세팅
                    effect.volume = 0;    // J : 설정 데이터로 게임 시작 시 효과음악 초기값 세팅
            }
            else
            {
                foreach (AudioSource effect in effects) // J : 모든 effect sound의 음량, 슬라이더 초기값 세팅
                    effect.volume = effectVolume;    // J : 설정 데이터로 게임 시작 시 효과음악 초기값 세팅
            }

            SetBackgroundSoundImage(backgroundVolume);
            SetEffectSoundImage(effectVolume);  // J : 초기값으로 소리 이미지 세팅
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
    private void BackgroundSoundSlider()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            float backgroundVolume = backgroundSlider.value;    // J : 배경음악 슬라이더의 값 가져오기
            bgm.volume = backgroundVolume;  // J : 배경음악 음량을 슬라이더의 값으로 세팅
            DataController.Instance.settingData.BackgroundSound = backgroundVolume;  // J : 설정 데이터에 저장
            DataController.Instance.settingData.BackgroundMute = false;  // J : 설정 데이터에 저장

            SetBackgroundSoundImage(backgroundVolume);
        }
            
    }

    // J : 슬라이더의 값으로 음량 조절+설정 데이터에 저장
    private void EffectSoundSlider()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {          
            float effectVolume = effectSlider.value;    // J : 효과음악 슬라이더의 값 가져오기
            foreach (AudioSource effect in effects) // J : 모든 effect sound의 음량 세팅
                effect.volume = effectVolume;    // J : 설정 데이터로 게임 시작 시 효과음악 초기값 세팅
            DataController.Instance.settingData.EffectSound = effectVolume;  // J : 설정 데이터에 저장
            DataController.Instance.settingData.EffectMute = false;  // J : 설정 데이터에 저장

            SetEffectSoundImage(effectVolume);  // J : 음량에 맞게 소리 이미지 세팅
        }

    }

    public void SetBackgroundSoundMute()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            backgroundMute = true;
            bgm.volume = 0;  // K : 배경 음악 음소거
            DataController.Instance.settingData.BackgroundMute = true;  // J : 설정 데이터에 저장
            backgroundOn.SetActive(false);
            backgroundOff.SetActive(true);
        }
    }

    public void SetBackgroundSoundCancelMute()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            backgroundMute = false;
            float backgroundVolume = backgroundSlider.value;    // J : 배경음악 슬라이더의 값 가져오기
            bgm.volume = backgroundVolume;  // J : 배경음악 음량을 슬라이더의 값으로 세팅
            DataController.Instance.settingData.BackgroundSound = backgroundVolume;  // J : 설정 데이터에 저장
            DataController.Instance.settingData.BackgroundMute = false;  // J : 설정 데이터에 저장
            SetBackgroundSoundImage(backgroundVolume);
        }
    }

    public void SetEffectSoundMute()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            effectMute = true;
            foreach (AudioSource effect in effects) 
                effect.volume = 0;   // K : 효과음 음소거
            DataController.Instance.settingData.EffectMute = true;  // J : 설정 데이터에 저장
            effectOn.SetActive(false);
            effectOff.SetActive(true);
        }
    }

    public void SetEffectSoundCancelMute()
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {
            effectMute = false;
            float effectVolume = effectSlider.value;    // J : 효과음악 슬라이더의 값 가져오기
            foreach (AudioSource effect in effects) // J : 모든 effect sound의 음량 세팅
                effect.volume = effectVolume;    // J : 설정 데이터로 게임 시작 시 효과음악 초기값 세팅
            DataController.Instance.settingData.EffectSound = effectVolume;  // J : 설정 데이터에 저장
            DataController.Instance.settingData.EffectMute = false;  // J : 설정 데이터에 저장
            SetEffectSoundImage(effectVolume);
        }
    }

    // J : 음량에 맞게 소리 이미지 세팅
    private void SetBackgroundSoundImage(float backgroundVolume)
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

            if (backgroundMute)
            {
                backgroundOn.SetActive(false);
                backgroundOff.SetActive(true);
            }
        }
    }

    private void SetEffectSoundImage(float effectVolume)
    {
        if (setting != null)    // J : 씬에 설정창이 있는 경우
        {

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

            if (effectMute)
            {
                effectOn.SetActive(false);
                effectOff.SetActive(true);
            }
        }
    }
}

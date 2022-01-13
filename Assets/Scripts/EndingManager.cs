using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   // J : UI 프로그래밍을 위해 추가 (Text 등)
using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public GameManager manager;
    public TalkManager talkManager;     // J : 엔딩 대화 데이터 가져오기 위함
    public FadeManager fadeManager;
    public CameraShake cameraShake;     // J : 카메라를 흔들기 위해 호출

    public GameObject talkPanel;        // J : 대화창
    public TextMeshProUGUI talkText;    // J : 대화창의 text
    public GameObject panel;            // N : 화면 어둡게

    public EffectPlay effect; // K: 효과음 이벤트 발생 오브젝트
    private AudioSource bgm; // K: 효과음 이벤트 발생 오브젝트

    public int endingCode;              // J : Bad Ending Code
    public int talkIndex;               // J : talkIndex를 저장하기 위한 변수

    void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    // N : 스페이스바 입력 받는 시간을 주기 위해
    public void TheEnd()
    {
        manager.isTheEnd = true;
        DataController.Instance.endingData.currentEndingCode = 0;
    }

    // J : 배드 엔딩
    public void BadEnding(int code)
    {
        endingCode = code;
        manager.isEndingShow = true;    // J : ending 보여주는 중
        DataController.Instance.endingData.firstGame = 0;
        bgm.Stop();     // K : 배경음악 끄기 
        talkPanel.SetActive(true);  // J : 대화창 활성화
        BadEndingTalk();    // J : 배드엔딩 대화 시작
    }

    // J : 배드엔딩 대화
    public void BadEndingTalk()
    {
        string talkData = talkManager.GetTalkData(11000 + endingCode, talkIndex);   // J : TalkManager로부터 talkData를 가져오기
        if (talkData == null)   // J : 해당 talkID의 talkData를 모두 가져왔다면
        {
            talkPanel.SetActive(false); // J : 대화창 비활성화
            if (endingCode == 7 || endingCode == 8) // J : 재난 엔딩인 경우 카메라 흔들기
            {
                cameraShake.Shake(BadEndingShow);
                return;
            }
            BadEndingShow();    // J : 카드 보여주기
            return;
        }
        talkText.text = talkData;       // J : talkPanel의 text를 talkData로 설정
        talkIndex++;                    // J : 해당 talkID의 다음 talkData string을 가져오기 위해
    }

    // J : 배드엔딩 카드 보여주기
    public void BadEndingShow()
    {
        switch (endingCode)
        {
            case 0:
                Debug.Log("Hungry,,,");
                DataController.Instance.endingData.hungry = 1;
                panel.transform.Find("Bad-Hungry").gameObject.SetActive(true);
                break;
            case 1:
                Debug.Log("Lonely,,,");
                DataController.Instance.endingData.lonely = 1;
                panel.transform.Find("Bad-Lonely").gameObject.SetActive(true);
                break;
            case 2:
                Debug.Log("Cold,,,");
                DataController.Instance.endingData.cold = 1;
                panel.transform.Find("Bad-Frozen").gameObject.SetActive(true);
                break;
            case 3: // N : Bad Ending (독열매)
                Debug.Log("Poison Berry,,,");
                DataController.Instance.endingData.poisonBerry = 1;
                panel.transform.Find("Bad-Berry").gameObject.SetActive(true);
                break;
            case 4: // N : Bad Ending (AI가 이해하지 못함)
                Debug.Log("먼소리야,,,");
                DataController.Instance.endingData.error = 1;
                panel.transform.Find("Bad-Error").gameObject.SetActive(true);
                break;
            case 5: // J : Bad Ending (감전사)
                Debug.Log("감전사,,,");
                DataController.Instance.endingData.electric = 1;
                panel.transform.Find("Bad-Electric").gameObject.SetActive(true);
                break;
            case 6: // J : Bad Ending (멧돼지)
                Debug.Log("멧돼지");
                DataController.Instance.endingData.pig = 1;
                panel.transform.Find("Bad-Pig").gameObject.SetActive(true);
                break;
            case 7: // J : Bad Ending (쓰나미)
                Debug.Log("쓰나미");
                DataController.Instance.endingData.storm = 1;
                panel.transform.Find("Bad-Storm").gameObject.SetActive(true);
                break;
            case 8: // J : Bad Ending (운석충돌)
                Debug.Log("운석 충돌");
                DataController.Instance.endingData.space = 1;
                panel.transform.Find("Bad-Space").gameObject.SetActive(true);
                break;
        }
        effect.Play("BadEndingEffect");
        panel.SetActive(true);

        // J : 데이터 파일 저장
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();
        Invoke("TheEnd", 2.0f);
    }



    // K : 해피 엔딩
    public void timeOutEnding()
    {
        Debug.Log("그냥 저냥 살았습니당 ~~");
        DataController.Instance.endingData.timeOut = 1;
        DataController.Instance.endingData.currentEndingCode = 101;
        // J : 데이터 파일 저장
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : 페이드아웃
    }

    public void successTwo()
    {
        Debug.Log("human과 ai는 단둘이 행복하게 살았답니다");
        DataController.Instance.endingData.two = 1;
        DataController.Instance.endingData.currentEndingCode = 102;
        // J : 데이터 파일 저장
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : 페이드아웃
    }

    public void successAI()
    {
        Debug.Log("다른 ai를 만들어내서 ai들과 함께 살게 됨");
        DataController.Instance.endingData.AITown = 1;
        DataController.Instance.endingData.currentEndingCode = 103;
        // J : 데이터 파일 저장
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : 페이드아웃
    }

    public void successPeople()
    {
        Debug.Log("통신기를 만들어서 다른 생존자들을 만남");
        DataController.Instance.endingData.people = 1;
        DataController.Instance.endingData.currentEndingCode = 104;
        // J : 데이터 파일 저장
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : 페이드아웃
    }

    // J : 페이드아웃 끝나면 호출
    public void LoadHappyScene()
    {
        SceneManager.LoadScene("HappyEnding");
    }
}


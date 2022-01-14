using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI 프로그래밍을 위해 추가 (Text 등)
using TMPro;            // J : TextMeshProUGUI를 위해 추가

// C : 전체적인 게임 진행 및 관리를 도와주는 스크립트
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;           // C : 대화창의 text
    public GameObject scanObject;   // C : 스캔된(조사한) game object
    public GameObject talkPanel;    // C : 대화창
    public bool isTPShow;           // C : talkPanel의 상태 저장 (보여주기 or 숨기기)
    public TalkManager talkManager; // C : GameManager에서 TalkManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public int talkIndex;           // C : 필요한 talkIndex를 저장하기 위한 변수 생성
    public int day = 20;            // J : 하루는 20초
    public int specialEventCoolTimeDay;        // 스페셜 이벤트가 발생하기 위한 쿨타임 일수

    public SpecialEventManager specialManager; // J : GameManager에서 SpecialEventManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public LearningManager learningManager;     // C : 플레이어가 AI를 학습시키는 action을 시도했을 때, 적절한 학습을 시킬 수 있도록 learningManager 변수 생성
    public ScreenManager screenManager; // N : 책 개수 가져오기 위해
    public AIAction aiAction;           // K : ai와 play가 충돌중인지 확인하기 위해서 > 대화 가능

    public bool playerTalk = false;           // J : 플레이어가 대화하는 중에는 special event를 유예하도록 변수 생성
    public bool isSelectedAILearning = true;         // K : AI가 학습을 할것인지 학습을 하지 않을 것인지 확인하는 플래그
    public bool isEndingShow = false;         // N : 엔딩 여부 (엔딩 카드 나타난 직후부터)
    public bool isTheEnd = false;         // N : 게임 종료 여부 (엔딩 카드 나타나고 2초 뒤부터)
    int randomNum = 0;                  // C : AI와의 대화 시, 랜덤한 대화 내용을 출력하기 위한 변수 생성
    public int dayTalk = 0;        // N : AI와의 대화 횟수

    public TextMeshProUGUI alertText;           // N : 알림창의 text

    public ObjectData aiObjData;            // C : 빌드 시 스크립트를 통한 GetComponent 방식에 에러가 발생하기 때문에 (임시로) AIObject를 퍼블릭 변수로 설정

    private string talkData;
    public int talkId;

    // C : 플레이어가 Object에 대해 조사 시(플레이어의 액션 발생 시) 적절한 내용을 포함한 대화창 띄워주기
    public void Action(GameObject scanObj)
    {
        playerTalk = true;                  // J : 플레이어가 대화하는 중에는 special event를 유예하도록 설정
        scanObject = scanObj;               // C : parameter로 들어온 스캔된 game object를 public 변수인 scanObject에 대입
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject의 ObjectData instance 가져오기

        if (talkId == 0)
        {
            talkIndex = 0;              // C : 다음 Talk()함수 사용을 위해 talkIndex를 0으로 초기화
            randomNum = 0;              // C : 다음 Talk()함수 사용을 위해 randomNum을 0으로 초기화
        }

        if (aiAction.isAICollisionToPlayer) // K : ai와 충돌중이라면 학습장소에서도 대화하기를 우선으로 한다.
        {
            //objData = GameObject.Find("AI").GetComponent<ObjectData>(); // K : ai에게 대화하기를 하기 위해 오브젝트를 AI로 가져온다.
            talkId = aiObjData.id; // K : ai에게 대화하기를 하기 위해 오브젝트를 AI로 가져온다.
        } else
        {
            talkId = objData.id;
        }
                     // K : takl data의 id 지정 변수, 예외처리를 위해 추가 설정함
        if (talkId == 1000)      // C : objData가 AI  
        {
            // N :
            if (randomNum == 1000) talkId = 2000;
            else if (randomNum == 0) // C : 대화 첫 시작
            {
                if (learningManager.isAILearning) // K : 대화를 시도 했을때, AI 학습중인 경우 예외처리
                {
                    talkId = 500;
                    randomNum = 0;
                } else
                {
                    if (dayTalk > 0)
                    {
                        talkId = 2000;                                  // N : 하루에 한 번 이상 대화를 시도하는 경우 예외 처리
                        randomNum = 1000;                               // N : 대화 중에 하루가 지나면 새로운 대화가 일어나는 것을 방지
                    }
                    else
                    {
                        System.Random rand = new System.Random();
                        randomNum = rand.Next(1, 18);                  // C : 1~18까지의 난수를 대입
                    }
                }
            }
        }
        else if (objData.id >= 100 && objData.id <= 400)     // C : 학습하기를 시도했을 때
        {
            if (learningManager.isAILearning) // K : 학습하기 조사를 했을때, AI 학습중인 경우 예외처리
            {
                talkId = 500;
            }
            else if (screenManager.currBookNum() < 1) // K : 학습하기 조사를 했을때, 책이 없는 경우 예외처리
            {
                talkId = 600;
            }

            // N : 레벨 MAX에서 예외 처리
            else if (objData.id == 100 && screenManager.farmLv.IsMax() == true) talkId = 2100;
            else if (objData.id == 200 && screenManager.houseLv.IsMax() == true) talkId = 2200;
            else if (objData.id == 300 && screenManager.craftLv.IsMax() == true) talkId = 2300;
            else if (objData.id == 400 && screenManager.engineerLv.IsMax() == true) talkId = 2400;
        }

        Talk(talkId);                   // C : 필요한 talkPanel text 값 가져오기, K : 예외처리를 위해 objData.id > talkId로 수정

        if (talkId == 1000) {
            talkPanel.SetActive(true);      // C : talkPanel 숨기거나 보여주기
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(false); // N : 알림창 숨기기
        }
        else
        {
            talkPanel.SetActive(false);      // C : talkPanel 숨기거나 보여주기
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(true); // N : 알림창 띄워주기
        }

        if (talkData == null)
        {
            talkPanel.SetActive(false);      // C : talkPanel 숨기거나 보여주기
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(false); // N : 알림창 띄워주기
        }
    }

    // C : 상황에 따라 적절하게 필요한 talkPanel text 값 대화창에 띄우기
    void Talk(int id)
    {
        // C : 조사한 object에 해당하는 talkData 중 talkIndex 위치의 string을 가져오기
        talkData = talkManager.GetTalkData(id + randomNum, talkIndex);

        if (talkData == null || !isSelectedAILearning)           // C : 해당하는 id의 talkData string들을 모두 가져왔다면
        {
            if (id >= 100 && id <= 400)                          // C : 학습하기에 대한 talkData를 모두 가져온 경우
            {
                if (isSelectedAILearning)                        // C : AI가 학습을 해야하는/할수 있는 상황이라면
                {
                    learningManager.Learning(id);                // C : 적절한 학습을 실행한다.
                }
            }
            else if (id == 1000) screenManager.HeartStudy(0);    // C : AI와 대화하기에 대한 talkData를 모두 가져온 경우, 공감 능력을 1 상승시킨다.

            isSelectedAILearning = true;
            playerTalk = false;         // J : 정상적으로 special event가 발동하도록 설정
            isTPShow = false;           // C : talkPanel의 show 상태 false로 저장
            talkIndex = 0;              // C : 다음 Talk()함수 사용을 위해 talkIndex를 0으로 초기화
            randomNum = 0;              // C : 다음 Talk()함수 사용을 위해 randomNum을 0으로 초기화
            if (id == 1000) dayTalk++;  // N : 하루 대화 횟수 증가
            return;
        }

        if (id == 1000) talkText.text = talkData;       // C : talkPanel의 text를 talkData로 설정
        else alertText.text = talkData;                 // N : 알림창의 text를 talkData로 설정

        isTPShow = true;                // C : talkPanel의 show 상태 true로 저장 (해당하는 id의 talkData string이 아직 남아있음)
        talkIndex++;                    // C : 해당하는 id의 다음 talkData string을 가져오기 위해
    }

    void Start()
    {
        specialEventCoolTimeDay = 10;    // J : 스페셜 이벤트 쿨타임 설정
    }
}

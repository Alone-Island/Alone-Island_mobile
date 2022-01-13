using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // J : UI 프로그래밍을 위해 추가 (Text 등)
using TMPro;            // J : TextMeshProUGUI를 위해 추가

public class SpecialEventManager : MonoBehaviour
{
    public TalkManager talkManager;     // J : TalkManager의 함수를 호출할 수 있도록 talkManager 변수 생성
    public GameManager gameManager;     // J : 스페셜 이벤트 발동 조건을 체크하기 위해
    public ScreenManager screenManager; // N : 레벨 관리를 위해 호출
    public EndingManager endingManager; // N : 엔딩 처리를 위해 호출
    public GameObject talkPanel;        // J : 대화창
    public TextMeshProUGUI talkText;    // J : 대화창의 text
    public int talkIndex;               // J : talkIndex를 저장하기 위한 변수
    public bool special = false;        // J : 스페셜 이벤트 진행중인지 여부
    public bool specialTalk = false;    // J : AI가 스페셜 이벤트 대화를 하는지 여부 (선택지 선택 전)
    public bool resultTalk = false;     // J : 결과 텍스트 창을 보여주는지 여부 (선택지 선택 후)
    public TextMeshProUGUI selectText0, selectText1, selectText2;
    public Button selectButton0, selectButton1, selectButton2;

    public EffectPlay effect; // K: 효과음 이벤트 발생 오브젝트

    List<TextMeshProUGUI> selectText;   // J : 선택지 텍스트를 관리하기 위한 리스트
    List<Button> selectButton;          // J : 선택지 버튼을 관리하기 위한 리스트
    int specialID;                      // J : TalkManager로부터 talkData를 가져오기 위한 변수
    int firstRandomNum;                 // J : 랜덤 스페셜 이벤트를 위한 변수1 (0 : 선택지 2개, 1: 선택지 3개)
    int secondRandomNum;                // J : 랜덤 스페셜 이벤트를 위한 변수2
    int select;                         // J : 몇번째 선택지를 클릭했는지
    int endingCode;                     // J : 배드엔딩 코드
    List<int> beforeRandomNum;          // J : 지금까지 실행한 스페셜 이벤트 리스트


    // C : 스페셜이벤트 발생 직전, 플레이어 머리 위에 '!' 오브젝트를 띄워주기 위한 변수
    public PlayerAction playerAction;

    // J : Special Event 발생
    public void Action() 
    {
        special = true;

        System.Random rand = new System.Random();
        
        int danger = (int)((10 - screenManager.houseLv.fCurrValue));   // J : 위험도 계산
        if (rand.Next(100) < danger)    // J : 위험도가 높아 재난 발생
        {
            endingCode = (new System.Random()).Next(2) + 7;  // J : 각 재난은 50% 확률로 발생 (7 or 8)
            StartCoroutine("DisasterAfterAlarm");   // C : 스페셜이벤트 발생 알람 후 재난 발생
        }
        else 
        {
            specialTalk = true;  // J : Jump키를 눌렀을 때 object scan을 할 수 없게 함
            int currentID; bool different = true;
            while (true)    // J : 이전에 발생했던 스페셜 이벤트는 발생하지 않음
            {
                firstRandomNum = rand.Next(2);      // J : 0-1까지의 난수 생성 (0 : 선택지 2개, 1: 선택지 3개)
                secondRandomNum = rand.Next(1, 5);  // J : 1-4까지의 난수 생성
                currentID = 10 * firstRandomNum + secondRandomNum;
                foreach (int beforeID in beforeRandomNum)
                {
                    different = true;
                    if (currentID == beforeID)  // J : 이전에 발생했던 스페셜 이벤트인 경우
                    {
                        different = false;
                        break;
                    }
                }
                if (!different) // J : 난수 다시 생성
                    continue;
                // J : 이전에 발생하지 않았던 스페셜 이벤트인 경우
                beforeRandomNum.Add(currentID); // J : 지금까지 실행한 스페셜 이벤트 리스트에 추가
                specialID = 10000 + currentID; // J : talkData를 갖고 오기 위해 specialID 계산
                break;
            }

            StartCoroutine("TalkAfterAlarm");   // C : 스페셜이벤트 발생 알람 후 대화창 활성화 및 대화 시작
        }
    }

    // J : 스페셜 이벤트를 발동할 수 있는 상태인지 체크
    private IEnumerator Check()
    {
        // J : 대화가 끝날 때까지 대기
        while (true)
        {
            Debug.Log(gameManager.playerTalk);
            if (!gameManager.playerTalk)    // J : 대화가 끝나면 스페셜 이벤트 발동
            {
                Action();
                break;
            }
            yield return null;
        }
    }
        

    // J : 실행될 때마다 다음 문장으로 넘어감
    public void Talk() 
    {
        string talkData = talkManager.GetTalkData(specialID, talkIndex);   // J : TalkManager로부터 talkData를 가져오기
        if (talkData == null)   // J : 해당 talkID의 talkData를 모두 가져왔다면
        {
            specialTalk = false;     // J : Jump키를 눌렀을 때 object scan을 할 수 있게 함
            talkIndex = 0;      // J : talk index 초기화
            Select();           // J : 선택지 화면에 보임
            return;
        }
        talkText.text = talkData;       // J : talkPanel의 text를 talkData로 설정
        talkIndex++;                    // J : 해당 talkID의 다음 talkData string을 가져오기 위해
    }

    // J : 선택지 클릭 후 호출, 실행될 때마다 다음 문장으로 넘어감
    public void ResultTalk()
    {
        resultTalk = true;  // J : 결과 텍스트를 보여주는 상태
        string talkData = talkManager.GetResultData(specialID * 10 + select, talkIndex);   // J : TalkManager로부터 resultData를 가져오기
        if (talkData == null)   // J : 해당 talkID의 resultData를 모두 가져왔다면
        {
            resultTalk = false;     // J : 결과 텍스트 종료
            EndSpecialEvent();
            Result();           // J : 결과 반영
            return;
        }
        talkText.text = talkData;       // J : talkPanel의 text를 resultData로 설정
        talkIndex++;                    // J : 해당 talkID의 다음 resultData string을 가져오기 위해
    }

    // J : 선택지가 화면에 나타남
    void Select()
    {
        string selectData;        
        for (int selectIndex = 0; (selectData = talkManager.GetSelectData(specialID, selectIndex)) != null; selectIndex++) // J : selectData의 개수에 따라 selectButton이 보임
        {
            selectText[selectIndex].text = selectData;              // J : SelectButton의 text에 selectData대입
            selectButton[selectIndex].gameObject.SetActive(true);   // J : SelectButton 활성화
        }
    }

    // J : 결과 텍스트를 모두 보여준 뒤 호출됨, 결과 반영
    void Result()
    {
        switch (select) // J : 몇번째 선택지를 클릭했는지
        {
            case 0:
                switch (firstRandomNum)
                {
                    case 0:     // J : 선택지가 2개인 경우
                        switch (secondRandomNum)
                        {
                            case 1: // J : 배터리가 많이 닳았어요ㅠㅠ "하루만 아무것도 안하고 싶어요..
                                screenManager.dayTime = 20;
                                screenManager.HeartStudy(1); // N : 공감 1레벨 상승
                                break;
                            case 2: // J : 박사님을 위해 새로운 열매를 따왔어요!
                                endingManager.BadEnding(3); // N : Bad Ending (독열매)
                                break;
                            case 3: // J : 저기 야생동물이 있는 것 같아요! 잡아서 구워먹을까요?
                                screenManager.HeartStudy(1); // J : 공감 1레벨 상승
                                break;
                            case 4: // J : 저기 야생동물이 있는 것 같아요! 잡아서 구워먹을까요?
                                endingManager.BadEnding(6);  // J : 멧돼지 사망
                                break;

                        }
                        break;
                    case 1:     // J : 선택지가 3개인 경우
                        switch (secondRandomNum)
                        {
                            case 1: // J : 이 꽃 너무 이쁘지 않아요??
                                endingManager.BadEnding(4); // N : Bad Ending (AI가 이해하지 못함)
                                break;
                            case 2: // J : (AI가 물에 빠졌다)
                                endingManager.BadEnding(5);  // 감전사 사망
                                break;
                            case 3: // J : (나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)
                                screenManager.dayTime = 20;
                                screenManager.HeartStudy(2); // N : 공감 2레벨 상승
                                break;
                            case 4: // C : 박사님 신기하게 생긴 생물을 발견했어요! 박사님께 드리려고 힘들게 잡았어요ㅎㅎ
                                screenManager.HeartStudy(-1);   // C : 공감 1레벨 하락
                                break;
                        }
                        break;
                }
                break;
            case 1:
                switch (firstRandomNum)
                {
                    case 0:     // J : 선택지가 2개인 경우
                        screenManager.HeartStudy(-1); // J : 공감 1레벨 하락
                        break;
                    case 1:     // J : 선택지가 3개인 경우
                        switch (secondRandomNum)
                        {
                            case 1: // J : 이 꽃 너무 이쁘지 않아요??
                                screenManager.HeartStudy(1); // J : 공감 1레벨 상승
                                break;
                            case 2: // J : (AI가 물에 빠졌다)
                                screenManager.HeartStudy(-1); // J : 공감 1레벨 하락
                                break;
                            case 3: // J : (나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)
                                    // J : 변화없음
                                break;
                            case 4: // C : 박사님 신기하게 생긴 생물을 발견했어요! 박사님께 드리려고 힘들게 잡았어요ㅎㅎ
                                    // C : 변화 없음
                                break;
                        }
                        break;
                }
                break;
            case 2:
                switch (secondRandomNum)
                {
                    case 1: // J : 이 꽃 너무 이쁘지 않아요??
                        screenManager.HeartStudy(-1); // J : 공감 1레벨 하락
                        break;
                    case 2: // J : (AI가 물에 빠졌다)
                            // J : 구조 성공
                        break;
                    case 3: // J : (나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)
                        endingManager.BadEnding(5); // N : Bad Ending (AI 고장)
                        break;
                    case 4: // C : 박사님 신기하게 생긴 생물을 발견했어요! 박사님께 드리려고 힘들게 잡았어요ㅎㅎ
                        screenManager.HeartStudy(1);    // C : 공감 1레벨 상승
                        break;
                }
                break;
        }
    }


    // J : SelectButton0을 클릭했을 때 호출되는 함수
    public void Select0()
    {
        select = 0;
        ResultTalk();
        SelectComplete();   // J :선택이 완료되면 선택지 비활성화
    }

    // J : SelectButton1을 클릭했을 때 호출되는 함수
    public void Select1()
    {
        select = 1;
        ResultTalk();
        SelectComplete();   // J :선택이 완료되면 선택지 비활성화
    }

    // J : SelectButton2을 클릭했을 때 호출되는 함수
    public void Select2()
    {
        select = 2;
        ResultTalk();
        SelectComplete();   // J :선택이 완료되면 선택지 비활성화
    }

    // J :선택이 완료되면 호출, 대화창과 선택지 비활성화
    void SelectComplete()
    {
        for (int i = 0; i < 3; i++)
            selectButton[i].gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        selectText = new List<TextMeshProUGUI>();
        selectText.Add(selectText0);
        selectText.Add(selectText1);
        selectText.Add(selectText2);

        selectButton = new List<Button>();
        selectButton.Add(selectButton0);
        selectButton.Add(selectButton1);
        selectButton.Add(selectButton2);

        beforeRandomNum = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // J : 스페셜 이벤트 종료 (변수 초기화)
    private void EndSpecialEvent()
    {
        special = false;
        talkIndex = 0;      // J : talk index 초기화
        talkPanel.SetActive(false); // J : 대화창 비활성화
    }

    // C : 스페셜이벤트 발생 알람 후 재난 발생
    IEnumerator DisasterAfterAlarm()
    {
        // K : 스페셜 이벤트 효과음 발생
        effect.Play("SpecialEventEffect");

        playerAction.StartCoroutine("OnAlarm");
        yield return new WaitForSeconds(2.2f);      // C : 알람 애니메이션 끝난 후
        effect.Stop("SpecialEventEffect");
        EndSpecialEvent();
        endingManager.BadEnding(endingCode);
    }

    // C : 스페셜이벤트 발생 알람 후 대화창 활성화 및 대화 시작
    IEnumerator TalkAfterAlarm()
    {
        // K : 스페셜 이벤트 효과음 발생
        effect.Play("SpecialEventEffect");

        playerAction.StartCoroutine("OnAlarm");
        yield return new WaitForSeconds(2.2f);      // C : 알람 애니메이션 끝난 후
        effect.Stop("SpecialEventEffect");
        talkPanel.SetActive(true);
        Talk();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    public ScreenManager screenManager;     // C : 적절한 학습하기 함수를 실행하기 위한 변수
    public AIAction aiAction;               // C : 학습하기에 따른 AI의 행동을 결정하기 위한 변수
    public bool isAILearning = false;         // K : AI가 학습중인지 확인하는 변수
   
    private float initLearningTime = 4;     // C : 초기 학습 시간
    public float learningTime = 4;  // C : 스크린에 명시할 남은 학습 시간, K : 기본 학습시간으로 초기화

    public GameObject farmTextObject;       // C : 농사 학습하기 완료 시, 농사 레벨 위에 +1 애니메이션을 실행하기 위해 필요한 변수
    public GameObject houseTextObject;      // C : 건축 학습하기 완료 시, 건축 레벨 위에 +1 애니메이션을 실행하기 위해 필요한 변수
    public GameObject craftTextObject;      // C : 공예 학습하기 완료 시, 공예 레벨 위에 +1 애니메이션을 실행하기 위해 필요한 변수
    public GameObject engineerTextObject;   // C : 공학 학습하기 완료 시, 공학 레벨 위에 +1 애니메이션을 실행하기 위해 필요한 변수
    public GameObject levelUp;              // C : 학습하기 완료 시, 해당 레벨 위에 띄울 레벨업 애니메이션이 설정된 game object 변수
    private float levelUpEffectTime = 0;    // C : 레벨업 애니메이션이 진행될 시간을 조절하기 위한 변수

    private int learningId;                 // C : 학습하기 id (100~400)

    // C : 학습하기 완료 시, 적절한 결과 및 애니메이션 실행
    public void CompleateLearning()
    {
        isAILearning = false;
        learningTime = initLearningTime;       // K : 기본 학습시간으로 초기화

        // C : 학습하기 id에 따라 다른 결과 실행
        switch (learningId)
        {
            // C : 농사 학습하기
            case 100:
                screenManager.FarmStudy();      // C : 농사 레벨 증가

                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(farmTextObject.transform);
                levelUp.SetActive(true);

                break;
            // C : 건축 학습하기
            case 200:             
                screenManager.HouseStudy();     // C : 건축 레벨 증가
               
                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(houseTextObject.transform);
                levelUp.SetActive(true);

                break;
            // C : 공예 학습하기
            case 300:
                screenManager.CraftStudy();     // C : 공예 레벨 증가

                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(craftTextObject.transform);
                levelUp.SetActive(true);

                break;
            // C : 공학 학습하기
            case 400:
                screenManager.EngineerStudy();  // C : 공학 레벨 증가

                // C : levelUp animation 실행하기
                levelUp.transform.SetParent(engineerTextObject.transform);
                levelUp.SetActive(true);

                break;
            // C : 에러 핸들링
            default:
                Debug.Log("fail learning level up");
                break;
        }
    }
    
    public void WaitingLearning() // K : AI 학습 시간을 기다리는 함수
    {
        learningTime--; // K : 1초마다 남은 학습시간 1 감소
        if (learningTime == 0) // K : 남은 학습시간이 0이 되면
        {
            CompleateLearning(); // K : AI 학습 시간이 끝나면 호출되는 함수
        } else
        {
            Invoke("WaitingLearning", 1);   // K : 학습이 시간이 끝날때까지 1초에 한번씩 호출, 재귀함수
        }
    }

    // C : 학습하기
    public void Learning(int id)
    {
        learningId = id;
        // C : AI 학습이 가능할 경우
        if (!isAILearning && !aiAction.isAICollisionToPlayer) {
            switch (learningId)
            {
                // C : 농사 학습하기
                case 100:
                    Debug.Log(learningId);
                    screenManager.useBook();                // C : 책 사용에 따른 책 개수 줄이기
                    screenManager.FarmLevelUpAnimation();   // C : 농사 학습에 따른 농사 학습장소 맵 변화
                    learningTime = initLearningTime + screenManager.farmLv.fCurrValue; // C : 학습시간 조절
                    aiAction.GoToLearningPlace(-7, -7);     // C : AI를 학습장소로 이동시키기
                    isAILearning = true;                    // C : AI 학습상태 true로 변환       
                    Invoke("WaitingLearning", 1);           // C : 학습완료까지 기다리기
                    
                    break;
                // C : 건축 학습하기
                case 200:
                    Debug.Log(learningId);
                    screenManager.useBook();                // C : 책 사용에 따른 책 개수 줄이기
                    screenManager.HouseLevelUpAnimation();  // C : 건축 학습에 따른 건축 학습장소 맵 변화
                    learningTime = initLearningTime + screenManager.houseLv.fCurrValue; // C : 학습시간 조절
                    aiAction.GoToLearningPlace(7, 8);      // C : AI를 학습장소로 이동시키기
                    isAILearning = true;                    // C : AI 학습상태 true로 변환
                    Invoke("WaitingLearning", 1);           // C : 학습완료까지 기다리기

                    break;
                // C : 공예 학습하기
                case 300:
                    Debug.Log(learningId);
                    screenManager.useBook();                // C : 책 사용에 따른 책 개수 줄이기
                    screenManager.CraftLevelUpAnimation();  // C : 공예 학습에 따른 공예 학습장소 맵 변화
                    learningTime = initLearningTime + screenManager.craftLv.fCurrValue; // C : 학습시간 조절
                    aiAction.GoToLearningPlace(5, 0);       // C : AI를 학습장소로 이동시키기
                    isAILearning = true;                    // C : AI 학습상태 true로 변환
                    Invoke("WaitingLearning", 1);           // C : 학습완료까지 기다리기

                    break;
                // C : 공학 학습하기
                case 400:
                    Debug.Log(learningId);
                    screenManager.useBook();                    // C : 책 사용에 따른 책 개수 줄이기
                    screenManager.EngineerLevelUpAnimation();   // C : 공학 학습에 따른 공학 학습장소 맵 변화
                    learningTime = initLearningTime + screenManager.engineerLv.fCurrValue; // C : 학습시간 조절
                    aiAction.GoToLearningPlace(-5, 5);          // C : AI를 학습장소로 이동시키기
                    isAILearning = true;                        // C : AI 학습상태 true로 변환
                    Invoke("WaitingLearning", 1);               // C : 학습완료까지 기다리기

                    break;
                // C : 에러 핸들링
                default:
                    Debug.Log("fail learning");
                    break;
            }
        }
    }

    void Update()
    {
        // C : 레벨업 애니메이션의 화면 실행 시간을 조정
        if (levelUp.activeSelf == true)                 // C : 레벨업 object의 active 상태가 true이면
        {
            levelUpEffectTime += Time.deltaTime;        // C : 레벨업 애니메이션 타임에 흐른 시간 추가
            if (levelUpEffectTime > 2f)                 // C : 레벨업 애니메이션 타임이 2초가 지났을 때
            {
                levelUp.SetActive(false);               // C : 레벨업 object의 active 상태를 false로 변환
                levelUpEffectTime = 0;                  // C : 다음 애니메이션을 위해 레벨업 애니메이션 타임을 0으로 초기화
            }
        }
        else                                            // C : 레벨업 object의 active 상태가 false이면
        {
            levelUpEffectTime = 0;                      // C : 다음 애니메이션을 위해 레벨업 애니메이션 타임을 0으로 초기화
        }
    }
}

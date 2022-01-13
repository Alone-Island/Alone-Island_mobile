using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HappyEnding : MonoBehaviour
{
    public GameObject panel;            // N : 화면 어둡게

    [SerializeField] public TextMeshProUGUI dialog; // K : text 오브젝트를 받아오기 위한 변수입니다. > using TMPro;          
    public string[] fullText;                       // K : 타이핑 효과를 줄 텍스트를 담기 위한 배열입니다

    private int endingCode;                         // K : 해피 엔딩 코드를 담을 변수 입니다.

    public EffectPlay effect; // K: 효과음 이벤트 발생 오브젝트


    // K : synopsys의 텍스트들(여러 문장)의 배열입니다.

    private string[] happySosoLifeFullText = {
        "벌써 90일이 지났어요..",
        "이런 하루하루도 나쁘지는 않네요..",
        "앞으로도 잘 지내봐요. 박사님.."
    };
    private string[] happyAIFullText = {
        "박사님! 박사님이 가르칠 로봇들이 잔뜩 생겼어요..",
        "제 공학 능력이 이렇게 성장했네요..",
        "앞으로 저희가 다같이 박사님을 도울게요..",
        "박사박사님!!님!! 박박사님!사님! 박사님!!!"
    };
    private string[] happyPeopleFullText = {
        "박사님!! \n제가 통신기를 만들었어요!!!!",
        "사실 매일 조금씩 만들고 있었는데... \n드디어 완성이에요!!",
        "지금 당장 통신기를 사용해봐요!!!",
        "치...지직....치지지..직...여기는....",
    };
    private string[] happyTwoFullText = {
        "박사님! 제가 열심히 꽃을 심었더니 우리 섬이 예뻐졌어요!!",
        "박사님과의 하루하루가 정말 즐거워요..",
        "박사님도 그러셨으면 좋겠어요.."
    };


    string subText; // K : synopsys의 텍스트(한 문장) 일부를 저장하기 위한 변수입니다.
    int currentPoint = 0; // K : synopsysFullText에서 현재 포인터가 어디있는지 저장하기 위한 변수입니다.

    public bool isTyping = true; // K : 현재 글자가 화면에 타이핑되고 있는지 확인하기 위한 변수입니다.
    bool isSkipPart = false;     // K : 현재 문장을 타이핑 효과 없이 바로 화면에 띄울지 확인하기 위한 변수입니다.
    bool isEnd = false;                 // K : 해피엔딩 카드가 뜨고 일정시간이 지난 후 해피엔딩 신이 종료했느닞 확인하기 위한 변수입니다.
    bool isEndingCardShow = false;      // K : 해피엔딩 카드가 화면에 띄워졌는지 확인하기 위한 변수입니다.

    void Start()
    {
        endingCode = DataController.Instance.endingData.currentEndingCode; // K : DataController 중 eningData에 저장된 현재 엔딩코드를 가져옵니다.
        switch (endingCode) // K : 엔딩코드에 따라 맞는 엔딩 스크립트를 가져옵니다.
        {
            case 101:
                fullText = happySosoLifeFullText;
                break;
            case 102:
                fullText = happyTwoFullText;
                break;
            case 103:
                fullText = happyAIFullText;
                break;
            case 104:
                fullText = happyPeopleFullText;
                break;
            default:
                Debug.Log("해피에딩 에러");
                break;
        }
        StartCoroutine("TypingAction", 0);          // K : 스크립트의 시작과 동시에 시놉시스를 타이핑을 시작하는 코드입니다.
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : 스페이스바를 눌렀을 때
        {
            if (!isEndingCardShow)  // K : 해피 엔딩 카드가 보여지고 있지 않을때만 타이핑 효과를 주기 위한 코드입니다.
            {
                if (!isTyping)  // K : 현재 글자가 화면에 타이핑 되고 있지 않을 때
                {
                    // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있지 않는다면, 다음 문장 타이핑하기 위한 코드입니다.
                    StartCoroutine("TypingAction", 0);
                }
                else
                {
                    // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있다면, 부분 스킵을 위해 isSkipPart true로 변경하는 코드입니다.
                    isSkipPart = true;
                }
            } 
            if (isEnd) // K : 해피엔딩이 종료되었을 때만 엔딩 크레딧 신으로 넘어가기 위한 코드입니다.
            {
                SceneManager.LoadScene("EndingCredits");
            }
        }
    }

    IEnumerator TypingAction()
    {
        if (currentPoint >= fullText.Length)    // K : 모든 텍스트들을 타이핑 했을 때
        {           
            ShowHappyEndingCard();              // K : 모든 텍스트가 타이핑이 종료하면 해피엔딩 카드를 띄우는 함수를 호출합니다.
            StopCoroutine("TypingAction");
            yield return 0;
        }

        dialog.text = "";   // K : Text 오브젝트의 text 초기화
        isTyping = true;    // K : 텍스트 화면에 타이핑을 시작했기 때문에, isTyping true

        for (int i = 0; i < fullText[currentPoint].Length; i++) // K : 텍스트 한 문장의 한 글자 한 글자를 화면에 나타나게 하기 위한 반복문
        {
            yield return new WaitForSeconds(0.07f); // K : 텍스트 한 글자 한 글자 사이의 딜레이

            subText += fullText[currentPoint].Substring(0, i);  // K : 텍스트의 인덱스 0~i까지 자름
            dialog.text = subText;                                      // K : Text 오브젝트에 subText 적용 
            subText = "";                                               // K : subText 초기화

            if (isSkipPart)                                             // K : 부분 스킵이 true면
            {
                dialog.text = fullText[currentPoint];           // K : Text 오브젝트에 전체 텍스트 문장을 띄운다
                isSkipPart = false;                                     // K : isSkipPart 초기화
                isTyping = false;                                       // K : isTyping 초기화
                break;
            }
        }

        isTyping = false;   // isTyping 초기화
        currentPoint++;     // 텍스트 배열의 포인터 옮김
    }



    // N : 스페이스바 입력 받는 시간을 주기 위해
    public void TheEnd()
    {
        isEnd = true;                                             // K : 해피 엔딩 신이 종료됨
        DataController.Instance.endingData.currentEndingCode = 0; // K : DataController 중 eningData에 저장된 현재 엔딩코드를 초기화 합니다.
    }

    // J : 모든 엔딩의 공통 코드
    private void ending()
    {
        effect.Play("HappyEndingEffect");
        Debug.Log("Game Happy Ending Credits");
        currentPoint = 0;               // K : 타이핑 효과를 준 배열의 포인터를 0으로 초기화
        subText = "";                   // K : 타이핑 효과를 주기 위해 텍스트의 일부 를 담기 위한 변수 초기화
        isTyping = false;               // K : 타이핑이 멈춤
        isSkipPart = false;             // K : 타이핑 스킵 초기화

        isEndingCardShow = true;        // K : 엔딩 카드가 띄워졌음

        panel.SetActive(true);                                  // K : 배경을 어둡게 하기위한 패널을 띄웁니다
        DataController.Instance.endingData.firstGame = 0;      // K : 첫번째 게임이 아님을 확인하기 위해 firstGame을 0으로 변경합니다.
    }


    // K : 해피 엔딩 endingCode 101-104
    public void ShowHappyEndingCard()
    {        
        ending();                       // K : 공통 엔딩 코드 함수 호출
        switch (endingCode)
        {
            case 101:
                panel.transform.Find("Happy-SosoLife").gameObject.SetActive(true);
                break;
            case 102:
                panel.transform.Find("Happy-Two").gameObject.SetActive(true);
                break;
            case 103:
                panel.transform.Find("Happy-AITown").gameObject.SetActive(true);
                break;
            case 104:
                panel.transform.Find("Happy-People").gameObject.SetActive(true);
                break;
            default:
                Debug.Log("해피엔딩 카드 에러");
                break;
        }
        Invoke("TheEnd", 2.0f);           // K : 카드를 띄운 뒤 2초후에 해피 엔딩 신 종료 함수 호출
    }
}

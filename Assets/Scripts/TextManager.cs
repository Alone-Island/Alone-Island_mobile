using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// K : 텍스트에 효과를 주기 위한 매니저입니다.
public class TextManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text 오브젝트를 받아오기 위한 변수입니다. > using TMPro;          
    public GameManager manager;
    public EndingManager endingManager;
    private string[] fullText;                       // K : 타이핑 효과를 줄 텍스트를 담기 위한 배열입니다

    // K : synopsys의 텍스트들(여러 문장)의 배열입니다.
    private string[] synopsysFullText = { 
        "나는 로봇공학자 K...\nAI 로봇을 개발하기 위해\n연구실에서만 지낸 시간이 수년... ", 
        "드디어... 완성했다...\n나의 역작, AI 로봇 NJ-C!!!!\n이제 세상에 공개할 때가 왔...",
        "아니...뭐지??",
        "내가 살고 있던 마을은 섬이 되어있었다...",
        "통신은 모두 끊겨있고..\n사람들은 전혀 보이지 않는다!!\n갑자기 무인도에 혼자 남게 되다니?!?",
        "하지만 나에게는 NJ-C가 있어!\n아직 깡통에 불과하는 이 로봇을 학습시킨다면\n생존할 수 있을거야!!",
        "이곳에서 '3개월만' 버텨보자!"
    };

    string subText; // K : synopsys의 텍스트(한 문장) 일부를 저장하기 위한 변수입니다.
    int currentPoint = 0; // K : synopsysFullText에서 현재 포인터가 어디있는지 저장하기 위한 변수입니다.

    public bool isTyping = true; // K : 현재 글자가 화면에 타이핑되고 있는지 확인하기 위한 변수입니다.
    bool isSkipPart = false;     // K : 현재 문장을 타이핑 효과 없이 바로 화면에 띄울지 확인하기 위한 변수입니다.

    void Start() {
        fullText = synopsysFullText;                // K : synopsys를 타이핑 효과를 주기 위해 fullText에 담는 코드입니다.

        StartCoroutine("TypingAction", 0);          // K : 스크립트의 시작과 동시에 시놉시스를 타이핑을 시작하는 코드입니다.
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : 스페이스바를 눌렀을 때
        {

            if (!isTyping)  // K : 현재 글자가 화면에 타이핑 되고 있지 않을 때
            {
                // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있지 않는다면, 다음 문장 타이핑하기 위한 코드
                StartCoroutine("TypingAction", 0);
            }
            else
            {
                // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있다면, 부분 스킵을 위해 isSkipPart true로 변경하는 코드
                isSkipPart = true;
            }
        }
    }

    public void GoToGameScreen() // K : 게임 신으로 가는 함수입니다.
    {
        // K : 모든 변수 초기화
        StopCoroutine("TypingAction");  // K : 타이핑 코루틴을 멈추기 위한 코드
        currentPoint = 0;               // K : 타이핑 효과를 준 배열의 포인터를 0으로 초기화
        subText = "";                   // K : 타이핑 효과를 주기 위해 텍스트의 일부 를 담기 위한 변수 초기화
        isTyping = false;               // K : 타이핑이 멈춤
        isSkipPart = false;             // K : 타이핑 스킵 초기화

        SceneManager.LoadScene("MainGame"); // K : 시놉시스가 끝나고 게임 신으로 가게 하는 함수입니다. > using UnityEngine.SceneManagement
    }

    IEnumerator TypingAction() {
        if (currentPoint >= fullText.Length)    // K : 모든 텍스트들을 타이핑 했을 때
        {
            Debug.Log("Game Start"); // K : 모든 텍스트를 출력 완료, 게임 플레이 신으로 이동
            GoToGameScreen();
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

    // J : 화면 터치하면 실행
    public void ScreenTouch()
    {
        if (!isTyping)  // K : 현재 글자가 화면에 타이핑 되고 있지 않을 때
        {
            // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있지 않는다면, 다음 문장 타이핑하기 위한 코드
            StartCoroutine("TypingAction", 0);
        }
        else
        {
            // K : 스페이스바를 눌렀을 때 현재 글자가 화면에 타이핑 되고 있다면, 부분 스킵을 위해 isSkipPart true로 변경하는 코드
            isSkipPart = true;
        }
    }
}

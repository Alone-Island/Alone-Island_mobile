using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public PlayerStat hungerStat; // N : 배고픔 스탯
    public PlayerStat happyStat; // N : 행복 스탯
    public PlayerStat temperatureStat; // N : 체온 스탯
    public PlayerStat dangerStat; // N : 위험 스탯

    public AILevel farmLv; // N : 농사 레벨
    public AILevel houseLv; // N : 건축 레벨
    public AILevel craftLv; // N : 공예 레벨
    public AILevel engineerLv; // N : 공학 레벨
    public AILevel heartLv; // N : 공감 레벨

    public LearningManager learningManager;
    public TextMeshProUGUI learningTime;
    public TextMeshProUGUI learningTitle;

    private int day = 0; // N : 날짜
    public int dayTime = 0; // N : 하루의 시간

    //private int bookNum = 0; // N : 책 개수
    [SerializeField] private TextMeshProUGUI calender; // N : 날짜 텍스트
    [SerializeField] private TextMeshProUGUI book; // N : 책 개수 텍스트
    [SerializeField] private int bookNum;

    public EndingManager endingManager;
    public GameManager gameManager;     // J : GameManager에서 하루가 몇초인지 가져옴
    public SpecialEventManager specialManager;
    public SettingManager settingManager;

    // C : 공감 능력 향상 시 레벨업 애니메이션을 위해
    public GameObject heartTextObject;      // C : 공감 레벨 위에 +1 애니메이션을 실행하기 위해 필요한 변수
    public GameObject levelUp;              // C : AI와 대화하기 완료 시, 공감 레벨 위에 띄울 레벨업 애니메이션이 설정된 game object 변수
    public GameObject levelDown;            // C : 스페셜 이벤트에서 부적절한 선택지를 골랐을 때, 공감 레벨 위에 띄울 레벨다운 애니메이션이 설정된 game object 변수
    //private float time = 0;               // C :
    private float downTime = 0;             // C : 레벨다운 애니메이션이 진행될 시간을 조절하기 위한 변수

    private Color currColor = new Color(1f, 1f, 1f, 0f);        // N :
    Transform nextMap;                                          // N :
    Transform preMap;                                           // N :

    // Start is called before the first frame update
    void Start()
    {
        // N : 스탯 초기화
        hungerStat.initStat(100, 100);
        happyStat.initStat(50, 100);
        temperatureStat.initStat(100, 100);
        //dangerStat.initStat(100, 100);

        // N : 레벨 초기화
        farmLv.initLv(1, 10);
        houseLv.initLv(1, 10);
        craftLv.initLv(1, 10);
        engineerLv.initLv(1, 15);
        heartLv.initLv(1, 20);

        // N : 캘린더 초기화
        calender.text = "day 01";
        // N : 책 개수 초기화
        book.text = "0 books";

        // N : 장소 맵 초기화
        GameObject.Find("Farm").transform.Find("Lv3-4").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Farm").transform.Find("Lv5-6").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Farm").transform.Find("Lv7-8").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Farm").transform.Find("Lv9-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("House").transform.Find("Lv3-5").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("House").transform.Find("Lv6-8").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("House").transform.Find("Lv9-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Craft_Room").transform.Find("Lv4-7").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Craft_Room").transform.Find("Lv8-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Lab").transform.Find("Lv3-5").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Lab").transform.Find("Lv6-8").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Lab").transform.Find("Lv9-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("plant").transform.Find("Lv8-14").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("plant").transform.Find("Lv15-20").GetComponent<Renderer>().material.color = currColor;

        dayAfter();
        //Invoke("dayAfter", gameManager.day);
    }

    // Update is called once per frame
    void Update()
    {
        // N :learningTime 
        if(learningManager.isAILearning)
        {
            learningTime.text = learningManager.learningTime.ToString();
            learningTitle.alpha = 1;
        } else
        {
            learningTime.text = "";
            learningTitle.alpha = 0;
        }


        /*
        // C :
        if (levelUp.activeSelf == true)     // C :
        {
            time += Time.deltaTime;
            if (time > 2f)                      // C : 
            {
                levelUp.SetActive(false);
                time = 0;
            }
        }
        else
        {
            time = 0;
        }
        */

        // C : 레벨다운 애니메이션의 화면 실행 시간을 조정
        if (levelDown.activeSelf == true)           // C : 레벨다운 object의 active 상태가 true이면
        {
            downTime += Time.deltaTime;             // C : 레벨다운 애니메이션 타임에 흐른 시간 추가
            if (downTime > 2f)                      // C : 레벨다운 애니메이션 타임이 2초가 지났을 때
            {
                levelDown.SetActive(false);         // C : 레벨다운 object의 active 상태를 false로 변환
                downTime = 0;                       // C : 다음 애니메이션을 위해 레벨다운 애니메이션 타임을 0으로 초기화
            }
        }
        else                                        // C : 레벨다운 object의 active 상태가 false이면
        {
            downTime = 0;                           // C : 다음 애니메이션을 위해 레벨다운 애니메이션 타임을 0으로 초기화
        }
    }

    // N : 날짜 변화
    public void dayAfter()
    {
        if (gameManager.isEndingShow) return;
        // N : 캘린더 관리

        day++;
        if (day < 10) calender.text = "day " + "0" + day.ToString();
        else calender.text = "day " + day.ToString();

        // N : 90일 이후
        if (day >= 90)
        {
            endingManager.timeOutEnding();
            return;
        }

        // J : 스페셜 이벤트 주기마다 스페셜 이벤트 발동
        if (day % gameManager.specialEventCoolTimeDay == 0)
            specialManager.StartCoroutine("Check");

        if (day > 1)
        {
            // N : 스탯 관리
            hungerStat.fCurrValue = hungerStat.fCurrValue + farmLv.fCurrValue - 10;
            happyStat.fCurrValue = happyStat.fCurrValue + heartLv.fCurrValue - 5;
            temperatureStat.fCurrValue = temperatureStat.fCurrValue + craftLv.fCurrValue - 10;
        }        

        // N : 엔딩 처리
        if (hungerStat.fCurrValue <= 0) endingManager.BadEnding(0);
        else if (happyStat.fCurrValue <= 0) endingManager.BadEnding(1);
        else if (temperatureStat.fCurrValue <= 0) endingManager.BadEnding(2);

        //N : AI와 대화 횟수 초기화
        gameManager.dayTalk = 0;

        timeFly();
    }

    public void timeFly()
    {
        // J : 설정하는 중이나 스페셜 이벤트 중에는 시간이 흐르지 않음
        if(!settingManager.nowSetting && !specialManager.special) dayTime++;

        if (dayTime >= gameManager.day) { dayTime = 0; dayAfter(); }
        else Invoke("timeFly", 1);
    }

    public int currBookNum()
    {
        return bookNum;
    }

    // N : 책 줍기
    public void getBook()
    {
        bookNum++;
        book.text = bookNum.ToString() + " books";
    }

    // N : 책 쓰기
    public void useBook()
    {
        bookNum--;
        book.text = bookNum.ToString() + " books";
    }

    // N : 농사 배우기
    public void FarmStudy()
    {
        farmLv.fCurrValue++;
        hungerStat.fCurrValue += 50;
    }

    // N : 건축 배우기
    public void HouseStudy()
    {
        houseLv.fCurrValue++;
        //dangerStat.fCurrValue += 50;
    }

    // N : 공예 배우기
    public void CraftStudy()
    {
        craftLv.fCurrValue++;
        temperatureStat.fCurrValue += 50;

        
    }

    // N : 공학 배우기
    public void EngineerStudy()
    {
        engineerLv.fCurrValue++;
               

        // N : 엔딩 처리
        if (engineerLv.fCurrValue >= engineerLv.maxValue)
        {
            if (heartLv.fCurrValue > 17.0f) endingManager.successPeople(); // N : 공감 능력이 높은 경우
            else endingManager.successAI(); // N : 공감 능력이 낮은 경우
        }
    }

    // N : 공감 배우기 (말걸기 n = 0)
    public void HeartStudy(int n)
    {
        if (n == 0)
        {
            //useBook();
            heartLv.fCurrValue++;
            happyStat.fCurrValue += 5;
        }
        else
        {
            heartLv.fCurrValue += n;
            happyStat.fCurrValue += (5 * n);
        }

        // C : levelUp animation 실행하기
        if (n >= 0)
        {
            levelUp.transform.SetParent(heartTextObject.transform);
            levelUp.SetActive(true);
        }
        else
        {
            levelDown.SetActive(true);
        }

        // C : 공감 능력 레벨에 따른 맵 변화
        HeartLevelUpAnimation();

        // N : 엔딩 처리
        if (happyStat.fCurrValue < 0 || heartLv.fCurrValue < 0) endingManager.BadEnding(1);
        else if (heartLv.fCurrValue >= heartLv.maxValue)
        {
            if (engineerLv.fCurrValue > 13.0f) endingManager.successPeople(); // N : 공학 능력이 높은 경우
            else endingManager.successTwo(); // N : 공학 능력이 낮은 경우
        }
    }

    // N, K : 농사 학습시 레벨업 애니매이션
    public void FarmLevelUpAnimation()
    {
        if (farmLv.fCurrValue == 8)
        {
            // N : 레벨 9-10
            preMap = GameObject.Find("Farm").transform.Find("Lv7-8");
            nextMap = GameObject.Find("Farm").transform.Find("Lv9-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (farmLv.fCurrValue == 6)
        {
            // N : 레벨 7-8
            preMap = GameObject.Find("Farm").transform.Find("Lv5-6");
            nextMap = GameObject.Find("Farm").transform.Find("Lv7-8");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (farmLv.fCurrValue == 4)
        {
            // N : 레벨 5-6
            preMap = GameObject.Find("Farm").transform.Find("Lv3-4");
            nextMap = GameObject.Find("Farm").transform.Find("Lv5-6");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (farmLv.fCurrValue == 2)
        {
            nextMap = GameObject.Find("Farm").transform.Find("Lv3-4");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
        
    }

    // N, K : 건축 학습시 레벨업 애니매이션
    public void HouseLevelUpAnimation()
    {
        if (houseLv.fCurrValue == 8)
        {
            // N : 레벨 9-10
            preMap = GameObject.Find("House").transform.Find("Lv6-8");
            nextMap = GameObject.Find("House").transform.Find("Lv9-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (houseLv.fCurrValue == 5)
        {
            // N : 레벨 6-8
            preMap = GameObject.Find("House").transform.Find("Lv3-5");
            nextMap = GameObject.Find("House").transform.Find("Lv6-8");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (houseLv.fCurrValue == 2)
        {
            nextMap = GameObject.Find("House").transform.Find("Lv3-5");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
    }
    // N, K : 공예 학습시 레벨업 애니매이션
    public void CraftLevelUpAnimation()
    {
        if (craftLv.fCurrValue == 7)
        {
            // N : 레벨 8-10
            preMap = GameObject.Find("Craft_Room").transform.Find("Lv4-7");
            nextMap = GameObject.Find("Craft_Room").transform.Find("Lv8-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (craftLv.fCurrValue == 3)
        {
            preMap = GameObject.Find("Craft_Room").transform.Find("Lv1-3");
            nextMap = GameObject.Find("Craft_Room").transform.Find("Lv4-7");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
    }
    // N, K : 공학 학습시 레벨업 애니매이션
    public void EngineerLevelUpAnimation()
    {
        if (engineerLv.fCurrValue == 8)
        {
            // N : 레벨 9-10
            preMap = GameObject.Find("Lab").transform.Find("Lv6-8");
            nextMap = GameObject.Find("Lab").transform.Find("Lv9-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (engineerLv.fCurrValue == 5)
        {
            // N : 레벨 6-8
            preMap = GameObject.Find("Lab").transform.Find("Lv3-5");
            nextMap = GameObject.Find("Lab").transform.Find("Lv6-8");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (engineerLv.fCurrValue == 2)
        {
            nextMap = GameObject.Find("Lab").transform.Find("Lv3-5");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
    }
    // N, K : 공감 학습시 레벨업 애니매이션
    public void HeartLevelUpAnimation()
    {
        if (heartLv.fCurrValue == 14)
        {
            // N : 레벨 8-14
            preMap = GameObject.Find("plant").transform.Find("Lv8-14");
            nextMap = GameObject.Find("plant").transform.Find("Lv15-20");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (heartLv.fCurrValue == 7)
        {
            nextMap = GameObject.Find("plant").transform.Find("Lv8-14");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
    }

    IEnumerator fadeIn()
    {
        nextMap.gameObject.SetActive(true); // J : 다음 레벨의 맵 활성화
        while (currColor.a < 1f)
        {
            currColor.a += 0.1f;
            nextMap.GetComponent<Renderer>().material.color = currColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator fadeOut()
    {
        while (currColor.a > 0f)
        {
            currColor.a -= 0.1f;
            preMap.GetComponent<Renderer>().material.color = currColor;
            yield return new WaitForSeconds(0.1f);
        }
        preMap.gameObject.SetActive(false); // J : 이전 레벨의 맵 비활성화
        StartCoroutine("fadeIn");
    }
}
